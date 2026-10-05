using Engineering.Application.Abstractions.Data.MachineTypes;

namespace Engineering.Application.Services.CabinTypes.Commands.CabinTypeCodeCreator;

public class CabinTypeCodeCreatorCommandHandler : ICommandHandler<CabinTypeCodeCreatorCommand, int?>
{
    private readonly ILogger<CabinTypeCodeCreatorCommand> _logger;
    private readonly ICabinTypeRepository _repository;

    public CabinTypeCodeCreatorCommandHandler(
        ILogger<CabinTypeCodeCreatorCommand> logger,
        ICabinTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<int?>> Handle(CabinTypeCodeCreatorCommand request, CT ct)
    {
        try
        {
            var result = await _repository.CabinTypeCodeCreator(request.CompanyId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<int?>(SharedErrors.UnknownError);
        }
    }
}