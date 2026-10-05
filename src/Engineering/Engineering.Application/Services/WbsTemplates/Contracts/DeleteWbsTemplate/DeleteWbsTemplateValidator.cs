namespace Engineering.Application.Services.WbsTemplates.Contracts.DeleteWbsTemplate;

public class DeleteWbsTemplateValidator : AbstractValidator<DeleteWbsTemplateRequest>
{
    public DeleteWbsTemplateValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}