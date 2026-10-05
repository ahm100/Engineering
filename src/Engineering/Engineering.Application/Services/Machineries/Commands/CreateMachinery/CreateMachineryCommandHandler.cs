using Engineering.Application.Abstractions.Data.Machineries;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Commands.CreateMachinery;

public class CreateMachineryCommandHandler : ICommandHandler<CreateMachineryCommand, Machinery?>
{
    private readonly ILogger<CreateMachineryCommand> _logger;
    private readonly IMachineryRepository _repository;

    public CreateMachineryCommandHandler(ILogger<CreateMachineryCommand> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Machinery?>> Handle(CreateMachineryCommand request, CT ct)
    {
        try
        {
            var newMachinery = new Machinery(request.MachineriesGroup,
                request.MachineryName,
                request.MachineryCode,
                request.IsActive,
                request.CompanyId);
            var result = await _repository.Create(newMachinery, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Machinery?>(SharedErrors.UnknownError);
        }
    }
}