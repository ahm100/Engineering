using Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorsByServiceIds;

namespace Engineering.Application.Services.Contractors.Models.ContractorServices.GetFilteredContractors;

public class GetFilteredContractorsValidator : AbstractValidator<GetFilteredContractorsRequest>
{
    public GetFilteredContractorsValidator()
    {
        RuleFor(c => c.ProjectId).GreaterThan(0).NotEmpty().WithError(ContractorServicesErrors.ProjectIdIsEmpty);
        RuleFor(c => c.PageIndex).GreaterThanOrEqualTo(0).LessThanOrEqualTo(10000).NotEmpty().WithError(ContractorServicesErrors.InvalidPageIndex);
        RuleFor(c => c.PageSize).GreaterThanOrEqualTo(0).LessThanOrEqualTo(10000).NotEmpty().WithError(ContractorServicesErrors.InvalidPageSize);
    }
}
