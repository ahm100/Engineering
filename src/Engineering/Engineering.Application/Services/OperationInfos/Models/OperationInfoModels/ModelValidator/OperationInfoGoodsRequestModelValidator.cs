using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Requests;

namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.ModelValidator;

public class OperationInfoGoodsRequestModelValidator : AbstractValidator<OperationInfoGoodsRequestModel>
{
    public OperationInfoGoodsRequestModelValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProductStandardErrors.ProductIdIsEmpty);
        RuleFor(oo => oo.GoodsNumber).GreaterThanOrEqualTo(0).WithError(ProductStandardErrors.ProductNumberIsEmpty);
        RuleFor(oo => oo.StandardProductType)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum)
            .NotNull().WithError(ProductStandardErrors.TypeIsEmpty);
    }
}
