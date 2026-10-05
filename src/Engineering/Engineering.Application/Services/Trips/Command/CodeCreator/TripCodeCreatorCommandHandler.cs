using Engineering.Application.Abstractions.Data.Trips;

namespace Engineering.Application.Services.Trips.Commands.CodeCreator;

public class TripCodeCreatorCommandHandler : ICommandHandler<TripCodeCreatorCommand, string?>
{
    private readonly ILogger<TripCodeCreatorCommand> _logger;
    private readonly ITripRepository _repository;

    public TripCodeCreatorCommandHandler(ILogger<TripCodeCreatorCommand> logger, ITripRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<string?>> Handle(TripCodeCreatorCommand request, CT ct)
    {
        try
        {
            var result = await _repository.CodeCreator(request.CompanyId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<string?>(SharedErrors.UnknownError);
        }
    }
}