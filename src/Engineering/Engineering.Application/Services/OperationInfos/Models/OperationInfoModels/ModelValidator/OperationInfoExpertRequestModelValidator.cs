using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Requests;

namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.ModelValidator;

public class OperationInfoExpertRequestModelValidator : AbstractValidator<OperationInfoExpertRequestModel>
{
    public OperationInfoExpertRequestModelValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(ExpertStandardErrors.ExpertUnitIdIsEmpty);
        RuleFor(oo => oo.ExpertNumber).GreaterThanOrEqualTo(0).WithError(ExpertStandardErrors.ExpertNumberIsEmpty);
        RuleFor(oo => oo.TimeSpant).NotEmpty().WithError(ExpertStandardErrors.TimeSpantIsEmpty);
    }
}

