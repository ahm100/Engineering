using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleStartDate;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditProjectScheduleStartDate;

public class EditProjectScheduleStartDateCommandHandler : ICommandHandler<EditProjectScheduleStartDateCommand, EditProjectScheduleStartDateResponse?>
{
    private readonly ILogger<EditProjectScheduleStartDateCommandHandler> _logger;
    private readonly IProjectScheduleImportRepository _importRepository;

    public EditProjectScheduleStartDateCommandHandler(
        ILogger<EditProjectScheduleStartDateCommandHandler> logger,
        IProjectScheduleImportRepository importRepository)
    {
        _logger = logger;
        _importRepository = importRepository;
    }

    public async Task<Result<EditProjectScheduleStartDateResponse?>> Handle(
        EditProjectScheduleStartDateCommand request, CT ct)
    {
        try
        {
            var import = await _importRepository.GetByProjectId(request.ProjectId, ct);
            if (import is null)
                return Result.Failure<EditProjectScheduleStartDateResponse>(ProjectErrors.ProjectNotFound)!;
            import.SetScheduleStartDate(request.StartDate.Date);
            await _importRepository.Update(import);
            return new EditProjectScheduleStartDateResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EditProjectScheduleStartDateResponse>(SharedErrors.UnknownError)!;
        }
    }
}
