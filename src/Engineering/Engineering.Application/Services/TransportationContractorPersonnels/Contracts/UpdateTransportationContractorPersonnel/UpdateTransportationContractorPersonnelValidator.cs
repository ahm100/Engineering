namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.UpdateTransportationContractorPersonnel;

public class UpdateTransportationContractorPersonnelValidator : AbstractValidator<UpdateTransportationContractorPersonnelRequest>
{
    public UpdateTransportationContractorPersonnelValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(TransportationContractorCmts.TransportationContractorPersonnelId);

        RuleFor(c => c.TransportationContractorId)
            .IsPositive(TransportationContractorCmts.TransportationContractorId);

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
                .NotNull().SetValidator(new UpdatePersonnelAddressModelValidator()!);
        });
    }
}

public class UpdatePersonnelAddressModelValidator : AbstractValidator<UpdatePersonnelAddressModel>
{
    public UpdatePersonnelAddressModelValidator()
    {
        RuleFor(c => c.Address)
            .IsFullString(TransportationContractorCmts.Address, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.Title)
            .IsFullString(GlobalCmts.Title, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(c => c.CityId)
            .IsPositive(TransportationContractorCmts.CityId);
    }
}