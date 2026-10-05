namespace Engineering.Application.Services.OperationLocations.Queries.HaveOperationLocationChild;

public class HaveOperationLocationChildQueryValidator : AbstractValidator<HaveOperationLocationChildQuery>
{
    public HaveOperationLocationChildQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationLocationErrors.IdIsEmpty);
    }
}