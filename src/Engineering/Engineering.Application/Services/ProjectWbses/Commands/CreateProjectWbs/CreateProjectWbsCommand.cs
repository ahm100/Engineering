using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectWbs;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateProjectWbs;

public record CreateProjectWbsCommand(
    long ProjectId,
    long? ParentWbsId,
    long ProjectScheduleImportId,
    string TitleFa,
    string Code) : ICommand<CreateProjectWbsResponse?>;