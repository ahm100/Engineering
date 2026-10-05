namespace Engineering.Application.Services.RequestRewards.Contracts.CreateRequestReward;

public class CreateRequestRewardValidator : AbstractValidator<CreateRequestRewardRequest>
{
    public CreateRequestRewardValidator()
    {
        RuleForEach(oo => oo.Data).NotNull().SetValidator(new CreateRequestRewardModelValidator()).WithError(RequestRewardErrors.InValidData);
        RuleForEach(oo => oo.Data).NotEmpty().SetValidator(new CreateRequestRewardModelValidator()).WithError(RequestRewardErrors.InValidData);
    }
}

public class CreateRequestRewardModelValidator : AbstractValidator<CreateRequestRewardModel>
{
    public CreateRequestRewardModelValidator()
    {
        RuleFor(oo => oo.Type).IsInEnum().WithError(RequestRewardErrors.InValidType);
        RuleFor(oo => oo.CostCenterId).IsPositive(GlobalCmts.CostCenter);
        RuleFor(oo => oo.RegistrationDate).IsDate(RequestRewardCmts.RegistrationDate);
        RuleFor(oo => oo.Description).NotEmpty().WithError(RequestRewardErrors.InValidDescription);
        RuleForEach(oo => oo.ThirdPartyIds).NotEmpty().ChildRules(oo => oo.RuleFor(oo => oo).NotEmpty().WithError(RequestRewardErrors.InValidThirdPartyIds)).WithError(RequestRewardErrors.InValidThirdPartyIds);
        When(oo => oo.Products != null, () =>
        {
            RuleFor(oo => oo.Products!.Sum(c => c.Price)).LessThanOrEqualTo(oo => oo.OfferedPrice).WithError(RequestRewardErrors.InValidProductPrices);
            RuleForEach(oo => oo.Products).NotEmpty().SetValidator(new CreateRequestRewardProductModelValidator());
        });
        When(oo => oo.Documents != null, () =>
        {
            RuleForEach(oo => oo.Documents).NotEmpty().SetValidator(new CreateRequestRewardDocumentModelValidator());
        });
        When(oo => oo.ThirdPartyIds != null, () =>
        {
            RuleForEach(oo => oo.ThirdPartyIds).NotEmpty().SetValidator(new CreateRequestRewardThirdPartyModelValidator());
        });
        RuleFor(oo => oo.IsDeleted)
            .IsRequiredBool(GlobalCmts.IsDeleted);
    }
}

public class CreateRequestRewardProductModelValidator : AbstractValidator<CreateRequestRewardProductModel>
{
    public CreateRequestRewardProductModelValidator()
    {
        RuleFor(oo => oo.ProductId)
            .IsPositive(RequestRewardCmts.ProductId);
        RuleFor(oo => oo.Price)
            .IsPositive(RequestRewardCmts.Price);
        RuleFor(oo => oo.Count)
            .IsPositive(GlobalCmts.Count);
        RuleFor(oo => oo.ProductId)
            .IsPositive(GlobalCmts.ProductId);
        RuleFor(oo => oo.CurrencyId)
            .IsPositive(GlobalCmts.CurrencyId);
        RuleFor(oo => oo.IsDeleted)
            .IsRequiredBool(GlobalCmts.IsDeleted);
    }
}

public class CreateRequestRewardThirdPartyModelValidator : AbstractValidator<CreateRequestRewardThirdPartyModel>
{
    public CreateRequestRewardThirdPartyModelValidator()
    {
        RuleFor(oo => oo.ThirdPartyId)
            .IsPositive(RequestRewardCmts.ThirdPartyId);
        RuleFor(oo => oo.IsDeleted)
            .IsRequiredBool(GlobalCmts.IsDeleted);
    }
}

public class CreateRequestRewardDocumentModelValidator : AbstractValidator<CreateRequestRewardDocumentModel>
{
    public CreateRequestRewardDocumentModelValidator()
    {
        RuleFor(oo => oo.IsDeleted)
            .IsRequiredBool(GlobalCmts.IsDeleted);
    }
}