using Engineering.Application.Services.DailyProjectOperations.Models.CreateDailyProjectOperation;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationById;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationService;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyServiceInfo;
using Engineering.Application.Services.DailyProjectOperations.Models.UpdateDailyProjectOperation;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewards;
using Engineering.Application.Services.TelegramChats.TelegramServices;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetsCurrencyById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredByIds;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredByIds;
using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.Messengers.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.RequestRewards;
using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Queries.GetUserById;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.DailyProjectOperations;

public partial class DailyProjectOperationLogic : IDailyProjectOperationLogic
{
    ///Private Methods
    private async Task<List<Currency>?> CurrencyDataReceiver(
        List<RequestReward>? requestRewards, CT ct)
    {
        List<Currency>? currencies = [];
        var allIds = new List<long>();
        if (requestRewards is not null && requestRewards.Count > 0)
        {
            foreach (var requestReward in requestRewards)
            {
                if (requestReward.RequestRewardProducts is not null && requestReward.RequestRewardProducts.Count > 0)
                {
                    var ids = requestReward.RequestRewardProducts?.Select(x => (long)x.CurrencyId).Distinct().ToList();
                    if (ids is not null && ids.Count > 0)
                        allIds.AddRange(ids);
                }
                if (requestReward is not null)
                {
                    if (requestReward.CurrencyId is not null && requestReward.CurrencyId > 0)
                        allIds.Add(requestReward.CurrencyId!.Value);
                }
            }

            if (allIds.Count > 0)
            {
                var currenciesData = await _mediator.Send(new GetsCurrencyByIdQuery(1, allIds.Count, allIds, true), ct);
                currencies = currenciesData.Value?.Data;
            }
        }

        return currencies;
    }

    private async Task<List<Product>?> ProductDataReceiver(
        List<RequestReward>? requestRewards, CT ct)
    {
        List<Product>? products = [];
        var allIds = new List<long>();
        if (requestRewards is not null && requestRewards.Count > 0)
        {
            foreach (var requestReward in requestRewards)
            {
                if (requestReward.RequestRewardProducts is not null && requestReward.RequestRewardProducts.Count > 0)
                {
                    var ids = requestReward.RequestRewardProducts?.Select(x => (long)x.ProductId).Distinct().ToList();
                    if (ids is not null && ids.Count > 0)
                        allIds.AddRange(ids);
                }
            }

            if (allIds.Count > 0)
            {
                var productData = await _mediator.Send(new GetsProductByIdQuery(1, allIds.Count, null, allIds), ct);
                products = productData.Value?.Data;
            }
        }

        return products;
    }

    private async Task<List<FilteredUserModel>?> ThirdPartyDataReceiver(
        List<RequestReward>? requestRewards, CT ct)
    {
        List<FilteredUserModel>? users = [];
        var allIds = new List<long>();
        if (requestRewards is not null && requestRewards.Count > 0)
        {
            foreach (var requestReward in requestRewards)
            {
                if (requestReward.RequestRewardThirdParties is not null && requestReward.RequestRewardThirdParties.Count > 0)
                {
                    var ids = requestReward.RequestRewardThirdParties?.Select(x => (long)x.ThirdPartyId).Distinct().ToList();
                    if (ids is not null && ids.Count > 0)
                        allIds.AddRange(ids);
                }
            }

            if (allIds.Count > 0)
            {
                var userData = await _mediator.Send(new GetFilteredByIdsQuery(allIds, null, 1, allIds.Count), ct);
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                users = userData.Value?.Data;
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            }
        }

        return users;
    }

