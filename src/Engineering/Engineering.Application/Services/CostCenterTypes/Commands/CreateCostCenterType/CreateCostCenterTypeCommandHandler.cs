using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterType = Engineering.Domain.Entities.CostCenters.CostCenterType;

namespace Engineering.Application.Services.CostCenterTypes.Commands.CreateCostCenterType;

public class CreateCostCenterTypeCommandHandler : ICommandHandler<CreateCostCenterTypeCommand, CostCenterType>
{
    private readonly ILogger<CreateCostCenterTypeCommand> _logger;
    private readonly ICostCenterTypeRepository _repository;

    public CreateCostCenterTypeCommandHandler(
        ILogger<CreateCostCenterTypeCommand> logger,
        ICostCenterTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterType?>> Handle(CreateCostCenterTypeCommand request, CT ct)
    {
        try
        {
            var newCostCenterType = new CostCenterType(
                request.CostCenterTypeName,
                request.CostCenterTypeCode,
                request.IsActive,
                request.CompanyId);
            var result = await _repository.Create(newCostCenterType, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterType>(SharedErrors.UnknownError);
        }
    }
}