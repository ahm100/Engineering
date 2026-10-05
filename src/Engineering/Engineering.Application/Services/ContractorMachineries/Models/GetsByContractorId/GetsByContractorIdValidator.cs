namespace Engineering.Application.Services.ContractorMachineries.Models.GetsByContractorId;

public class GetsByContractorIdValidator : AbstractValidator<GetsByContractorIdRequest>
{
    public GetsByContractorIdValidator()
    {
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
        RuleFor(oo => oo.ContractorId).NotEmpty().WithError(ContractorMachineryErrors.ContractorIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
