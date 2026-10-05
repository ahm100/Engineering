using Engineering.Application.Abstractions.Data.Transportations;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Commands.Create;

public class CreateTransportationCommandHandler : ICommandHandler<CreateTransportationCommand, Transportation?>
{
    private readonly ILogger<CreateTransportationCommand> _logger;
    private readonly ITransportationRepository _repository;

    public CreateTransportationCommandHandler(ILogger<CreateTransportationCommand> logger, ITransportationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Transportation?>> Handle(CreateTransportationCommand request, CT ct)
    {
        try
        {
            var entity = new Transportation(request.TransportationName, request.TransportationCode, request.IsPassenger, request.IsActive, request.CompanyId, request.TransportationType);
            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Transportation?>(SharedErrors.UnknownError);
        }
    }
}