using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.Commands.ReSchheduledProjectSchedule;

namespace Engineering.Application.Services.ProjectWbses.Commands.ReScheduleProjectSchedule;

public class ReScheduleProjectScheduleCommandHandler
    : IRequestHandler<ReSchheduledProjectScheduleCommand, Result<bool>>
{
    private readonly ILogger<ReScheduleProjectScheduleCommandHandler> _logger;
    private readonly IProjectScheduleImportRepository _importRepository;

    public ReScheduleProjectScheduleCommandHandler(
        ILogger<ReScheduleProjectScheduleCommandHandler> logger,
        IProjectScheduleImportRepository importRepository)
    {
        _logger = logger;
        _importRepository = importRepository;
    }

    public async Task<Result<bool>> Handle(
        ReSchheduledProjectScheduleCommand request, CancellationToken ct)
    {
        try
        {
            var import = await _importRepository.GetById(request.Id, ct);
            if (import is null)
                return Result.Failure<bool>(ProjectErrors.ProjectNotFound)!;

            import.SetStatusDate(request.DateTime);
            import.SetRescheduleFromDate(request.DateTime);
            await _importRepository.Update(import);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError)!;
        }
    }
}