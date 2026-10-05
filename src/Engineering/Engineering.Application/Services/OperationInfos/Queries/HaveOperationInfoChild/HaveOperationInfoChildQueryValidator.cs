namespace Engineering.Application.Services.OperationInfos.Queries.HaveOperationInfoChild;

public class HaveOperationInfoChildQueryValidator : AbstractValidator<HaveOperationInfoChildQuery>
{
    public HaveOperationInfoChildQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationInfoErrors.IdIsEmpty);
    }
}