    private GetDailyProjectOperationRequestRewardModel RequestRewardFullModeling(
        DailyProjectOperation dailyProjectOperation,
        RequestReward item,
        List<GetRequestRewardProductModel>? requestRewardProductModels,
        List<GetRequestRewardDocumentModel>? requestRewardDocuments,
        List<GetRequestRewardThirdPartyModel>? requestRewardThirdPartyModels,
        Currency? currency)
    {
        return new GetDailyProjectOperationRequestRewardModel()
        {
            CostCenterId = item.CostCenter!.Id,
            CurrencyId = item.CurrencyId,
            Currency = currency?.Name,
            Id = item.Id,
            Description = item.Description,
            OfferedPrice = item.OfferedPrice,
            ProjectId = item.Project?.Id,
            ProjectOperationDetailId = item.ProjectOperationDetail?.Id,
            ProjectOperationId = item.ProjectOperation?.Id,
            RegistrationDate = item.RegistrationDate,
            Type = item.Type,
            Products = requestRewardProductModels?.Count > 0 ? requestRewardProductModels : null,
            Documents = requestRewardDocuments?.Count > 0 ? requestRewardDocuments : null,
            ThirdPartyIds = requestRewardThirdPartyModels?.Count > 0 ? requestRewardThirdPartyModels : null,
            CostCenterName = dailyProjectOperation.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
            ProjectName = dailyProjectOperation.ProjectOperationDetail.ProjectOperation.Project.ProjectName,
            OperationInfoName = dailyProjectOperation.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
            PrivateName = dailyProjectOperation.ProjectOperationDetail.OperationLocation.PrivateName,
            PrivateCode = dailyProjectOperation.ProjectOperationDetail.OperationLocation.PrivateCode,
            PublicName = dailyProjectOperation.ProjectOperationDetail.OperationLocation.PublicName,
            PublicCode = dailyProjectOperation.ProjectOperationDetail.OperationLocation.PublicCode,
        };
    }

    private GetDailyProjectOperationByIdResponse DailyProjectOperationFullModeling(
        DailyProjectOperation value,
        List<GetDailyProjectOperationByIdDocumentModel>? documents,
        List<GetDailyProjectOperationByIdExpertModel>? experts,
        List<GetDailyProjectOperationByIdMachineryModel>? machineries,
        List<GetDailyProjectOperationByIdSeviceModel>? services,
        List<GetDailyProjectOperationProduct> products,
        List<GetDailyProjectOperationRequestRewardModel>? requestRewards,
        Company? company)
    {
        return new GetDailyProjectOperationByIdResponse()
        {
            Id = value.Id,
            Type = value.Type,
            StartDate = value.StartDate,
            EndDate = value.EndDate,
            CreatorId = value.CreatorId,
            Created = TimeCalculator.DatePiker(value.Created),
            Status = value.Status,
            Height = value.Height,
            Length = value.Length,
            Width = value.Width,
            Number = value.Number,
            PrivateName = value.ProjectOperationDetail.OperationLocation.PrivateName,
            PrivateCode = value.ProjectOperationDetail.OperationLocation.PrivateCode,
            PublicName = value.ProjectOperationDetail.OperationLocation.PublicName,
            PublicCode = value.ProjectOperationDetail.OperationLocation.PublicCode,
            ProjectOperationDetailId = value.ProjectOperationDetail.Id,
            Weight = value.Weight,
            OperationInfoCode = value.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode,
            OperationInfoName = value.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
            Volume = value.DailyProjectOperationServices.Sum(x => x.Volume),
            ProjectOperationId = value.ProjectOperationDetail.ProjectOperation.Id,
            ProjectId = value.ProjectOperationDetail.ProjectOperation.Project.Id,
            ProjectName = value.ProjectOperationDetail.ProjectOperation.Project.ProjectName,
            CostCenterId = value.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
            CostCenterName = value.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
            Documents = documents,
            Experts = experts,
            Machineris = machineries,
            Services = services,
            Products = products,
            RequestRewards = requestRewards,
            Description = value.Description,
            CompanyId = value.CompanyId,
            CompanyNameFa = company?.NameFa
        };
    }

    private List<long>? GetMeasureunitIds(
        List<GetsDailyProjectOperationServiceModel>? models)
    {
        List<long>? measureIds = [];
        if (models is not null && models.Count > 0)
        {
            var measurementIds = models?.Where(x => x.UnitOfMeasurementId != null && x.UnitOfMeasurementId > 0).Select(c => (long)c.UnitOfMeasurementId!).Distinct().ToList();
            var servicemeasurementIds = models?.Where(x => x.ServiceInfoMeasureId != null && x.ServiceInfoMeasureId > 0)
                .Select(x => (long)x.ServiceInfoMeasureId!).Distinct().ToList();
            var detailServicemeasurementIds = models?.Where(x => x.ProjectOperationDetailServiceInfoMeasureId != null && x.ProjectOperationDetailServiceInfoMeasureId > 0)
                .Select(x => (long)x.ProjectOperationDetailServiceInfoMeasureId!).Distinct().ToList();
            measureIds.AddRange(measurementIds ?? []);
            measureIds.AddRange(servicemeasurementIds ?? []);
            measureIds.AddRange(detailServicemeasurementIds ?? []);
        }
        return measureIds;
    }

