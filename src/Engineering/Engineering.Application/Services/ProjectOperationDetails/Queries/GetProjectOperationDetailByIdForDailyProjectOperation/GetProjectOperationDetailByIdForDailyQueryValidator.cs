namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdForDailyProjectOperation;

public class GetProjectOperationDetailByIdForDailyQueryValidator : AbstractValidator<GetProjectOperationDetailByIdForDailyQuery>
{
    public GetProjectOperationDetailByIdForDailyQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
