using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.ModelValidator;

public class UpdateMachineryRequestModelValidator : AbstractValidator<UpdateMachineryRequestModel>
{
    public UpdateMachineryRequestModelValidator()
    {
        When(oo => oo.Id != null, () =>
        {
            RuleFor(oo => oo.Id).NotNull().WithError(ConsumableVolumeExpertErrors.IdIsEmpty);
        });
        RuleFor(oo => oo.MachineryId).NotNull().WithError(ConsumableVolumeMachineryErrors.MachineryIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeMachineryErrors.FinalValueIsEmpty);
        RuleFor(oo => oo.UnusedPercentage).NotNull().WithError(ConsumableVolumeMachineryErrors.UnusedPercentageIsEmpty);
    }
}
