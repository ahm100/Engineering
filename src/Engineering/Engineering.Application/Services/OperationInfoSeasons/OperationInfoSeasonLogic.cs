using Engineering.Application.Extensions.ServiceModeling;
using Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByIdsForSeason;
using Engineering.Application.Services.OperationInfoSeasons.Commands.CreateOperationInfoSeason;
using Engineering.Application.Services.OperationInfoSeasons.Commands.DeleteOperationInfoSeasonById;
using Engineering.Application.Services.OperationInfoSeasons.Models.CreateOperationInfoSeason;
using Engineering.Application.Services.OperationInfoSeasons.Models.CreateOperationInfoSeasonFormGoodsSupply;
using Engineering.Application.Services.OperationInfoSeasons.Models.GetsByOperationInfoId;
using Engineering.Application.Services.OperationInfoSeasons.Models.GetsOperationInfoSeasonByProjectOperationId;
using Engineering.Application.Services.OperationInfoSeasons.Queries.GetsByOperationInfoId;
using Engineering.Application.Services.OperationInfoSeasons.Queries.GetsOperationInfoSeasonByProjectOperationId;
using Engineering.Application.Services.Seasons.Queries.GetBySeasonIdsIncludeLess;
using Engineering.Application.Services.Seasons.Queries.GetSeasonById;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.Services.OperationInfoSeasons;

public class OperationInfoSeasonLogic : IOperationInfoSeasonLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<OperationInfoSeasonLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public OperationInfoSeasonLogic(
        IMediator mediator,
        ILogger<OperationInfoSeasonLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateOperationInfoSeasonResponse?>> CreateOperationInfoSeason(
        CreateOperationInfoSeasonModelRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateOperationInfoSeason, OperationInfoIds:{OperationInfoId}, SeasonsId:{SeasonsId},", request.OperationInfoIds, request.SeasonIds);

        bool hasCreated = false;
        List<OperationInfo>? operationInfos = null;
        if (request.OperationInfos is not null && request.OperationInfos.Any())
            operationInfos = request.OperationInfos;
        else if (request.OperationInfoIds is not null && request.OperationInfoIds.Any())
        {
            var operationInfoResponse = await _mediator.Send(new GetsOperationInfoByIdsForSeasonQuery(request.OperationInfoIds, 0, 0), ct);
            if (operationInfoResponse.IsFailure)
                return Result.Failure<CreateOperationInfoSeasonResponse>(operationInfoResponse.Error!);
            if (operationInfoResponse.Value is null)
                return Result.Failure<CreateOperationInfoSeasonResponse>(operationInfoResponse.Error!);
            operationInfos = operationInfoResponse.Value.Data;
        }
        else
            return Result.Failure<CreateOperationInfoSeasonResponse>(OperationInfoErrors.IdIsEmpty!);

        if (request.DeletedOperationInfoSeasonIds is not null && request.DeletedOperationInfoSeasonIds.Count > 0)
            foreach (var deletedId in request.DeletedOperationInfoSeasonIds)
            {
                var deleteSeason = await _mediator.Send(new DeleteOperationInfoSeasonByIdCommand(deletedId), ct);
                if (deleteSeason.IsFailure)
                    return Result.Failure<CreateOperationInfoSeasonResponse>(deleteSeason.Error!);
            }

        if (request.SeasonIds is not null && request.SeasonIds.Count > 0)
        {
            var seasonsData = await _mediator.Send(new GetBySeasonIdsIncludeLessQuery(request.SeasonIds!, 1, request.SeasonIds!.Count), ct); // بره سراغ متا دیتا
            if (seasonsData.IsFailure)
                return Result.Failure<CreateOperationInfoSeasonResponse>(OperationInfoSeasonErrors.UnValidSeasons);
            var seasons = seasonsData.Value!.Data;

            if (seasons is not null && seasons.Count > 0)
                if (operationInfos != null && operationInfos.Any())
                    foreach (var operationInfo in operationInfos)
                    {
                        List<Season>? creativeSeasons = new();
                        foreach (var season in seasons)
                            if (!operationInfo.OperationInfoSeasons.Any(s => s.Season.Id.Equals(season.Id)))
                                creativeSeasons?.Add(season);

                        if (creativeSeasons != null && creativeSeasons.Count > 0)
                        {
                            hasCreated = true;
                            var response = await _mediator.Send(new CreateOperationInfoSeasonCommand(operationInfo, seasonsData.Value!.Data!), ct);
                            if (response.IsFailure)
                                return Result.Failure<CreateOperationInfoSeasonResponse>(response.Error!);
                        }
                    }
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateOperationInfoSeasonResponse(true, hasCreated);
    }

    public async Task<Result<CreateOperationInfoSeasonFormGoodsSupplyResponse?>> CreateOperationInfoSeasonFormGoodsSupply(
        CreateOperationInfoSeasonFormGoodsSupplyRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateOperationInfoSeasonFormGoodsSupply");

        OperationInfoSeason? operationInfoSeason = null;
        if (request.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Id.Equals(request.SeasonId)))
            operationInfoSeason = request.OperationInfo.OperationInfoSeasons.FirstOrDefault(x => x.Season.Id.Equals(request.SeasonId));
        else
        {
            var seasonsData = await _mediator.Send(new GetSeasonByIdQuery(request.SeasonId), ct); // بره سراغ متا دیتا
            if (seasonsData.IsFailure)
                return Result.Failure<CreateOperationInfoSeasonFormGoodsSupplyResponse>(OperationInfoSeasonErrors.UnValidSeasons);
            var season = seasonsData.Value!;

            var response = await _mediator.Send(new CreateOperationInfoSeasonCommand(request.OperationInfo, [season]), ct);
            if (response.IsFailure)
                return Result.Failure<CreateOperationInfoSeasonFormGoodsSupplyResponse>(response.Error!);
            operationInfoSeason = response.Value!.FirstOrDefault();
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateOperationInfoSeasonFormGoodsSupplyResponse(operationInfoSeason!);
    }

    public async Task<Result<GetsOperationInfoSeasonByIdResponse?>> GetsByOperationInfoId(
        GetsOperationInfoSeasonByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByOperationInfoId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsOperationInfoSeasonByIdValidator, GetsOperationInfoSeasonByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsOperationInfoSeasonByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsByOperationInfoIdQuery(request.OprationInfoId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsOperationInfoSeasonByIdResponse>(response.Error!);

        var data = DataModeling.GetsCategoryBranchSeasonModeling(response.Value?.Data?.ToList());
        return new GetsOperationInfoSeasonByIdResponse(data?.CategoryData, data?.BranchData, data?.SeasonData);
    }

    public async Task<Result<GetsOperationInfoSeasonByProjectOperationIdResponse?>> GetsOperationInfoSeasonByProjectOperationId(
        GetsOperationInfoSeasonByProjectOperationIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsOperationInfoSeasonByProjectOperationId, pageIndex:{PageIndex} , pageSize:{PageSize}", 0, 0);

        var response = await _mediator.Send(new GetsOperationInfoSeasonByProjectOperationIdQuery(request.ProjectOperationId, 0, 0), ct);
        if (response.IsFailure)
            return Result.Failure<GetsOperationInfoSeasonByProjectOperationIdResponse>(response.Error!);

        var data = DataModeling.GetsCategoryBranchSeasonModeling(response.Value?.Data?.ToList());
        return new GetsOperationInfoSeasonByProjectOperationIdResponse(data?.CategoryData, data?.BranchData, data?.SeasonData);
    }

}