using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectSchedule;
using Engineering.Domain.Entities.Projects.ProjectCalendars;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateProjectSchedule;

public class CreateProjectScheduleCommandHandler : ICommandHandler<CreateProjectScheduleCommand, CreateProjectScheduleResponse?>
{
    private readonly ILogger<CreateProjectScheduleCommandHandler> _logger;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectScheduleImportRepository _importRepository;
    private readonly IProjectCalendarRepository _calendarRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectScheduleCommandHandler(
        ILogger<CreateProjectScheduleCommandHandler> logger,
        IProjectRepository projectRepository,
        IProjectScheduleImportRepository importRepository,
        IProjectCalendarRepository calendarRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _projectRepository = projectRepository;
        _importRepository = importRepository;
        _calendarRepository = calendarRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateProjectScheduleResponse?>> Handle(
        CreateProjectScheduleCommand request, CT ct)
    {
        try
        {
            var project = await _projectRepository.GetProjectByIdIncludeLess(request.ProjectId, ct);
            if (project is null)
                return Result.Failure<CreateProjectScheduleResponse>(ProjectErrors.ProjectNotFound)!;

            var existing = await _importRepository.GetByProjectId(request.ProjectId, ct);
            if (existing is not null)
                return new CreateProjectScheduleResponse(existing.Id, existing.ScheduleStartDate ?? existing.Created);

            var import = ProjectScheduleImport.CreateManual(
                project,
                request.ScheduleStartDate,
                createdBy: request.UserId);

            await _importRepository.Create(import, ct);

            var calendars = await _calendarRepository.GetByProjectId(request.ProjectId, ct) ?? [];
            if (calendars.Count == 0)
                await _calendarRepository.Create(ProjectCalendar.CreateStandard(project, "تقویم استاندارد", true), ct);

            await _unitOfWork.CommitAsync(ct);

            return new CreateProjectScheduleResponse(import.Id, request.ScheduleStartDate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CreateProjectScheduleResponse>(SharedErrors.UnknownError)!;
        }
    }
}