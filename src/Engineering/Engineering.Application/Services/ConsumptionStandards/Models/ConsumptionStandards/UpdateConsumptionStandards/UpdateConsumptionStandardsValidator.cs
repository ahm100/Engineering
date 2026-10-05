using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.ModelValidator;

namespace Engineering.Application.Services.ConsumptionStandards.Models.ConsumptionStandards.UpdateConsumptionStandards;

public class UpdateConsumptionStandardsValidator : AbstractValidator<UpdateConsumptionStandardsRequest>
{
    public UpdateConsumptionStandardsValidator()
    {
        RuleFor(oo => oo.OprationInfoId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(OperationInfoErrors.IdIsEmpty);
        When(oo => oo.ExpertStandards != null, () =>
        {
            RuleForEach(oo => oo.ExpertStandards).NotEmpty().SetValidator(new UpdateOperationInfoExpertRequestModelValidator());
        });
        When(oo => oo.GoodsStandards != null, () =>
        {
            RuleForEach(oo => oo.GoodsStandards).NotEmpty().SetValidator(new UpdateOperationInfoGoodsRequestModelValidator());
        });
        When(oo => oo.MachineryStandards != null, () =>
        {
            RuleForEach(oo => oo.MachineryStandards).NotEmpty().SetValidator(new UpdateOperationInfoMachineriesRequestModelValidator());
        });
    }
}
