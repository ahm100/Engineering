namespace Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateById;

public class GetWbsTemplateByIdValidator : AbstractValidator<GetWbsTemplateByIdRequest>
{
    public GetWbsTemplateByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}