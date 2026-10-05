namespace Engineering.Application.Services.RequestContractors.Models.CreateRequestContractor;

public class ProjectOperationDetailContractorRequestModelValidator : AbstractValidator<ProjectOperationDetailContractorRequestModel>
{
    public ProjectOperationDetailContractorRequestModelValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidProjectOperationDetail)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleForEach(c => c.ServiceInfos)
            .NotEmpty()
            .WithError(RequestContractorErrors.InValidServiceInfoModel)
            .SetValidator(new ServiceInfoContractorRequestModelValidator());
    }
}
