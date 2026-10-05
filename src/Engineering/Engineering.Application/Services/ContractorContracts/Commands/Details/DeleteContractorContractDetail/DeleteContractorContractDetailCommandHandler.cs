using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetail;

public class DeleteContractorContractDetailCommandHandler : ICommandHandler<DeleteContractorContractDetailCommand, ContractorContractDetail>
{
    private readonly ILogger<DeleteContractorContractDetailCommandHandler> _logger;
    private readonly IContractorContractDetailRepository _repository;

    public DeleteContractorContractDetailCommandHandler(
        ILogger<DeleteContractorContractDetailCommandHandler> logger,
        IContractorContractDetailRepository repostiory)
    {
        _logger = logger;
        _repository = repostiory;
    }

    public async Task<Result<ContractorContractDetail?>> Handle(DeleteContractorContractDetailCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetContractorContractDetailById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ContractorContractDetail>(ContractorContractDetailErrors.ContractorContractDetailWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<ContractorContractDetail>(ContractorContractDetailErrors.IsDeleted);
            if (entity.ContractorStatusStatementServices.Any(s => !CSSStatusRules.AllowForDelete.Contains(s.ContractorStatusStatementDetail.ContractorStatusStatement.Status)))
                return Result.Failure<ContractorContractDetail>(ContractorContractDetailErrors.ContractorContractDetailHaveCSS);

            entity.SoftDelete();

            foreach (var price in entity.ContractorContractDetailPrices)
            {
                price.SoftDelete();
                price.AddHistory();
            }

            foreach (var service in entity.ContractorContractDetailServices)
                service.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContractDetail>(SharedErrors.UnknownError);
        }
    }
}
