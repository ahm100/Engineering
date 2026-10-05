namespace Engineering.Application.Services.Contractors.Queries.ContractorServices.GetContractorsByServiceIds;

public class GetContractorsByServiceIdsQueryValidator : AbstractValidator<GetContractorsByServiceIdsQuery>
{
    public GetContractorsByServiceIdsQueryValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(ContractorServicesErrors.IdIsEmpty);
    }
}
