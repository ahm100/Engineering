using Engineering.Application.Abstractions.Data.ProjectOperationDetails;

namespace Engineering.Application.Services.DetailContractorServices.Commands.StateChangerDetailContractorServices;

public class StateChangerDetailContractorServicesCommandHandler : ICommandHandler<StateChangerDetailContractorServicesCommand, bool?>
{
    private readonly ILogger<StateChangerDetailContractorServicesCommand> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public StateChangerDetailContractorServicesCommandHandler(ILogger<StateChangerDetailContractorServicesCommand> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerDetailContractorServicesCommand request, CT ct)
    {
        try
        {
            if (request.State)
                foreach (var item in request.Items)
                {
                    if (item.IsActive != request.State)
                    {
                        item.SetActive();
                        await _repository.Update(item);
                    }
                }
            else
                foreach (var item in request.Items)
                {
                    if (item.IsActive != request.State)
                    {
                        item.SetInActive();
                        await _repository.Update(item);
                    }
                }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}