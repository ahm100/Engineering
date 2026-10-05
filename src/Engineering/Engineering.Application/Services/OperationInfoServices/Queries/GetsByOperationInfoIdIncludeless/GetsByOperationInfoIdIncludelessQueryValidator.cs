namespace Engineering.Application.Services.OperationInfoServices.Queries.GetsByOperationInfoIdIncludeless;

public class GetsByOperationInfoIdIncludelessQueryValidator : AbstractValidator<GetsByOperationInfoIdIncludelessQuery>
{
    public GetsByOperationInfoIdIncludelessQueryValidator()
    {
        RuleFor(oo => oo.OprationInfoId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoServiceErrors.OperationInfoIsEmpty);
    }
}