using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.ModelValidator;

namespace Engineering.Application.Services.ConsumableVolumes.Models.UpdateConsumableVolumes;

public class UpdateConsumableVolumesValidator : AbstractValidator<UpdateConsumableVolumesRequest>
{
    public UpdateConsumableVolumesValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(ConsumableVolumeExpertErrors.ProjectOperationDetailIdIsEmpty);
        When(oo => oo.ExpertRequests != null, () =>
        {
            RuleForEach(oo => oo.ExpertRequests).NotEmpty().SetValidator(new UpdateExpertRequestModelValidator());
        });
        When(oo => oo.MachineryRequests != null, () =>
        {
            RuleForEach(oo => oo.MachineryRequests).NotEmpty().SetValidator(new UpdateMachineryRequestModelValidator());
        });
        When(oo => oo.ProductRequests != null, () =>
        {
            RuleForEach(oo => oo.ProductRequests).NotEmpty().SetValidator(new UpdateProductRequestModelValidator());
        });
    }
}
