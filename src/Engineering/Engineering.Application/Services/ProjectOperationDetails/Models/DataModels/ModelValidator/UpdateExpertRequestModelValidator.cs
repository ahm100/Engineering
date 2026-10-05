using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.ModelValidator;

public class UpdateExpertRequestModelValidator : AbstractValidator<UpdateExpertRequestModel>
{
    public UpdateExpertRequestModelValidator()
    {
        When(oo => oo.Id != null, () =>
        {
            RuleFor(oo => oo.Id).NotNull().WithError(ConsumableVolumeExpertErrors.IdIsEmpty);
        });
        RuleFor(oo => oo.ExpertId).NotNull().WithError(ConsumableVolumeExpertErrors.ExpertIdIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeExpertErrors.FinalValueIsEmpty);
        RuleFor(oo => oo.UnusedPercentage).NotNull().WithError(ConsumableVolumeExpertErrors.UnusedPercentageIsEmpty);

    }
}
