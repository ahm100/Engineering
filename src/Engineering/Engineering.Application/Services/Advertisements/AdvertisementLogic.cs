using Engineering.Application.Services.Advertisements.Commands.ChangeAdvertisementState;
using Engineering.Application.Services.Advertisements.Commands.CreateAdvertisement;
using Engineering.Application.Services.Advertisements.Commands.DeleteAdvertisement;
using Engineering.Application.Services.Advertisements.Commands.UpdateAdvertisement;
using Engineering.Application.Services.Advertisements.Contracts.ChangeAdvertisementState;
using Engineering.Application.Services.Advertisements.Contracts.CreateAdvertisement;
using Engineering.Application.Services.Advertisements.Contracts.DeleteAdvertisement;
using Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementById;
using Engineering.Application.Services.Advertisements.Contracts.GetFltrAdvertisement;
using Engineering.Application.Services.Advertisements.Contracts.UpdateAdvertisement;
using Engineering.Application.Services.Advertisements.Queries.GetAdvertisementById;
using Engineering.Application.Services.Advertisements.Queries.GetFltrAdvertisement;

namespace Engineering.Application.Services.Advertisements;

public class AdvertisementLogic : IAdvertisementLogic
{
    private IMediator _mediator;
    private ILogger<AdvertisementLogic> _logger;
    private IUnitOfWork _unitOfWork;

    public AdvertisementLogic(
        IMediator mediator,
        ILogger<AdvertisementLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateAdvertisementResponse?>> CreateAdvertisement(
        CreateAdvertisementRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateAdvertisement");
        var create = await _mediator.Send(new CreateAdvertisementCommand(
            request.TitleFa,
            request.TitleEn,
            request.DescriptionFa,
            request.DescriptionEn,
            request.TechnicalCode,
            request.DocumentUrls,
            request.IsActive), ct);
        if (create.IsBad())
            return create.Failure<CreateAdvertisementResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new CreateAdvertisementResponse(create.Value!.Id, true);
    }

    public async Task<Result<UpdateAdvertisementResponse?>> UpdateAdvertisement(
        UpdateAdvertisementRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateAdvertisement");
        var update = await _mediator.Send(new UpdateAdvertisementCommand(
            request.Id,
            request.TitleFa,
            request.TitleEn,
            request.DescriptionFa,
            request.DescriptionEn,
            request.TechnicalCode,
            request.DocumentUrls,
            request.IsActive), ct);
        if (update.IsBad())
            return update.Failure<UpdateAdvertisementResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return update;
    }

    public async Task<Result<DeleteAdvertisementResponse?>> DeleteAdvertisement(
        DeleteAdvertisementRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteAdvertisement");
        var delete = await _mediator.Send(new DeleteAdvertisementCommand(
            request.Id), ct);
        if (delete.IsBad())
            return delete.Failure<DeleteAdvertisementResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return delete;
    }

    public async Task<Result<ChangeAdvertisementStateResponse?>> ChangeAdvertisementState(
        ChangeAdvertisementStateRequest request, CT ct)
    {
        _logger.LogInformation("Request for ChangeAdvertisementState");
        var delete = await _mediator.Send(new ChangeAdvertisementStateCommand(
            request.Ids,
            request.IsActive), ct);
        if (delete.IsBad())
            return delete.Failure<ChangeAdvertisementStateResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return delete;
    }

    public async Task<Result<GetAdvertisementByIdResponse?>> GetAdvertisementById(
        GetAdvertisementByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetAdvertisementById");
        var result = await _mediator.Send(new GetAdvertisementByIdQuery(
            request.Id), ct);
        if (result.IsBad())
            return result.Failure<GetAdvertisementByIdResponse>()!;

        return result;
    }

    public async Task<Result<GetFltrAdvertisementResponse?>> GetFltrAdvertisement(
        GetFltrAdvertisementRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFltrAdvertisement");
        var result = await _mediator.Send(new GetFltrAdvertisementQuery(
            request.FilterData,
            request.IsActive,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad())
            return result.Failure<GetFltrAdvertisementResponse>()!;

        return result;
    }
}