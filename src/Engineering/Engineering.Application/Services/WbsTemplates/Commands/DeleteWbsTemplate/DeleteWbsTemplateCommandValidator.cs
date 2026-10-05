namespace Engineering.Application.Services.WbsTemplates.Commands.DeleteWbsTemplate;

public class DeleteWbsTemplateCommandValidator : AbstractValidator<DeleteWbsTemplateCommand>
{
    public DeleteWbsTemplateCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}