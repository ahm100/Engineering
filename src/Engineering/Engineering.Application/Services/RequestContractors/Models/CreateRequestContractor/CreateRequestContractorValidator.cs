namespace Engineering.Application.Services.RequestContractors.Models.CreateRequestContractor;

public class CreateRequestContractorValidator : AbstractValidator<CreateRequestContractorRequest>
{
    public CreateRequestContractorValidator()
    {
        RuleForEach(c => c.ProjectOperationDetails)
            .NotEmpty()
            .WithError(RequestContractorErrors.InValidProjectOperationDetailModel)
            .SetValidator(new ProjectOperationDetailContractorRequestModelValidator());
    }
}
