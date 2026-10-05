using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.ModelValidator;

public class CreateProductRequestModelValidator : AbstractValidator<CreateProductRequestModel>
{
    public CreateProductRequestModelValidator()
    {
        RuleFor(oo => oo.ProductGroupId).NotNull().WithError(ConsumableVolumeProductErrors.ProductGroupIdIsEmptyIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeProductErrors.FinalValueIsEmpty);
    }
}
