
namespace Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDoc;

public class GetDisciplineDocValidator : AbstractValidator<GetDisciplineDocRequest>
{
    public GetDisciplineDocValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);

        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);

        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}
