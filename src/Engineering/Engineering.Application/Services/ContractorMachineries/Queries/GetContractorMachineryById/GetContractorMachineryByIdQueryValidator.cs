namespace Engineering.Application.Services.ContractorMachineries.Queries.GetContractorMachineryById;

public class GetContractorMachineryByIdQueryValidator : AbstractValidator<GetContractorMachineryByIdQuery>
{
    public GetContractorMachineryByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ContractorMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}