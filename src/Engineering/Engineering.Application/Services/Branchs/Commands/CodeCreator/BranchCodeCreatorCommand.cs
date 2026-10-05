namespace Engineering.Application.Services.Branchs.Commands.CodeCreator;

public record BranchCodeCreatorCommand(
    long? CompanyId)
    : ICommand<string?>;