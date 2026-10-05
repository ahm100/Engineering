namespace Engineering.Application.Services.ContractorMachineries.Models.GetContractorMachineryById;

public class GetContractorMachineryByIdValidator : AbstractValidator<GetContractorMachineryByIdRequest>
{
    public GetContractorMachineryByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ContractorMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
