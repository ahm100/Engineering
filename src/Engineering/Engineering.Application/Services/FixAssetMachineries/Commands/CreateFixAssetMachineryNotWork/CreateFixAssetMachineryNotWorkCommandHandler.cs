using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachineryNotWork = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryNotWork;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachineryNotWork;

public class CreateFixAssetMachineryNotWorkCommandHandler : ICommandHandler<CreateFixAssetMachineryNotWorkCommand, FixAssetMachineryNotWork?>
{
    private readonly ILogger<CreateFixAssetMachineryNotWorkCommand> _logger;
    private readonly IFixAssetMachineryNotWorkRepository _repository;

    public CreateFixAssetMachineryNotWorkCommandHandler(ILogger<CreateFixAssetMachineryNotWorkCommand> logger, IFixAssetMachineryNotWorkRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetMachineryNotWork?>> Handle(CreateFixAssetMachineryNotWorkCommand request, CT ct)
    {
        try
        {
            var newFixAssetMachinery = new FixAssetMachineryNotWork(request.Description, request.StartDate, request.EndDate, request.FixAssetMachinery);
            var result = await _repository.Create(newFixAssetMachinery, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FixAssetMachineryNotWork?>(SharedErrors.UnknownError);
        }
    }
}