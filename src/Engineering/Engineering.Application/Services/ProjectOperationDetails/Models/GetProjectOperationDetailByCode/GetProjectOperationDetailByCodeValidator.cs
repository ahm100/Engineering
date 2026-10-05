namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailByCode;

public class GetProjectOperationDetailByCodeValidator : AbstractValidator<GetProjectOperationDetailByCodeRequest>
{
    public GetProjectOperationDetailByCodeValidator()
    {
        RuleFor(oo => oo.Code).NotEmpty().WithError(ProjectOperationDetailErrors.CodeIsEmpty);
    }
}
