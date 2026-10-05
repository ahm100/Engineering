using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.DeleteProjectOperationDetail;

public class DeleteProjectOperationDetailCommandHandler : ICommandHandler<DeleteProjectOperationDetailCommand, ProjectOperationDetail>
{
    private readonly ILogger<DeleteProjectOperationDetailCommand> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public DeleteProjectOperationDetailCommandHandler(ILogger<DeleteProjectOperationDetailCommand> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(DeleteProjectOperationDetailCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetProjectOperationDetailForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);

            if (entity.IsDeleted)
                return Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.IsDeleted);
            if (entity.DailyOperations.Any(x => x.FinalAmount > 0))
                return Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.CanNottDelete);
            if (entity.ProjectOperationDetailContractorServices.Any(x => x.ContractorContractDetailServices.Any()))
                return Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.CanNottDeleteForContract);
            if (entity.RequestGoodsSupplies.Count > 0)
                return Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.CanNottDeleteForGoods);
            if (entity.ConsumableVolumeProducts.Any())
            {
                if (entity.ConsumableVolumeProducts.Any(x => x.RequestGoodsSupplyDetails.Count > 0))
                    return Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.CanNottDeleteForGoodsDetails);
            }

            entity.SetIsDeleted();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
