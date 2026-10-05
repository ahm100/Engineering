using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetailService;

public class DeleteContractorContractDetailServiceCommandHandler : ICommandHandler<DeleteContractorContractDetailServiceCommand, ContractorContractDetailService>
{
    private readonly ILogger<DeleteContractorContractDetailServiceCommandHandler> _logger;
    private readonly IContractorContractDetailServiceRepository _repository;
    private readonly IProjectOperationDetailContractorExpertRepository _detailRepo;

    public DeleteContractorContractDetailServiceCommandHandler(
        ILogger<DeleteContractorContractDetailServiceCommandHandler> logger,
        IContractorContractDetailServiceRepository repostiory,
        IProjectOperationDetailContractorExpertRepository detailRepo)
    {
        _logger = logger;
        _repository = repostiory;
        _detailRepo = detailRepo;
    }

    public async Task<Result<ContractorContractDetailService?>> Handle(DeleteContractorContractDetailServiceCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetContractorContractDetailServiceById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ContractorContractDetailService>(ContractorContractDetailErrors.ContractorContractDetailWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<ContractorContractDetailService>(ContractorContractDetailErrors.IsDeleted);
            if (entity.ProjectOperationDetailContractorService.DailyOperationServices.Any(x => x.ContractorStatusStatementServiceDailies.Any()))
                return Result.Failure<ContractorContractDetailService>(ContractorContractDetailErrors.ContractorContractDetailServiceHaveCSS);
            if (entity.ContractorContractDetail.ContractorContractDetailServices.Count(x => x.Id != request.Id) == 0)
                return Result.Failure<ContractorContractDetailService>(ContractorContractDetailErrors.CanNotDeleteContractorContractDetailService);

            var details = await _detailRepo.GetByProjectOperationDetailContractorServiceId(entity.ProjectOperationDetailContractorServiceId, ct);

            entity.SetIsDeleted();

            if (details is not null && details.Count > 0)
                foreach (var item in details)
                    if (!item.ProjectOperationDetailContractorService.ContractorContractDetailServices.Any(x => !x.IsDeleted))
                    {
                        item.SetHaveContract(false);
                        await _detailRepo.Update(item);
                    }

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContractDetailService>(SharedErrors.UnknownError);
        }
    }
}
