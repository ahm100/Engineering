
namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.CreateRequestGoodsSupply;

public class CreateRequestGoodsSupplyCommandValidator : AbstractValidator<CreateRequestGoodsSupplyCommand>
{
    public CreateRequestGoodsSupplyCommandValidator()
    {
        RuleFor(oo => oo.Type)
            .IsEnum(GlobalCmts.Type);
        RuleFor(oo => oo.ProjectOperation)
            .NotEmpty()
            .WithError(RequestGoodsSupplyErrors.InValidProjectOperation);
        RuleFor(oo => oo.OperationInfoSeason)
            .NotEmpty()
            .WithError(RequestGoodsSupplyErrors.InValidOperationInfoSeasonId);
    }
}
