namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyDetailByProjectOperationId;

public class GetsGoodsSupplyDetailByProjectOperationIdQueryValidator : AbstractValidator<GetsGoodsSupplyDetailByProjectOperationIdQuery>
{
    public GetsGoodsSupplyDetailByProjectOperationIdQueryValidator()
    {
        RuleFor(c => c.ProjectOperationId)
            .IsPositive(GlobalCmts.Id);
    }
}
