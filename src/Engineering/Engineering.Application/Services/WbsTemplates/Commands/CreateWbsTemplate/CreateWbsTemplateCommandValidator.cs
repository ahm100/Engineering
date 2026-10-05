namespace Engineering.Application.Services.WbsTemplates.Commands.CreateWbsTemplate;

public class CreateWbsTemplateCommandValidator : AbstractValidator<CreateWbsTemplateCommand>
{
    public CreateWbsTemplateCommandValidator()
    {
        RuleFor(oo => oo.Title)
            .IsRequiredString(GlobalCmts.Title);
        RuleFor(oo => oo.Code)
            .IsRequiredString(GlobalCmts.Title);
        RuleFor(oo => oo.IsActive)
            .IsRequiredBool(GlobalCmts.IsActive);
    }
}