    private List<long>? GetMeasureunitIds(
        List<GetsDailyServiceInfoModel>? models)
    {
        List<long>? measureIds = [];
        if (models is not null && models.Count > 0)
        {
            var measurementIds = models?.Where(x => x.UnitOfMeasurementId != null && x.UnitOfMeasurementId > 0).Select(c => (long)c.UnitOfMeasurementId!).Distinct().ToList();
            measureIds.AddRange(measurementIds ?? []);
        }
        return measureIds;
    }

    private List<long>? GetContractorIds(
        List<GetsDailyProjectOperationServiceModel>? models)
    {
        List<long>? cIds = [];
        if (models is not null && models.Count > 0)
        {
            var contractorIds = models?.Where(c => c.ContractorId != null && c.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList();
            var detailContractorIds = models?.Where(c => c.ProjectOperationDetailContractorId != null && c.ProjectOperationDetailContractorId > 0).Select(x => (long)x.ProjectOperationDetailContractorId!).Distinct().ToList();
            cIds.AddRange(contractorIds ?? []);
            cIds.AddRange(detailContractorIds ?? []);
        }
        return cIds;
    }

    private async Task<Result> MessageSender(
        DailyProjectOperation dailyValue,
        ProjectOperationDetail projectOperationDetail,
        bool isUpdate, CT ct)
    {
        var responseGetChats = await _messengerChannelRepo.GetFltrChannel([projectOperationDetail.ProjectOperation.ProjectId],
                            [projectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId!],
                            MessengerMessageType.DailyOperation, ct);
        if (responseGetChats is not null && responseGetChats.Count > 0)
        {
            _logger.LogInformation("Start Send Telegram Message for ProjectOperationDetail ID: {ProjectOperationDetailId}", dailyValue.ProjectOperationDetail.Id);

            List<Guid> ids = new();
            if (dailyValue.DailyProjectOperationDocuments is not null && dailyValue.DailyProjectOperationDocuments.Any())
                foreach (var item in dailyValue.DailyProjectOperationDocuments)
                    ids.Add(Guid.Parse(item.Url));

            var currentUserId = _userProfileService.GetProfileInfo().UserId;
            var creator = "";
            var getUser = await _mediator.Send(new GetUserByIdQuery(currentUserId), ct);
            if (getUser.IsSuccess && getUser.Value is not null)
                creator = getUser.Value.FullName;

            var measurementId = projectOperationDetail.ProjectOperation.OperationInfo.UnitOfMeasurementId;
            var measurement = await WebServicesLogic.MeasurementDataReceiver([measurementId], _mediator, ct);
            var unit = measurement?.FirstOrDefault(x => x.Id == measurementId)?.Name ?? "";

            foreach (var item in responseGetChats)
            {
                var createDateShamsi = TimeCalculator.ConvertToShamsi(DateTime.Now);
                var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
                var createTime = TimeZoneInfo.ConvertTime(DateTime.Now, tehranTimeZone).ToString("HH:mm:ss");
                var startDateShamsi = TimeCalculator.ConvertToShamsi(dailyValue.StartDate);
                var resultUrl = ids != null ? string.Join(",", ids) : string.Empty;

                var message = await TelegramDailyModel(dailyValue, projectOperationDetail, isUpdate, unit, ct);

                await TelegramServicesLogic.SendMessage(item, message, ids, _mediator, _authorization, _messageSenderConfig, ct);

            }
        }
        return Result.Success();
    }

    private async Task<Result> SendMessageCreateDailyProjectOperation(
        CreateDailyProjectOperationRequest request,
        ProjectOperationDetail projectOperationDetail,
        DailyProjectOperation createResponse,
        bool isUpdate, CT ct)
    {
        var getTelegramChatsByProjectOperationDetailId = await _messengerChannelRepo.GetFltrChannel([projectOperationDetail.ProjectOperation.ProjectId],
                            [projectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId!],
                            MessengerMessageType.DailyOperation, ct);

        if (getTelegramChatsByProjectOperationDetailId is not null &&
            getTelegramChatsByProjectOperationDetailId.Count > 0)
        {
            List<Guid> ids = new();
            if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                foreach (var item in request.DocumentUrls)
                    ids.Add(Guid.Parse(item));

            var currentUserId = _userProfileService.GetProfileInfo().UserId;
            var creator = "";
            var getUser = await _mediator.Send(new GetUserByIdQuery(currentUserId), ct);
            if (getUser.IsSuccess && getUser.Value is not null)
            {
                creator = getUser.Value.FullName;
            }

            var measurementId = projectOperationDetail.ProjectOperation.OperationInfo.UnitOfMeasurementId;
            var unit = "";
            var measurement = await WebServicesLogic.MeasurementDataReceiver(new List<long>() { measurementId },
                _mediator, ct);
            unit = measurement?.FirstOrDefault(x => x.Id == measurementId)?.Name;

            foreach (var item in getTelegramChatsByProjectOperationDetailId)
            {
                var createDateShamsi = TimeCalculator.ConvertToShamsi(DateTime.Now);
                var startDateShamsi = TimeCalculator.ConvertToShamsi(request.StartDate);
                var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
                var createTime = TimeZoneInfo.ConvertTime(DateTime.Now, tehranTimeZone).ToString("HH:mm:ss");

                var resultUrl = request.DocumentUrls != null
                    ? string.Join(",", request.DocumentUrls)
                    : string.Empty;

                var message = await TelegramDailyModel(createResponse, projectOperationDetail, isUpdate, unit, ct);

                await TelegramServicesLogic.SendMessage(item, message, ids, _mediator, _authorization, _messageSenderConfig, ct);


            }
        }
        return Result.Success();
    }

    private async Task<Result> DailyProjectOperationMessageSender(
        UpdateDailyProjectOperationRequest request,
        DailyProjectOperation dailyValue,
        ProjectOperationDetail projectOperationDetail,
        bool isUpdate, CT ct)
    {
        var responseGetChats = await _messengerChannelRepo.GetFltrChannel([projectOperationDetail.ProjectOperation.ProjectId],
                            [projectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId!],
                            MessengerMessageType.DailyOperation, ct);
        if (responseGetChats is not null && responseGetChats.Count > 0)
        {
            _logger.LogInformation("Start Send Telegram Message for ProjectOperationDetail ID: {ProjectOperationDetailId}", dailyValue.ProjectOperationDetail.Id);

            List<Guid> ids = new();
            if (request.DocumentUrls is not null && request.DocumentUrls.Any())
                foreach (var item in request.DocumentUrls)
                    ids.Add(Guid.Parse(item));

            var currentUserId = _userProfileService.GetProfileInfo().UserId;
            var creator = "";
            var getUser = await _mediator.Send(new GetUserByIdQuery(currentUserId), ct);
            if (getUser.IsSuccess && getUser.Value is not null)
                creator = getUser.Value.FullName;

            var measurementId = projectOperationDetail.ProjectOperation.OperationInfo.UnitOfMeasurementId;
            var measurement = await WebServicesLogic.MeasurementDataReceiver([measurementId], _mediator, ct);
            var unit = measurement?.FirstOrDefault(x => x.Id == measurementId)?.Name ?? "";

            foreach (var item in responseGetChats)
            {
                var createDateShamsi = TimeCalculator.ConvertToShamsi(DateTime.Now);
                var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
                var createTime = TimeZoneInfo.ConvertTime(DateTime.Now, tehranTimeZone).ToString("HH:mm:ss");
                var startDateShamsi = TimeCalculator.ConvertToShamsi(request.StartDate);
                var resultUrl = request.DocumentUrls != null ? string.Join(",", request.DocumentUrls) : string.Empty;

                var message = await TelegramDailyModel(dailyValue, projectOperationDetail, isUpdate, unit, ct);

                await TelegramServicesLogic.SendMessage(item, message, ids, _mediator, _authorization, _messageSenderConfig, ct);

            }
        }
        return Result.Success();
    }

}
