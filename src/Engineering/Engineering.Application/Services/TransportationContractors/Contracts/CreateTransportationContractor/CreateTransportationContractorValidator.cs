namespace Engineering.Application.Services.TransportationContractors.Contracts.CreateTransportationContractor;

public class CreateTransportationContractorValidator : AbstractValidator<CreateTransportationContractorRequest>
{
    public CreateTransportationContractorValidator()
    {
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

        RuleFor(oo => oo.IsActive)
            .IsRequiredBool(GlobalCmts.IsActive);

        When(c => c.ContractorPersonnels != null && c.ContractorPersonnels.Any(), () =>
        {
            RuleForEach(c => c.ContractorPersonnels)
                .NotEmpty().SetValidator(new CreateContractorPersonnelValidator());
        });

        When(c => c.Address != null, () =>
        {
            RuleFor(c => c.Address)
                .NotNull().SetValidator(new CreateTransportationContractorAddressValidator()!);
        });

        When(c => c.PriceWeights != null && c.PriceWeights.Count > 0, () =>
        {
            RuleForEach(c => c.PriceWeights).SetValidator(new CreateContractorPriceWeightModelValidator()!);
        });

        When(c => c.Insurances != null && c.Insurances.Count > 0, () =>
        {
            RuleForEach(c => c.Insurances).SetValidator(new CreateContractorInsuranceModelValidator()!);
        });
    }
}
public class CreateContractorPersonnelValidator : AbstractValidator<CreateContractorPersonnelModel>
{
    public CreateContractorPersonnelValidator()
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
                .NotNull().SetValidator(new CreateContractorPersonnelAddressValidator()!);
        });
    }
}
public class CreateTransportationContractorAddressValidator : AbstractValidator<CreateTransportationContractorAddressModel>
{
    public CreateTransportationContractorAddressValidator()
    {
        RuleFor(c => c.Address)
            .IsFullString(TransportationContractorCmts.Address, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.Title)
            .IsFullString(GlobalCmts.Title, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.CityId)
            .IsPositive(TransportationContractorCmts.CityId);
    }
}
public class CreateContractorPersonnelAddressValidator : AbstractValidator<CreateContractorPersonnelAddressModel>
{
    public CreateContractorPersonnelAddressValidator()
    {
        RuleFor(c => c.Address)
            .IsFullString(TransportationContractorCmts.Address, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.Title)
            .IsFullString(GlobalCmts.Title, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.CityId)
            .IsPositive(TransportationContractorCmts.CityId);
    }
}

public class CreateContractorPriceWeightModelValidator : AbstractValidator<CreateContractorPriceWeightModel>
{
    public CreateContractorPriceWeightModelValidator()
    {
        RuleFor(c => c.UntilWeight)
            .IsRequiredDecimal(TransportationContractorPriceWeightCmts.UntilWeight);

        RuleFor(c => c.Price)
            .IsRequiredDecimal(TransportationContractorPriceWeightCmts.Price);

        RuleFor(c => c.IsFixed)
            .IsRequiredBool(TransportationContractorPriceWeightCmts.IsFixed);
    }
}

public class CreateContractorInsuranceModelValidator : AbstractValidator<CreateContractorInsuranceModel>
{
    public CreateContractorInsuranceModelValidator()
    {
        RuleFor(c => c.MinProductPrice)
            .IsRequiredDecimal(TransportationContractorInsuranceCmts.MinProductPrice);

        RuleFor(c => c.MaxProductPrice)
            .IsRequiredDecimal(TransportationContractorInsuranceCmts.MaxProductPrice);

        RuleFor(c => c.FixedPrice)
            .IsRequiredDecimal(TransportationContractorInsuranceCmts.FixedPrice);
    }
}
