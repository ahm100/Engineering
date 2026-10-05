
using Engineering.Application.Abstractions.Data.Transportations;

namespace Engineering.Application.Services.Transportations.Commands.CodeCreator;

public class TransportationCodeCreatorCommandHandler : ICommandHandler<TransportationCodeCreatorCommand, string?>
{
    private readonly ILogger<TransportationCodeCreatorCommand> _logger;
    private readonly ITransportationRepository _repository;

    public TransportationCodeCreatorCommandHandler(ILogger<TransportationCodeCreatorCommand> logger, ITransportationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<string?>> Handle(TransportationCodeCreatorCommand request, CT ct)
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