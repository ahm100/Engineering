namespace Engineering.Application.Services.Projects.Queries.HaveProjectChild;

public class HaveProjectChildQueryValidator : AbstractValidator<HaveProjectChildQuery>
{
    public HaveProjectChildQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(MetaDataErrors.IdIsEmpty);
    }
}