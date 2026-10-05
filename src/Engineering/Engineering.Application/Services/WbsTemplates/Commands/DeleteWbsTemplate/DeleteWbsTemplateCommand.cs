using Engineering.Domain.Entities.WbsTemplates;

namespace Engineering.Application.Services.WbsTemplates.Commands.DeleteWbsTemplate;

public record DeleteWbsTemplateCommand(
    long Id
     ) : ICommand<WbsTemplate?>;