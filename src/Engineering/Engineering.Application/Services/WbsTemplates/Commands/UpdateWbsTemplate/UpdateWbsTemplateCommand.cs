using Engineering.Domain.Entities.WbsTemplates;

namespace Engineering.Application.Services.WbsTemplates.Commands.UpdateWbsTemplate;

public record UpdateWbsTemplateCommand(
    long Id,
    string? Title,
    string? Code,
    string? Description,
    bool? IsActive
     ) : ICommand<WbsTemplate?>;