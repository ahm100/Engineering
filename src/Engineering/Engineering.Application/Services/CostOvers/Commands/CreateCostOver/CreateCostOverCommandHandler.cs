using Engineering.Application.Abstractions.Data.CostOvers;
using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Commands.CreateCostOver;

public class CreateCostOverCommandHandler : ICommandHandler<CreateCostOverCommand, CostOver?>
{
    private readonly ILogger<CreateCostOverCommand> _logger;
    private readonly ICostOverRepository _repository;

    public CreateCostOverCommandHandler(
        ILogger<CreateCostOverCommand> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostOver?>> Handle(CreateCostOverCommand request, CT ct)
    {
        try
        {
            var entity = new CostOver(
                request.CostOverName,
                request.CostOverCode,
                request.IsActive,
                request.CompanyId);

            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostOver?>(SharedErrors.UnknownError);
        }
    }
}