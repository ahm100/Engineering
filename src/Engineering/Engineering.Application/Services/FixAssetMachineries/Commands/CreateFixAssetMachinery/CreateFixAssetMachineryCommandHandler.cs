using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachinery;

public class CreateFixAssetMachineryCommandHandler : ICommandHandler<CreateFixAssetMachineryCommand, FixAssetMachinery?>
{
    private readonly ILogger<CreateFixAssetMachineryCommand> _logger;
    private readonly IFixAssetMachineryRepository _repository;

    public CreateFixAssetMachineryCommandHandler(ILogger<CreateFixAssetMachineryCommand> logger, IFixAssetMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetMachinery?>> Handle(CreateFixAssetMachineryCommand request, CT ct)
    {
        try
        {
            var newFixAssetMachinery = new FixAssetMachinery(
                request.MachinerySpecification,
                request.NumberPlates,
                request.FixAssetMachineryType,
                request.MachineryPrice,
                request.IsActive,
                request.ContractorId,
                request.DriverId,
                request.DriverName,
                request.StartDate,
                request.EndDate,
                request.Description,
                request.CompanyId,
                request.Machinery);
            var result = await _repository.Create(newFixAssetMachinery, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FixAssetMachinery?>(SharedErrors.UnknownError);
        }
    }
}