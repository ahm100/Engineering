using Engineering.Application.Services.ProjectOperationWbses.Contracts.DeleteProjectOperationWbs;

namespace Engineering.Application.Services.ProjectOperationWbses.Commands.DeleteProjectOperationWbs;

public record DeleteProjectOperationWbsCommand(
    long Id) : ICommand<DeleteProjectOperationWbsResponse?>;