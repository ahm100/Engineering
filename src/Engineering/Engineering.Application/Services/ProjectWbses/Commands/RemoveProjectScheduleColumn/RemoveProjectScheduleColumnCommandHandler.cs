using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectScheduleColumn;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectScheduleColumn;

public class RemoveProjectScheduleColumnCommandHandler : ICommandHandler<RemoveProjectScheduleColumnCommand, RemoveProjectScheduleColumnResponse>
{
    private readonly ILogger<RemoveProjectScheduleColumnCommandHandler> _logger;
    private readonly IProjectScheduleColumnRepository _repository;
    private readonly IProjectScheduleTaskValueRepository _valueRepository;

    public RemoveProjectScheduleColumnCommandHandler(
        ILogger<RemoveProjectScheduleColumnCommandHandler> logger,
        IProjectScheduleColumnRepository repository,
        IProjectScheduleTaskValueRepository valueRepository)
    {
        _logger = logger;
        _repository = repository;
        _valueRepository = valueRepository;
    }

    public async Task<Result<RemoveProjectScheduleColumnResponse>> Handle(
        RemoveProjectScheduleColumnCommand command, CT ct)
    {
        try
        {
            var column = await _repository.GetById(command.Id, ct);
            if (column is null)
                return Result.Failure<RemoveProjectScheduleColumnResponse>(SharedErrors.ItemNotFound)!;

            if (column.ColumnType != ProjectScheduleColumnType.Custom)
                return Result.Failure<RemoveProjectScheduleColumnResponse>(
                    ProjectErrors.SystemColumnCannotBeDeleted)!;

            var values = await _valueRepository.GetByColumnId(column.Id, ct);
            foreach (var value in values)
                value.SoftDelete();

            column.SoftDelete();

            var siblings = await _repository.GetByImportId(column.ProjectScheduleImportId, ct);
            var order = 1;
            foreach (var c in siblings.Where(c => c.Id != column.Id).OrderBy(c => c.SortOrder))
                c.SetSortOrder(order++);

            return new RemoveProjectScheduleColumnResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RemoveProjectScheduleColumnResponse>(SharedErrors.UnknownError)!;
        }
    }
}