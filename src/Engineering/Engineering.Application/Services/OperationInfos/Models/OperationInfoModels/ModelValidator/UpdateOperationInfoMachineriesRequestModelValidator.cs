using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Requests;

namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.ModelValidator;

public class UpdateOperationInfoMachineriesRequestModelValidator : AbstractValidator<UpdateOperationInfoMachineryRequestModel>
{
    public UpdateOperationInfoMachineriesRequestModelValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(MachineryStandardErrors.MachineryUnitIdIsEmpty);
        RuleFor(oo => oo.MachineryNumber).GreaterThanOrEqualTo(0).WithError(MachineryStandardErrors.MachineryNumberIsEmpty);
        RuleFor(oo => oo.TimeSpant).NotEmpty().WithError(MachineryStandardErrors.TimeSpantIsEmpty);
    }
}
