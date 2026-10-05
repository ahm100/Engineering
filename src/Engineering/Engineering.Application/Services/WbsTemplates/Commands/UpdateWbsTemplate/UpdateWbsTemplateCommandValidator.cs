namespace Engineering.Application.Services.WbsTemplates.Commands.UpdateWbsTemplate;

public class UpdateWbsTemplateCommandValidator : AbstractValidator<UpdateWbsTemplateCommand>
{
    public UpdateWbsTemplateCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}