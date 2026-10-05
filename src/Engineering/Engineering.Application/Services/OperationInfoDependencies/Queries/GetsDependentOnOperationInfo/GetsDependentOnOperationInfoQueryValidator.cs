namespace Engineering.Application.Services.OperationInfoDependencies.Queries.GetsDependentOnOperationInfo;

public class GetsDependentOnOperationInfoQueryValidator : AbstractValidator<GetsDependentOnOperationInfoQuery>
{
    public GetsDependentOnOperationInfoQueryValidator()
    {
        RuleFor(oo => oo.OperationInfoId).NotNull().WithError(OperationInfoDependencyErrors.OperationInfoIdIsEmpty);
    }
}
