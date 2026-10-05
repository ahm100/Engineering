namespace Engineering.Application.Services.Categories.Commands.CodeCreator;

public record CodeCreatorCommand(
    long? CompanyId)
    : ICommand<string?>;