using Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;
using Engineering.Application.Services.TransportationRequests.Commands.ChangeCargoToSecurityConfirm;
using Engineering.Application.Services.TransportationRequests.Commands.ChangeStatus;
using Engineering.Application.Services.TransportationRequests.Commands.ChangeStatusToAccepted;
using Engineering.Application.Services.TransportationRequests.Commands.ChangeStatusToRejected;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToAccepted;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToPaid;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToPending;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToRequestRejection;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToRequestResended;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToSecurityConfirmTransportation;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToSendDoneTransportation;
using Engineering.Application.Services.TransportationRequests.Models.GroupTransportationRequestStatusChanger;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests;

public partial class TransportationRequestLogic : ITransportationRequestLogic
{
    public async Task<Result<GroupTransportationRequestStatusChangerResponse?>> GroupTransportationRequestStatusChanger(GroupTransportationRequestStatusChangerRequest request, CT ct)
    {
        _logger.LogInformation("Request for GroupTransportationRequestStatusChanger, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<GroupTransportationRequestStatusChangerValidator, GroupTransportationRequestStatusChangerRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GroupTransportationRequestStatusChangerResponse>(isValidRequest.Error!);

        List<TransportationRequest>? confirmtransportations = [];
        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new ChangeStatusTransportationRequestCommand(
                item, request.Status, request.ManagerDescription), ct);
            if (response.IsFailure || response.Value is null)
                return Result.Failure<GroupTransportationRequestStatusChangerResponse>(response.Error!);
            var data = response.Value;

            if (request.Status == TransportationRequestStatus.Accepted &&
                data.Transportation.TransportationType != TransportationType.SnappPassenger &&
                data.Transportation.TransportationType != TransportationType.Airplane)
                confirmtransportations.Add(data);
        }

        await _unitOfWork.CommitAsync(ct);

        if (confirmtransportations is not null && confirmtransportations.Count > 0)
        {
            var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
            if (config.IsSuccess && config.Value is not null && config.Value.SendTelegramMessage)
                foreach (var item in confirmtransportations)
                {
                    var sendTelegramResult = await SendMessageConfirmedTransportationRequest(item, ct);
                }
        }

