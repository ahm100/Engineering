using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.ModelValidator;

public class CreateExpertRequestModelValidator : AbstractValidator<CreateExpertRequestModel>
{
    public CreateExpertRequestModelValidator()
    {
        RuleFor(oo => oo.ExpertId).NotNull().WithError(ConsumableVolumeExpertErrors.ExpertIdIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeExpertErrors.FinalValueIsEmpty);
    }
}
