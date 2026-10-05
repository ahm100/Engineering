using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.ModelValidator;

public class UpdateProductRequestModelValidator : AbstractValidator<UpdateProductRequestModel>
{
    public UpdateProductRequestModelValidator()
    {
        When(oo => oo.Id != null, () =>
        {
            RuleFor(oo => oo.Id).NotNull().WithError(ConsumableVolumeExpertErrors.IdIsEmpty);
        });
        RuleFor(oo => oo.ProductGroupId).NotNull().WithError(ConsumableVolumeProductErrors.ProductGroupIdIsEmptyIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeProductErrors.FinalValueIsEmpty);
        RuleFor(oo => oo.UnusedPercentage).NotNull().WithError(ConsumableVolumeProductErrors.UnusedPercentageIsEmpty);
        RuleFor(oo => oo.VolumeProductType)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum)
            .NotNull().WithError(GlobalErrors.TypeIsNull);
    }
}
