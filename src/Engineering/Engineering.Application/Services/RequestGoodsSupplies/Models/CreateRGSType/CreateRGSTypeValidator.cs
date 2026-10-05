namespace Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;

public class CreateRGSTypeValidator : AbstractValidator<CreateRGSTypeRequest>
{
    public CreateRGSTypeValidator()
    {
        RuleFor(_ => _.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);

        RuleFor(_ => _.Type)
            .IsEnum(GlobalCmts.Type);

        RuleFor(_ => _.Description)
            .IsRequiredString(RGSCmts.Description);

        RuleFor(_ => _.DescriptionEn)
            .IsRequiredString(RGSCmts.DescriptionEn);

        RuleFor(_ => _.ConsumptionAddress)
            .IsRequiredString(RGSCmts.ConsumptionAddress);

        RuleForEach(_ => _.ProductModels)
            .NotEmpty()
            .SetValidator(new CreateRGSTypeProductModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailProduct);

        RuleForEach(_ => _.ServiceModels)
            .NotEmpty()
            .SetValidator(new CreateRGSTypeServiceModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailService);

        RuleForEach(_ => _.AdvertisementModels)
            .NotEmpty()
            .SetValidator(new CreateRGSTypeAdvertisementModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailAds);

        RuleForEach(_ => _.ProjectModels)
            .NotEmpty()
            .SetValidator(new CreateRGSTypeProjectModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailAds);
    }
}

public class CreateRGSTypeProductModelValidator : AbstractValidator<CreateRGSTypeProductModel>
{
    public CreateRGSTypeProductModelValidator()
    {
        RuleFor(_ => _.ReferenceId)
            .IsPositive(RGSCmts.ReferenceId);
        RuleFor(_ => _.RequestedCount)
            .IsPositive(RGSCmts.RequestedCount);
        RuleFor(_ => _.Importance)
            .IsEnum(RGSCmts.Importance);
        RuleForEach(_ => _.Details)
            .NotEmpty()
            .SetValidator(new CreateRGSTypeDetailProductModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailProduct);
    }
}

public class CreateRGSTypeDetailProductModelValidator : AbstractValidator<CreateRGSTypeDetailProductModel>
{
    public CreateRGSTypeDetailProductModelValidator()
    {
        RuleFor(_ => _.ReferenceId)
            .IsPositive(RGSCmts.ReferenceId);
        RuleFor(_ => _.RequestedCount)
            .IsPositive(RGSCmts.RequestedCount);
        RuleFor(_ => _.Importance)
            .IsEnum(RGSCmts.Importance);
    }
}

public class CreateRGSTypeServiceModelValidator : AbstractValidator<CreateRGSTypeServiceModel>
{
    public CreateRGSTypeServiceModelValidator()
    {
        RuleFor(_ => _.ReferenceId)
            .IsPositive(RGSCmts.ReferenceId);
        RuleFor(_ => _.RequestedCount)
            .IsPositive(RGSCmts.RequestedCount);
        RuleFor(_ => _.Importance)
            .IsEnum(RGSCmts.Importance);
        RuleForEach(_ => _.Details)
            .NotEmpty()
            .SetValidator(new CreateRGSTypeDetailServiceModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailService);
    }
}

public class CreateRGSTypeDetailServiceModelValidator : AbstractValidator<CreateRGSTypeDetailServiceModel>
{
    public CreateRGSTypeDetailServiceModelValidator()
    {
        RuleFor(_ => _.ReferenceId)
            .IsPositive(RGSCmts.ReferenceId);
        RuleFor(_ => _.RequestedCount)
            .IsPositive(RGSCmts.RequestedCount);
        RuleFor(_ => _.Importance)
            .IsEnum(RGSCmts.Importance);
    }
}

public class CreateRGSTypeAdvertisementModelValidator : AbstractValidator<CreateRGSTypeAdvertisementModel>
{
    public CreateRGSTypeAdvertisementModelValidator()
    {
        RuleFor(_ => _.ReferenceId)
            .IsPositive(RGSCmts.ReferenceId);
        RuleFor(_ => _.RequestedCount)
            .IsPositive(RGSCmts.RequestedCount);
        RuleFor(_ => _.Importance)
            .IsEnum(RGSCmts.Importance);
        RuleForEach(_ => _.Details)
            .NotEmpty()
            .SetValidator(new CreateRGSTypeDetailAdvertisementModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailAds);
    }
}

public class CreateRGSTypeDetailAdvertisementModelValidator : AbstractValidator<CreateRGSTypeDetailAdvertisementModel>
{
    public CreateRGSTypeDetailAdvertisementModelValidator()
    {
        RuleFor(_ => _.ReferenceId)
            .IsPositive(RGSCmts.ReferenceId);
        RuleFor(_ => _.RequestedCount)
            .IsPositive(RGSCmts.RequestedCount);
        RuleFor(_ => _.Importance)
            .IsEnum(RGSCmts.Importance);
    }
}

public class CreateRGSTypeProjectModelValidator : AbstractValidator<CreateRGSTypeProjectModel>
{
    public CreateRGSTypeProjectModelValidator()
    {
        RuleFor(_ => _.RequestedCount)
            .IsPositive(RGSCmts.RequestedCount);
        RuleFor(_ => _.Importance)
            .IsEnum(RGSCmts.Importance);
        RuleForEach(_ => _.Details)
            .NotEmpty()
            .SetValidator(new CreateRGSTypeDetailProjectModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailAds);
    }
}

public class CreateRGSTypeDetailProjectModelValidator : AbstractValidator<CreateRGSTypeDetailProjectModel>
{
    public CreateRGSTypeDetailProjectModelValidator()
    {
        RuleFor(_ => _.RequestedCount)
            .IsPositive(RGSCmts.RequestedCount);
        RuleFor(_ => _.Importance)
            .IsEnum(RGSCmts.Importance);
    }
}
