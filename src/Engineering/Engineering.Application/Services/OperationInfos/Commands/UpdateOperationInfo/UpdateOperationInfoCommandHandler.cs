using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Commands.UpdateOperationInfo;

public class UpdateOperationInfoCommandHandler : ICommandHandler<UpdateOperationInfoCommand, OperationInfo>
{
    private readonly ILogger<UpdateOperationInfoCommand> _logger;
    private readonly IOperationInfoRepository _repository;

    public UpdateOperationInfoCommandHandler(ILogger<UpdateOperationInfoCommand> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(UpdateOperationInfoCommand request, CT ct)
    {
        try
        {
            var entity = request.OperationInfo;

            var oISeasons = entity.OperationInfoSeasons.ToList();
            var seasons = oISeasons.Select(x => x.Season).ToList();
            List<string> checkSeasons;
            List<string> checkBranchs = [];
            List<string> checkCategories = [];

            checkSeasons = oISeasons.Where(x => !x.IsDeleted).Select(x => x.Season).Where(x => !x.IsActive).Select(x => x.SeasonName).ToList();
            foreach (var season in seasons)
            {
                var oiSeason = oISeasons.FirstOrDefault(x => x.Season.Id == season.Id);
                if ((!season.Branch.IsActive || season.Branch.IsDeleted) && !oiSeason.IsDeleted)
                    checkBranchs.Add(season.Branch.BranchName);
                if ((!season.Branch.Category.IsActive || season.Branch.Category.IsDeleted) && !oiSeason.IsDeleted)
                    checkCategories.Add(season.Branch.Category.CategoryName);
            }
            var seasonNamesStr = string.Join(", ", checkSeasons.Distinct());
            var branchNamesStr = string.Join(", ", checkBranchs.Distinct());
            var categoryNamesStr = string.Join(", ", checkCategories.Distinct());

            if (checkCategories.Any() || checkBranchs.Any() || checkSeasons.Any())
                return Result.Failure<OperationInfo>(OperationInfoErrors.InvalidUpdateStatus(categoryNamesStr, branchNamesStr, seasonNamesStr));

            entity.SetName(request.OperationInfoName);
            entity.SetCode(request.OperationInfoCode);
            entity.SetLatinName(request.OperationInfoLatinName);
            entity.SetPriority(request.Priority);
            entity.SetCompanyId(request.CompanyId);
            entity.SetUnitOfMeasurement(request.UnitOfMeasurementId);
            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive == true)
                    entity.SetActive();
                else
                    entity.SetInActive();
            }

            if (request.OperationInfoCode != entity.OperationInfoCode ||
                request.OperationInfoName != entity.OperationInfoName ||
                request.OperationInfoLatinName != entity.OperationLatinName ||
                request.Priority != entity.Priority ||
                request.BasePrice != entity.BasePrice ||
                request.UnitOfMeasurementId != entity.UnitOfMeasurementId ||
                request.HasChanged)
                entity.SetHasChanged(true);

            if (request.BasePrice != null && (entity.BasePrice == 0 || entity.BasePrice == null))
                entity.SetBasePrice(request.BasePrice!.Value!);

            entity.SetStandard();
            entity.AddHistory();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}