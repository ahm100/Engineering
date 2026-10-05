using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Commands.ChangeRequestMachineryStatus;

public class ChangeRequestMachineryStatusCommandHandler : ICommandHandler<ChangeRequestMachineryStatusCommand, RequestMachinery>
{
    private readonly ILogger<ChangeRequestMachineryStatusCommandHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public ChangeRequestMachineryStatusCommandHandler(ILogger<ChangeRequestMachineryStatusCommandHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachinery?>> Handle(ChangeRequestMachineryStatusCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;

            entity.ChangeStatus(request.Status);

            entity.AddHistory(request.Description);

            if (request.Status == RequestMachineryStatus.OnProject)
                entity.SetContractorId(request.ContractorId);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachinery>(SharedErrors.UnknownError);
        }
    }
}
