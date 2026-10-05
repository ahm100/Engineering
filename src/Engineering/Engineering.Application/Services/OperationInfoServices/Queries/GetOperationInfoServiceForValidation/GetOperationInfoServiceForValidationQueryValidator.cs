namespace Engineering.Application.Services.OperationInfoServices.Queries.GetOperationInfoServiceForValidation;

public class GetOperationInfoServiceForValidationQueryValidator : AbstractValidator<GetOperationInfoServiceForValidationQuery>
{
    public GetOperationInfoServiceForValidationQueryValidator()
    {
        RuleFor(oo => oo.OperationInfoId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoServiceErrors.OperationInfoIsEmpty);
    }
}
