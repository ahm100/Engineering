namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoProjectOperationsValidator;

public class GetOperationInfoProjectOperationsValidatorQueryValidator : AbstractValidator<GetOperationInfoProjectOperationsValidatorQuery>
{
    public GetOperationInfoProjectOperationsValidatorQueryValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
    }
}