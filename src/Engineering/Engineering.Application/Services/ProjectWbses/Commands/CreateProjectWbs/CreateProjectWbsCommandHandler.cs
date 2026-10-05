using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectWbs;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateProjectWbs;

public class CreateProjectWbsCommandHandler : ICommandHandler<CreateProjectWbsCommand, CreateProjectWbsResponse?>
{
    private readonly ILogger<CreateProjectWbsCommandHandler> _logger;
    private readonly IProjectWbsRepository _wbsRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectScheduleImportRepository _importRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateProjectWbsCommandHandler(
        ILogger<CreateProjectWbsCommandHandler> logger,
        IProjectWbsRepository wbsRepository,
        IProjectRepository projectRepository,
        IProjectScheduleImportRepository importRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _wbsRepository = wbsRepository;
        _projectRepository = projectRepository;
        _importRepository = importRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateProjectWbsResponse?>> Handle(CreateProjectWbsCommand request, CT ct)
    {
        var project = await _projectRepository.GetById(request.ProjectId, ct);
        if (project is null)
            return Result.Failure<CreateProjectWbsResponse>(ProjectErrors.ProjectNotFound)!;

        var import = await _importRepository.GetById(request.ProjectScheduleImportId, ct);
        if (import is null)
            return Result.Failure<CreateProjectWbsResponse>(ProjectErrors.ProjectTaskNotFound)!;

        ProjectWbs? parentWbs = null;
        if (request.ParentWbsId.HasValue)
        {
            parentWbs = await _wbsRepository.GetById(request.ParentWbsId.Value, ct);
            if (parentWbs is null)
                return Result.Failure<CreateProjectWbsResponse>(ProjectErrors.ProjectTaskNotFound)!;
        }

        var wbs = new ProjectWbs(
            project,
            parentWbs,
            import,
            titleFa: request.TitleFa,
            titleEn: null,
            code: request.Code,
            descriptionFa: null,
            descriptionEn: null,
            isActive: true);

        await _wbsRepository.Create(wbs, ct);
        await _unitOfWork.CommitAsync(ct);
        return new CreateProjectWbsResponse(wbs.Id);
    }
}