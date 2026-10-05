using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.ModelValidator;

public class CreateMachineryRequestModelValidator : AbstractValidator<CreateMachineryRequestModel>
{
    public CreateMachineryRequestModelValidator()
    {
        RuleFor(oo => oo.MachineryId).NotNull().WithError(ConsumableVolumeMachineryErrors.MachineryIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeMachineryErrors.FinalValueIsEmpty);
    }
}
