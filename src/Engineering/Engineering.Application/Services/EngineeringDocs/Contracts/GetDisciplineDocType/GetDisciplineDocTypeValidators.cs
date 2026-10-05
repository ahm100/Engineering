
namespace Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDocType;

public class GetDisciplineDocTypeValidator : AbstractValidator<GetDisciplineDocTypeRequest>
{
    public GetDisciplineDocTypeValidator()
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


        When(c => c.DisciplineId.HasValue, () =>
        {
            RuleFor(c => c.DisciplineId)
                .GreaterThan(0);
        });

    }

}