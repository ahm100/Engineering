namespace Engineering.Application.Services.WbsTemplates.Contracts.UpdateWbsTemplate;

public class UpdateWbsTemplateValidator : AbstractValidator<UpdateWbsTemplateRequest>
{
    public UpdateWbsTemplateValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}