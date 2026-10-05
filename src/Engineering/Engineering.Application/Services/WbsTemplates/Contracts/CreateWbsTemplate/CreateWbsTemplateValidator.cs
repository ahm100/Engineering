namespace Engineering.Application.Services.WbsTemplates.Contracts.CreateWbsTemplate;

public class CreateWbsTemplateValidator : AbstractValidator<CreateWbsTemplateRequest>
{
    public CreateWbsTemplateValidator()
    {
        RuleFor(oo => oo.Title)
            .IsRequiredString(GlobalCmts.Title);
        RuleFor(oo => oo.Code)
            .IsRequiredString(GlobalCmts.Title);
        RuleFor(oo => oo.IsActive)
            .IsRequiredBool(GlobalCmts.IsActive);
    }
}
