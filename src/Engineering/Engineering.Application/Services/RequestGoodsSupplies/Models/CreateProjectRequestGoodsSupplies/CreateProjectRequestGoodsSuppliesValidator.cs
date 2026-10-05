
namespace Engineering.Application.Services.RequestGoodsSupplies.Contracts.CreatePRequestGoodsSupplies;

public class CreateProjectRequestGoodsSuppliesValidator : AbstractValidator<CreateProjectRequestGoodsSuppliesRequest>
{
    public CreateProjectRequestGoodsSuppliesValidator()
    {
        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.Id);
        RuleFor(oo => oo.Type)
            .IsEnum(RequestGoodsSupplyErrors.InValidType);
        RuleFor(oo => oo.IsDraft)
            .IsRequiredBool(GlobalCmts.IsLast);
    }
}