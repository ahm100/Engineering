using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectScheduleColumn;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateProjectScheduleColumn;

public class CreateProjectScheduleColumnCommandHandler : ICommandHandler<CreateProjectScheduleColumnCommand, CreateProjectScheduleColumnResponse?>
{
    private readonly ILogger<CreateProjectScheduleColumnCommandHandler> _logger;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectScheduleImportRepository _importRepository;
    private readonly IProjectScheduleColumnRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectScheduleColumnCommandHandler(
        ILogger<CreateProjectScheduleColumnCommandHandler> logger,
        IProjectRepository projectRepository,
        IProjectScheduleImportRepository importRepository,
        IProjectScheduleColumnRepository repository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _projectRepository = projectRepository;
        _importRepository = importRepository;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateProjectScheduleColumnResponse?>> Handle(
        CreateProjectScheduleColumnCommand command, CT ct)
    {
        try
        {
            var title = command.Title?.Trim();
            if (string.IsNullOrWhiteSpace(title))
                return Result.Failure<CreateProjectScheduleColumnResponse>(ProjectErrors.ColumnTitleRequired)!;

            var titleEn = string.IsNullOrWhiteSpace(command.TitleEn) ? null : command.TitleEn.Trim();

            var project = await _projectRepository.GetById(command.ProjectId, ct);
            if (project is null)
                return Result.Failure<CreateProjectScheduleColumnResponse>(ProjectErrors.ProjectNotFound)!;

            var import = await _importRepository.GetByProjectId(command.ProjectId, ct);
            if (import is null)
                return Result.Failure<CreateProjectScheduleColumnResponse>(ProjectErrors.ProjectNotFound)!;

            var columns = await _repository.GetByImportId(import.Id, ct);

            if (columns.Any(c => string.Equals(c.TitleFa, title, StringComparison.OrdinalIgnoreCase)))
                return Result.Failure<CreateProjectScheduleColumnResponse>(ProjectErrors.ColumnTitleDuplicate)!;

            if (titleEn is not null &&
                columns.Any(c => string.Equals(c.TitleEn, titleEn, StringComparison.OrdinalIgnoreCase)))
                return Result.Failure<CreateProjectScheduleColumnResponse>(ProjectErrors.ColumnTitleDuplicate)!;

            if (columns.Any(c => string.Equals(c.TitleFa, title, StringComparison.OrdinalIgnoreCase)))
                return Result.Failure<CreateProjectScheduleColumnResponse>(ProjectErrors.ColumnTitleDuplicate)!;

            if (titleEn is not null &&
                columns.Any(c => string.Equals(c.TitleEn, titleEn, StringComparison.OrdinalIgnoreCase)))
                return Result.Failure<CreateProjectScheduleColumnResponse>(ProjectErrors.ColumnTitleDuplicate)!;


            int sortOrder;
            if (command.TargetColumnId is null)
            {
                sortOrder = columns.Count == 0 ? 1 : columns.Max(c => c.SortOrder) + 1;
            }
            else
            {
                var target = columns.FirstOrDefault(c => c.Id == command.TargetColumnId.Value);
                if (target is null)
                    return Result.Failure<CreateProjectScheduleColumnResponse>(ProjectErrors.ColumnNotFound)!;

                sortOrder = target.SortOrder;
                foreach (var c in columns.Where(c => c.SortOrder >= sortOrder))
                    c.SetSortOrder(c.SortOrder + 1);
            }

            var column = new ProjectScheduleColumn(
                import,
                ProjectScheduleColumnType.Custom,
                title,
                titleEn,
                sortOrder,
                command.DataType);

            
            await _repository.Create(column, ct);
            await _unitOfWork.CommitAsync(ct);
            return new CreateProjectScheduleColumnResponse(column.Id, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CreateProjectScheduleColumnResponse>(SharedErrors.UnknownError)!;
        }
    }
}
