using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationCargos.Commands.AddTransportationCargoBill;

public class AddTransportationCargoBillCommandHandler : ICommandHandler<AddTransportationCargoBillCommand, TransportationCargo>
{
    private readonly ILogger<AddTransportationCargoBillCommand> _logger;
    private readonly ITransportationCargoRepository _repository;

    public AddTransportationCargoBillCommandHandler(
        ILogger<AddTransportationCargoBillCommand> logger,
        ITransportationCargoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationCargo?>> Handle(AddTransportationCargoBillCommand cargo, CT ct)
    {
        try
        {
            var entity = cargo.TransportationCargo;
            if (entity is null)
                return Result.Failure<TransportationCargo>(TransportationRequestErrors.CargosNotfound);

            entity.AddCargoDocuments(cargo.DocumentUrls);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<TransportationCargo>(SharedErrors.UnknownError);
        }
    }
}