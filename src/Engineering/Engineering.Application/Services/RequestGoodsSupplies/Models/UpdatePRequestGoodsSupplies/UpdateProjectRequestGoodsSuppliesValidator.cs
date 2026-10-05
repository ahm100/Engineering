namespace Engineering.Application.Services.RequestGoodsSupplies.Contracts.UpdatePRequestGoodsSupplies;

public class UpdateProjectRequestGoodsSuppliesValidator : AbstractValidator<UpdateProjectRequestGoodsSuppliesRequest>
{
    public UpdateProjectRequestGoodsSuppliesValidator()
    {
        RuleFor(oo => oo.RequestGoodsSupplyId)
            .IsPositive(GlobalCmts.Id);
    }
}