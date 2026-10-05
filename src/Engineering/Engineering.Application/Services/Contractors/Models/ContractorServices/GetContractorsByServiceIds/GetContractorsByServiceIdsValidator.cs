namespace Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorsByServiceIds;

public class GetContractorsByServiceIdsValidator : AbstractValidator<GetContractorsByServiceIdsRequest>
{
    public GetContractorsByServiceIdsValidator()
    {
        RuleForEach(c => c.Ids).NotEmpty().WithError(ContractorServicesErrors.IdIsEmpty);
    }
}
