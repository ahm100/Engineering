using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateMachineDriver;

public class UpdateMachineDriverCommandHandler : ICommandHandler<UpdateMachineDriverCommand, TransportationRequest>
{
    private readonly ILogger<UpdateMachineDriverCommand> _logger;
    private readonly ITransportationRequestRepository _repository;

    public UpdateMachineDriverCommandHandler(
        ILogger<UpdateMachineDriverCommand> logger,
        ITransportationRequestRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TransportationRequest?>> Handle(UpdateMachineDriverCommand request, CT ct)
    {
        try
        {
            var entity = request.TransportationRequest;

            entity.SetMachine(request.MachineType);
            entity.SetDriverId(request.DriverId);
            entity.SetDriverName(request.Driver);
            entity.SetNumberPlates(request.NumberPlate);
            entity.SetCertificateNumbers(request.CertificateNumber);

            entity.AddHistory();

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<TransportationRequest>(SharedErrors.UnknownError);
        }
    }
}