        return new GroupTransportationRequestStatusChangerResponse(true);
    }

    public async Task<Result<ChangeToAcceptedTransportationRequestResponse?>> ChangeToAcceptedTransportationRequest(ChangeToAcceptedTransportationRequestRequest request, CT ct)
    {
        _logger.LogInformation("Request for ChangeToAcceptedTransportationRequest, Id:{Id}", request.Id);
        //Validate data
        var isValidRequest = await request.IsValidAsync<ChangeToAcceptedTransportationRequestValidator, ChangeToAcceptedTransportationRequestRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ChangeToAcceptedTransportationRequestResponse>(isValidRequest.Error!);

        var transporationRequest = await _repository.GetByIdNoInclude(request.Id, ct);
        if (transporationRequest is null)
            return Result.Failure<ChangeToAcceptedTransportationRequestResponse>(TransportationRequestErrors.TransportationRequestWithIdNotFound);

        if (!(transporationRequest.TransportationRequestStatus == TransportationRequestStatus.Pending))
            return Result.Failure<ChangeToAcceptedTransportationRequestResponse>(TransportationRequestErrors.UnValidStatus);

        var currentUser = _userProfileService.GetProfileInfo();
        if (currentUser is null)
            return Result.Failure<ChangeToAcceptedTransportationRequestResponse>(TransportationRequestErrors.UserInfoNotFound);

        var response = await _mediator.Send(new ChangeStatusToAcceptedTransportationRequestCommand(transporationRequest.Id, request.ManagerDescription, currentUser.UserId, DateTime.Now, request.PanelPaid), ct);
        if (response.IsFailure)
            return Result.Failure<ChangeToAcceptedTransportationRequestResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);

        var config = await _mediator.Send(new GetActiveConfigQuery(), ct);
        if (config.IsSuccess && config.Value is not null && config.Value.SendTelegramMessage)
            await SendMessageConfirmedTransportationRequest(transporationRequest, ct);

        return new ChangeToAcceptedTransportationRequestResponse(transporationRequest.Id, true);
    }

    public async Task<Result<ChangeToPaidTransportationRequestResponse?>> ChangeToPaidTransportationRequest(ChangeToPaidTransportationRequestRequest request, CT ct)
    {
        _logger.LogInformation("Request for ChangeToPaidTransportationRequest, Id:{Id}", request.Id);
        //Validate data
        var isValidRequest = await request.IsValidAsync<ChangeToPaidTransportationRequestValidator, ChangeToPaidTransportationRequestRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ChangeToPaidTransportationRequestResponse>(isValidRequest.Error!);

        var transporationRequest = await _repository.GetByIdNoInclude(request.Id, ct);
        if (transporationRequest is null)
            return Result.Failure<ChangeToPaidTransportationRequestResponse>(TransportationRequestErrors.TransportationRequestWithIdNotFound);

        if (!(transporationRequest.TransportationRequestStatus == TransportationRequestStatus.Accepted))
            return Result.Failure<ChangeToPaidTransportationRequestResponse>(TransportationRequestErrors.UnValidStatus);

        //ChangeToPaid TransportationRequest
        var response = await _mediator.Send(new ChangeStatusTransportationRequestCommand(
            transporationRequest.Id, TransportationRequestStatus.Paid, request.ManagerDescription), ct);
        if (response.IsFailure)
            return Result.Failure<ChangeToPaidTransportationRequestResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ChangeToPaidTransportationRequestResponse(response.Value!.Id, true);
    }

    public async Task<Result<ChangeToPendingTransportationRequestResponse?>> ChangeToPendingTransportationRequest(ChangeToPendingTransportationRequestRequest request, CT ct)
    {
        _logger.LogInformation("Request for ChangeToPendingTransportationRequest, Id:{Id}", request.Id);
        //Validate data
        var isValidRequest = await request.IsValidAsync<ChangeToPendingTransportationRequestValidator, ChangeToPendingTransportationRequestRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ChangeToPendingTransportationRequestResponse>(isValidRequest.Error!);

        var transporationRequest = await _repository.GetByIdNoInclude(request.Id, ct);
        if (transporationRequest is null)
            return Result.Failure<ChangeToPendingTransportationRequestResponse>(TransportationRequestErrors.TransportationRequestWithIdNotFound);

        if (!(transporationRequest.TransportationRequestStatus == TransportationRequestStatus.InitialRegistration ||
              transporationRequest.TransportationRequestStatus == TransportationRequestStatus.RequestResended))
            return Result.Failure<ChangeToPendingTransportationRequestResponse>(TransportationRequestErrors.UnValidStatus);

        //ChangeToPending TransportationRequest
        var response = await _mediator.Send(new ChangeStatusTransportationRequestCommand(
            transporationRequest.Id, TransportationRequestStatus.Pending, null), ct);
        if (response.IsFailure)
            return Result.Failure<ChangeToPendingTransportationRequestResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ChangeToPendingTransportationRequestResponse(response.Value!.Id, true);
    }

    public async Task<Result<ChangeToRequestRejectionTransportationRequestResponse?>> ChangeToRequestRejectionTransportationRequest(ChangeToRequestRejectionTransportationRequestRequest request, CT ct)
    {
        _logger.LogInformation("Request for ChangeToRequestRejectionTransportationRequest, Id:{Id}", request.Id);
        //Validate data
        var isValidRequest = await request.IsValidAsync<ChangeToRequestRejectionTransportationRequestValidator, ChangeToRequestRejectionTransportationRequestRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ChangeToRequestRejectionTransportationRequestResponse>(isValidRequest.Error!);

        var transporationRequest = await _repository.GetByIdNoInclude(request.Id, ct);
        if (transporationRequest is null)
            return Result.Failure<ChangeToRequestRejectionTransportationRequestResponse>(TransportationRequestErrors.TransportationRequestWithIdNotFound);

        if (!(transporationRequest.TransportationRequestStatus == TransportationRequestStatus.Pending))
            return Result.Failure<ChangeToRequestRejectionTransportationRequestResponse>(TransportationRequestErrors.UnValidStatus);

        var currentUser = _userProfileService.GetProfileInfo();
        if (currentUser is null)
            return Result.Failure<ChangeToRequestRejectionTransportationRequestResponse>(TransportationRequestErrors.UserInfoNotFound);

        //ChangeToRequestRejection TransportationRequest
        var response = await _mediator.Send(new ChangeStatusToRejectedTransportationRequestCommand(transporationRequest.Id, request.ManagerDescription, currentUser.UserId, DateTime.Now), ct);
        if (response.IsFailure)
            return Result.Failure<ChangeToRequestRejectionTransportationRequestResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ChangeToRequestRejectionTransportationRequestResponse(response.Value!.Id, true);
    }

    public async Task<Result<ChangeToRequestResendedTransportationRequestResponse?>> ChangeToRequestResendedTransportationRequest(ChangeToRequestResendedTransportationRequestRequest request, CT ct)
    {
        _logger.LogInformation("Request for ChangeToRequestRejectionTransportationRequest, Id:{Id}", request.Id);
        //Validate data
        var isValidRequest = await request.IsValidAsync<ChangeToRequestResendedTransportationRequestValidator, ChangeToRequestResendedTransportationRequestRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ChangeToRequestResendedTransportationRequestResponse>(isValidRequest.Error!);

        var transporationRequest = await _repository.GetByIdNoInclude(request.Id, ct);
        if (transporationRequest is null)
            return Result.Failure<ChangeToRequestResendedTransportationRequestResponse>(TransportationRequestErrors.TransportationRequestWithIdNotFound);

        if (!(ValidateTransportationRequestStatus.AllowStatusForReturn.Any(x => x == transporationRequest.TransportationRequestStatus)))
            return Result.Failure<ChangeToRequestResendedTransportationRequestResponse>(TransportationRequestErrors.UnValidStatus);

        var response = await _mediator.Send(new ChangeStatusTransportationRequestCommand(transporationRequest.Id,
            TransportationRequestStatus.RequestReturned, request.ManagerDescription), ct);
        if (response.IsFailure)
            return Result.Failure<ChangeToRequestResendedTransportationRequestResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ChangeToRequestResendedTransportationRequestResponse(response.Value!.Id, true);
    }

    public async Task<Result<ChangeToSendDoneTransportationResponse?>> ChangeToSendDoneTransportation(ChangeToSendDoneTransportationRequest request, CT ct)
    {
        var response = await _mediator.Send(new ChangeStatusTransportationRequestCommand(
            request.Id, TransportationRequestStatus.SendDone, request.ManagerDescription, true), ct);
        if (response.IsFailure) return Result.Failure<ChangeToSendDoneTransportationResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ChangeToSendDoneTransportationResponse(response.Value!.Id, true);
    }

    public async Task<Result<ChangeToSecurityConfirmTransportationResponse?>> ChangeToSecurityConfirmTransportation(ChangeToSecurityConfirmTransportationRequest request, CT ct)
    {
        if (request.Id is not null && request.Id > 0)
        {
            var response = await _mediator.Send(new ChangeStatusTransportationRequestCommand(
                request.Id.Value, TransportationRequestStatus.SecurityConfirm, request.ManagerDescription, true), ct);
            if (response.IsFailure) return Result.Failure<ChangeToSecurityConfirmTransportationResponse>(response.Error!);
        }

        if (request.CargoId is not null && request.CargoId > 0)
        {
            var response = await _mediator.Send(new ChangeCargoToSecurityConfirmCommand(
                request.CargoId.Value, request.ManagerDescription, true), ct);
            if (response.IsFailure) return Result.Failure<ChangeToSecurityConfirmTransportationResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new ChangeToSecurityConfirmTransportationResponse(request.Id, request.CargoId, true);
    }
}