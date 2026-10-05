namespace Engineering.Application.Services.RequestGoodsSupplies.Contracts.DeletePRequestGoodsSupplies;

public class DeleteProjectRequestGoodsSuppliesValidator : AbstractValidator<DeleteProjectRequestGoodsSuppliesRequest>
{
    public DeleteProjectRequestGoodsSuppliesValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}