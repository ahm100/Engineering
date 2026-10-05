namespace Engineering.Application.Services.Contractors.Queries.ContractorServices.GetFilteredContractors;

public class GetFilteredContractorsQueryValidator : AbstractValidator<GetFilteredContractorsQuery>
{
    public GetFilteredContractorsQueryValidator()
    {
        RuleFor(c => c.ProjectId).GreaterThan(0).NotEmpty().WithError(ContractorServicesErrors.ProjectIdIsEmpty);
    }
}
