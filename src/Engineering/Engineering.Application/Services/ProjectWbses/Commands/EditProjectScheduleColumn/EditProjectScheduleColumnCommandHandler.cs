using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleColumn;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditProjectScheduleColumn;

public class EditProjectScheduleColumnCommandHandler : ICommandHandler<EditProjectScheduleColumnCommand, EditProjectScheduleColumnResponse?>
{
    private readonly ILogger<EditProjectScheduleColumnCommandHandler> _logger;
    private readonly IProjectScheduleColumnRepository _repository;
    private readonly IProjectScheduleTaskValueRepository _valueRepository;

    public EditProjectScheduleColumnCommandHandler(
        ILogger<EditProjectScheduleColumnCommandHandler> logger,
        IProjectScheduleColumnRepository repository,
        IProjectScheduleTaskValueRepository valuerepository)
    {
        _logger = logger;
        _repository = repository;
        _valueRepository = valuerepository;
    }

    public async Task<Result<EditProjectScheduleColumnResponse?>> Handle(
        EditProjectScheduleColumnCommand command, CT ct)
    {
        try
        {
            var column = await _repository.GetById(command.Id, ct);
            if (column is null)
                return Result.Failure<EditProjectScheduleColumnResponse>(SharedErrors.ItemNotFound)!;

            var isSystem = column.ColumnType != ProjectScheduleColumnType.Custom;
            var titleFa = command.TitleFa?.Trim();
            var titleEn = command.TitleEn?.Trim();

            var titleFaChanged = titleFa is not null && titleFa != column.TitleFa;
            var titleEnChanged = command.TitleEn is not null &&
                (string.IsNullOrWhiteSpace(titleEn) ? null : titleEn) != column.TitleEn;
            var typeChanged = command.Type is not null && command.Type != column.ColumnType;
            var dataTypeChanged = command.DataType is not null && command.DataType != column.DataType;

            if (typeChanged)
                return Result.Failure<EditProjectScheduleColumnResponse>(
                    ProjectErrors.ColumnTypeCannotBeChanged)!;

            if (isSystem && (titleFaChanged || titleEnChanged || dataTypeChanged))
                return Result.Failure<EditProjectScheduleColumnResponse>(
                    ProjectErrors.SystemColumnCannotBeModified)!;

            var columns = await _repository.GetByImportId(column.ProjectScheduleImportId, ct);

            if (titleFaChanged)
            {
                if (titleFa!.Length == 0)
                    return Result.Failure<EditProjectScheduleColumnResponse>(
                        ProjectErrors.ColumnTitleRequired)!;

                if (columns.Any(c => c.Id != column.Id &&
                    string.Equals(c.TitleFa, titleFa, StringComparison.OrdinalIgnoreCase)))
                    return Result.Failure<EditProjectScheduleColumnResponse>(
                        ProjectErrors.ColumnTitleDuplicate)!;

                column.SetTitleFa(titleFa);
            }

            if (titleEnChanged)
                column.SetTitleEn(titleEn);

            if (dataTypeChanged)
            {
                var values = await _valueRepository.GetByColumnId(column.Id, ct);
                if (values.Count > 0)
                    return Result.Failure<EditProjectScheduleColumnResponse>(
                        ProjectErrors.ColumnDataTypeHasValues)!;

                column.SetDataType(command.DataType!.Value);
            }

            if (command.SortOrder is not null && command.SortOrder != column.SortOrder)
            {
                var ordered = columns.OrderBy(c => c.SortOrder).ToList();
                ordered.Remove(column);

                var index = Math.Clamp(command.SortOrder.Value - 1, 0, ordered.Count);
                ordered.Insert(index, column);

                for (var i = 0; i < ordered.Count; i++)
                    ordered[i].SetSortOrder(i + 1);
            }

            return new EditProjectScheduleColumnResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EditProjectScheduleColumnResponse>(SharedErrors.UnknownError)!;
        }
    }
}
