using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.UpdateRGSType;

public class UpdateRGSTypeValidator : AbstractValidator<UpdateRGSTypeRequest>
{
    public UpdateRGSTypeValidator()
    {
        RuleFor(oo => oo.RequestGoodsSupplyId)
            .IsPositive(GlobalCmts.Id);

        RuleFor(_ => _.RequestingOrganizationId)
            .IsPositive(RGSCmts.RequestingOrganizationId);

        RuleFor(_ => _.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);

        RuleFor(_ => _.Description)
            .IsRequiredString(RGSCmts.Description);

        RuleFor(_ => _.DescriptionEn)
            .IsRequiredString(RGSCmts.DescriptionEn);

        RuleFor(_ => _.ConsumptionAddress)
            .IsRequiredString(RGSCmts.ConsumptionAddress);

        RuleForEach(_ => _.CreateProductTypes)
            .NotEmpty()
            .SetValidator(new CreateRGSTypeProductModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailProduct);

        RuleForEach(_ => _.UpdateProductTypes)
            .NotEmpty()
            .SetValidator(new UpdateProductRGSTypeModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailProduct);

        RuleForEach(_ => _.CreateServiceTypes)
            .NotEmpty()
            .SetValidator(new CreateRGSTypeServiceModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailService);

        RuleForEach(_ => _.UpdateServiceTypes)
            .NotEmpty()
            .SetValidator(new UpdateServiceRGSTypeModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailService);

        RuleForEach(_ => _.CreateAdvertisementTypes)
            .NotEmpty()
            .SetValidator(new CreateRGSTypeAdvertisementModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailAds);

        RuleForEach(_ => _.UpdateAdvertisementTypes)
            .NotEmpty()
            .SetValidator(new UpdateAdvertisementRGSTypeModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailAds);
    }
}

public class UpdateProductRGSTypeModelValidator : AbstractValidator<UpdateProductRGSTypeModel>
{
    public UpdateProductRGSTypeModelValidator()
    {
        RuleFor(_ => _.RequestGoodsSupplyTypeId)
            .IsPositive(RGSCmts.RequestGoodsSupplyType);

        RuleFor(_ => _.ReferenceId)
            .IsPositive(RGSCmts.ReferenceId);

        RuleForEach(_ => _.CreateDetails)
            .NotEmpty()
            .SetValidator(new CreateRGSTypeDetailProductModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailProduct);

        RuleForEach(_ => _.UpdateDetails)
            .NotEmpty()
            .SetValidator(new UpdateRGSTypeDetailProductModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailProduct);
    }
}

public class UpdateRGSTypeDetailProductModelValidator : AbstractValidator<UpdateRGSTypeDetailProductModel>
{
    public UpdateRGSTypeDetailProductModelValidator()
    {
        RuleFor(_ => _.RGSTypeDetailId)
            .IsPositive(RGSCmts.RequestGoodsSupplyTypeDetail);

        RuleFor(_ => _.ReferenceId)
            .IsPositive(RGSCmts.ReferenceId);

        RuleFor(_ => _.RequestedCount)
            .IsPositive(RGSCmts.RequestedCount);
    }
}

public class UpdateServiceRGSTypeModelValidator : AbstractValidator<UpdateServiceRGSTypeModel>
{
    public UpdateServiceRGSTypeModelValidator()
    {
        RuleFor(_ => _.RequestGoodsSupplyTypeId)
            .IsPositive(RGSCmts.RequestGoodsSupplyType);

        RuleFor(_ => _.ReferenceId)
            .IsPositive(RGSCmts.ReferenceId);

        RuleForEach(_ => _.CreateDetails)
            .NotEmpty()
            .SetValidator(new CreateRGSTypeDetailServiceModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailService);

        RuleForEach(_ => _.UpdateDetails)
            .NotEmpty()
            .SetValidator(new UpdateRGSTypeDetailServiceModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailService);
    }
}

public class UpdateRGSTypeDetailServiceModelValidator : AbstractValidator<UpdateRGSTypeDetailServiceModel>
{
    public UpdateRGSTypeDetailServiceModelValidator()
    {
        RuleFor(_ => _.RGSTypeDetailId)
            .IsPositive(RGSCmts.RequestGoodsSupplyTypeDetail);

        RuleFor(_ => _.ReferenceId)
            .IsPositive(RGSCmts.ReferenceId);

        RuleFor(_ => _.RequestedCount)
            .IsPositive(RGSCmts.RequestedCount);
    }
}

public class UpdateAdvertisementRGSTypeModelValidator : AbstractValidator<UpdateAdvertisementRGSTypeModel>
{
    public UpdateAdvertisementRGSTypeModelValidator()
    {
        RuleFor(_ => _.RequestGoodsSupplyTypeId)
            .IsPositive(RGSCmts.RequestGoodsSupplyType);

        RuleFor(_ => _.ReferenceId)
            .IsPositive(RGSCmts.ReferenceId);

        RuleForEach(_ => _.CreateDetails)
            .NotEmpty()
            .SetValidator(new CreateRGSTypeDetailAdvertisementModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailAds);

        RuleForEach(_ => _.UpdateDetails)
            .NotEmpty()
            .SetValidator(new UpdateRGSTypeDetailAdvertisementModelValidator())
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetailAds);
    }
}

public class UpdateRGSTypeDetailAdvertisementModelValidator : AbstractValidator<UpdateRGSTypeDetailAdvertisementModel>
{
    public UpdateRGSTypeDetailAdvertisementModelValidator()
    {
        RuleFor(_ => _.RGSTypeDetailId)
            .IsPositive(RGSCmts.RequestGoodsSupplyTypeDetail);

        RuleFor(_ => _.ReferenceId)
            .IsPositive(RGSCmts.ReferenceId);

        RuleFor(_ => _.RequestedCount)
            .IsPositive(RGSCmts.RequestedCount);
    }
}