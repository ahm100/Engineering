using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.DeleteContractorContract;

public class DeleteContractorContractCommandHandler : ICommandHandler<DeleteContractorContractCommand, ContractorContract>
{
    private ILogger<DeleteContractorContractCommandHandler> _logger;
    private IContractorContractRepository _repository;

    public DeleteContractorContractCommandHandler(
        ILogger<DeleteContractorContractCommandHandler> logger,
        IContractorContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContract?>> Handle(DeleteContractorContractCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetContractorContractForDelete(request.Id, request.CompanyId, ct);
            if (entity is null)
                return Result.Failure<ContractorContract>(ContractorContractErrors.InValidProjectOperationServiceId);

            entity.SoftDelete();

            foreach (var detail in entity.Details)
            {
                detail.SoftDelete();
                foreach (var service in detail.ContractorContractDetailServices)
                    service.SetIsDeleted();
            }

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContract>(SharedErrors.UnknownError);
        }
    }
}
