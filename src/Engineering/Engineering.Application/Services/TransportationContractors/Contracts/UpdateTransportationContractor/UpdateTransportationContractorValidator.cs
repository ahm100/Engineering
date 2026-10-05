using Engineering.Application.Services.TransportationContractors.Contracts.CreateTransportationContractor;

namespace Engineering.Application.Services.TransportationContractors.Contracts.UpdateTransportationContractor;

public class UpdateTransportationContractorValidator : AbstractValidator<UpdateTransportationContractorRequest>
{
    public UpdateTransportationContractorValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(TransportationContractorCmts.TransportationContractorId);

        When(x => x.Type != ClientSdk.Enums.TransportationContractorCalculateType.Price, () =>
        {
            RuleFor(oo => oo.StartOfContract.Value)
                .IsDate(TransportationContractorCmts.StartOfContract);

            RuleFor(oo => oo.EndOfContract.Value)
                .IsDate(TransportationContractorCmts.EndOfContract);

            RuleFor(oo => oo.StartOfContract.Value.Date)
                .LessThanOrEqualTo(oo => oo.EndOfContract.Value.Date).WithError(GlobalErrors.StartDateCanNotBigerToEndDate);

            RuleFor(oo => oo.EndOfContract.Value.Date)
                .GreaterThanOrEqualTo(oo => oo.StartOfContract.Value.Date).WithError(GlobalErrors.EndDateCanNotSmallerToStartDate);

            RuleFor(oo => oo.PercentageValue.Value)
                .IsRequiredDecimal(TransportationContractorCmts.PercentageValue);
        });

        RuleFor(oo => oo.IsActive)
            .IsRequiredBool(GlobalCmts.IsActive);

        RuleFor(oo => oo.FirstPrefix)
            .IsFullString(TransportationContractorCmts.FirstPrefix, 10, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        When(c => c.ContractorPersonnels != null && c.ContractorPersonnels.Any(), () =>
        {
            RuleForEach(c => c.ContractorPersonnels)
                .NotEmpty().SetValidator(new UpdateContractorPersonnelValidator());
        });

        When(c => c.Address != null, () =>
        {
            RuleFor(c => c.Address)
                .NotNull().SetValidator(new UpdateTransportationContractorAddressValidator()!);
        });

        When(c => c.PriceWeights != null && c.PriceWeights.Count > 0, () =>
        {
            RuleForEach(c => c.PriceWeights).SetValidator(new CreateContractorPriceWeightModelValidator()!);
        });

        When(c => c.UpdatePriceWeights != null && c.UpdatePriceWeights.Count > 0, () =>
        {
            RuleForEach(c => c.UpdatePriceWeights).SetValidator(new UpdateContractorPriceWeightModelValidator()!);
        });

        When(c => c.Insurances != null && c.Insurances.Count > 0, () =>
        {
            RuleForEach(c => c.Insurances).SetValidator(new CreateContractorInsuranceModelValidator()!);
        });

        When(c => c.UpdateInsurances != null && c.UpdateInsurances.Count > 0, () =>
        {
            RuleForEach(c => c.UpdateInsurances).SetValidator(new UpdateContractorInsuranceModelValidator()!);
        });
    }
}
public class UpdateContractorPersonnelValidator : AbstractValidator<UpdateContractorPersonnelModel>
{
    public UpdateContractorPersonnelValidator()
    {
        RuleFor(c => c.FirstName)
            .IsFullString(TransportationContractorCmts.FirstName, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.LastName)
            .IsFullString(TransportationContractorCmts.LastName, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.PhoneNumber)
            .IsFullString(TransportationContractorCmts.PhoneNumber, 50, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.IdentityNo)
            .IsFullString(TransportationContractorCmts.IdentityNo, 20, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        When(c => c.PersonnelAddress != null, () =>
        {
            RuleFor(c => c.PersonnelAddress)
                .NotNull().SetValidator(new UpdateContractorPersonnelAddressValidator()!);
        });
    }
}
public class UpdateTransportationContractorAddressValidator : AbstractValidator<UpdateTransportationContractorAddressModel>
{
    public UpdateTransportationContractorAddressValidator()
    {
        RuleFor(c => c.Address)
            .IsFullString(TransportationContractorCmts.Address, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.Title)
            .IsFullString(GlobalCmts.Title, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.CityId)
            .IsPositive(TransportationContractorCmts.CityId);
    }
}
public class UpdateContractorPersonnelAddressValidator : AbstractValidator<UpdateContractorPersonnelAddressModel>
{
    public UpdateContractorPersonnelAddressValidator()
    {
        RuleFor(c => c.Address)
            .IsFullString(TransportationContractorCmts.Address, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.Title)
            .IsFullString(GlobalCmts.Title, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.CityId)
            .IsPositive(TransportationContractorCmts.CityId);
    }
}

public class UpdateContractorPriceWeightModelValidator : AbstractValidator<UpdateContractorPriceWeightModel>
{
    public UpdateContractorPriceWeightModelValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(TransportationContractorPriceWeightCmts.TransportationContractorPriceWeightId);

        RuleFor(c => c.UntilWeight)
            .IsRequiredDecimal(TransportationContractorPriceWeightCmts.UntilWeight);

        RuleFor(c => c.Price)
            .IsRequiredDecimal(TransportationContractorPriceWeightCmts.Price);

        RuleFor(c => c.IsFixed)
            .IsRequiredBool(TransportationContractorPriceWeightCmts.IsFixed);
    }
}
public class UpdateContractorInsuranceModelValidator : AbstractValidator<UpdateContractorInsuranceModel>
{
    public UpdateContractorInsuranceModelValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(TransportationContractorInsuranceCmts.TransportationContractorInsuranceId);

        RuleFor(c => c.MinProductPrice)
            .IsRequiredDecimal(TransportationContractorInsuranceCmts.MinProductPrice);

        RuleFor(c => c.MaxProductPrice)
            .IsRequiredDecimal(TransportationContractorInsuranceCmts.MaxProductPrice);

        RuleFor(c => c.FixedPrice)
            .IsRequiredDecimal(TransportationContractorInsuranceCmts.FixedPrice);
    }
}