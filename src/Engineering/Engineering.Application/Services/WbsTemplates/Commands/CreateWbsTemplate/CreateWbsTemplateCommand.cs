using Engineering.Domain.Entities.WbsTemplates;

namespace Engineering.Application.Services.WbsTemplates.Commands.CreateWbsTemplate;

public record CreateWbsTemplateCommand(
    string Title,
    string Code,
    string? Description,
    bool IsActive
     ) : ICommand<WbsTemplate?>;