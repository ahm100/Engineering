using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperationDependencies.Commands.RescheduleProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.SetPlannedDate;

namespace Engineering.Application.Services.ProjectOperations.Command.SetPlannedDate;

public class SetPlannedDateCommandHandler : ICommandHandler<SetPlannedDateCommand, SetPlannedDateResponse?>
{
    private readonly IProjectOperationRepository _repository;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SetPlannedDateCommandHandler> _logger;

    public SetPlannedDateCommandHandler(ILogger<SetPlannedDateCommandHandler> logger,
        IProjectOperationRepository repository,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _repository = repository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SetPlannedDateResponse?>> Handle(SetPlannedDateCommand request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationById(request.Id, ct);
            if (result is null)
                return Result.Failure<SetPlannedDateResponse>(ProjectOperationErrors.NotFound);

            result.SetPlannedSchedule(request.PlannedStartDate, request.PlannedFinishDate, request.PlannedDuration);

            await _repository.Update(result);

            var setSuccessors = await _mediator.Send(new RescheduleProjectOperationsCommand(result.Id), ct);
            if (setSuccessors.IsBad())
                return setSuccessors.Failure<SetPlannedDateResponse?>();

            return new SetPlannedDateResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<SetPlannedDateResponse?>(SharedErrors.UnknownError);
        }
    }
}