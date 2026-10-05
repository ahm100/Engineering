namespace Engineering.Application.Services.WbsTemplates.Queries.GetWbsTemplateById;

public class GetWbsTemplateByIdQueryValidator : AbstractValidator<GetWbsTemplateByIdQuery>
{
    public GetWbsTemplateByIdQueryValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}