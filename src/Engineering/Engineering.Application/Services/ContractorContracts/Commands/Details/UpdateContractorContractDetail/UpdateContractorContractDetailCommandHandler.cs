using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.UpdateContractorContractDetail;

public class UpdateContractorContractDetailCommandHandler :
    ICommandHandler<UpdateContractorContractDetailCommand, ContractorContractDetail>
{
    private readonly ILogger<UpdateContractorContractDetailCommandHandler> _logger;
    private readonly IContractorContractDetailRepository _repository;

    public UpdateContractorContractDetailCommandHandler(
        ILogger<UpdateContractorContractDetailCommandHandler> logger,
        IContractorContractDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContractDetail?>> Handle(
        UpdateContractorContractDetailCommand request, CT ct)
    {
        try
        {
            var entity = request.ContractorContractDetail;

            if (request.ProjectOperation is not null)
                if (entity.ProjectOperationId != request.ProjectOperation.Id)
                    entity.SetProjectOperation(request.ProjectOperation);

            if (request.StartDate is not null)
                if (entity.StartDate != request.StartDate)
                    entity.SetStartDate(request.StartDate);

            if (request.EndDate is not null)
                if (entity.EndDate != request.EndDate)
                    entity.SetEndDate(request.EndDate);

            if (entity.ContractCoefficient != request.ContractCoefficient)
                entity.SetContractCoefficient(request.ContractCoefficient);

            if (request.WorkLoad is not null)
                if (entity.WorkLoad != request.WorkLoad)
                    entity.SetWorkLoad(request.WorkLoad!.Value);

            if (request.UnitAmount is not null)
                if (entity.UnitAmount != request.UnitAmount)
                    entity.SetUnitAmount(request.UnitAmount!.Value);

            entity.SetTotalAmount();

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
