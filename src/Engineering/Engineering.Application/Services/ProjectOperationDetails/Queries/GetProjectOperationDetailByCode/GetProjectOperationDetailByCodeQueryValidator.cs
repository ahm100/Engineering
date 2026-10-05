namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByCode;

public class GetProjectOperationDetailByCodeQueryValidator : AbstractValidator<GetProjectOperationDetailByCodeQuery>
{
    public GetProjectOperationDetailByCodeQueryValidator()
    {
        RuleFor(oo => oo.Code).NotEmpty().WithError(ProjectOperationDetailErrors.CodeIsEmpty);
    }
}