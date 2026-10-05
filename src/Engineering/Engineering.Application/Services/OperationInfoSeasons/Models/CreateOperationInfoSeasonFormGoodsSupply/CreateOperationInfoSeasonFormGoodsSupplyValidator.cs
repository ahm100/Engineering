
namespace Engineering.Application.Services.OperationInfoSeasons.Models.CreateOperationInfoSeasonFormGoodsSupply;

public class CreateOperationInfoSeasonFormGoodsSupplyValidator : AbstractValidator<CreateOperationInfoSeasonFormGoodsSupplyRequest>
{
    public CreateOperationInfoSeasonFormGoodsSupplyValidator()
    {
        RuleFor(oo => oo.OperationInfo).NotNull().WithError(OperationInfoSeasonErrors.OperationInfoIsEmpty);
        RuleFor(oo => oo.SeasonId).NotNull().WithError(OperationInfoSeasonErrors.NoHaveSeason);
    }
}
