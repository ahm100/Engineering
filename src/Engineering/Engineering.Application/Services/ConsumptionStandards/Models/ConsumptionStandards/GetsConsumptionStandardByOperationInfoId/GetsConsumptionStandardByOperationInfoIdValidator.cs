
namespace Engineering.Application.Services.ConsumptionStandards.Models.ConsumptionStandards.GetsConsumptionStandardByOperationInfoId;

public class GetsConsumptionStandardByOperationInfoIdValidator : AbstractValidator<GetsConsumptionStandardByOperationInfoIdRequest>
{
    public GetsConsumptionStandardByOperationInfoIdValidator()
    {
        RuleFor(oo => oo.OprationInfoId).NotNull().WithError(OperationInfoErrors.IdIsEmpty);
    }
}
