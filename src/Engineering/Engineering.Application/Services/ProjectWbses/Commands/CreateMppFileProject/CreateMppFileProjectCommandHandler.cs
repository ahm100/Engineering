using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.Contracts.CreateMppFileProject;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateMppFileProject;

public class CreateMppFileProjectCommandHandler : ICommandHandler<CreateMppFileProjectCommand, CreateMppFileProjectResponse?>
{
    private readonly ILogger<CreateMppFileProjectCommandHandler> _logger;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectScheduleImportRepository _importRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateMppFileProjectCommandHandler(
        ILogger<CreateMppFileProjectCommandHandler> logger,
        IProjectRepository projectRepository,
        IProjectScheduleImportRepository importRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _projectRepository = projectRepository;
        _importRepository = importRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateMppFileProjectResponse?>> Handle(
        CreateMppFileProjectCommand request, CT ct)
    {
        try
        {
            var project = await _projectRepository.GetById(request.ProjectId, ct);
            if (project is null)
                return Result.Failure<CreateMppFileProjectResponse?>(ProjectErrors.ProjectNotFound);

            var existingImport = await _importRepository.GetByProjectId(request.ProjectId, ct);
            if (existingImport is not null)
            {
                return Result.Success(new CreateMppFileProjectResponse(existingImport.Id))!;
            }

            var import = ProjectScheduleImport.CreateManual(
                project,
                DateTime.UtcNow,
                request.UserId);

            await _importRepository.Create(import, ct);
            await _unitOfWork.CommitAsync(ct);
            return Result.Success(new CreateMppFileProjectResponse(import.Id))!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating manual project schedule for ProjectId: {ProjectId}", request.ProjectId);
            return Result.Failure<CreateMppFileProjectResponse?>(SharedErrors.UnknownError);
        }
    }
}