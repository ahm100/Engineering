
namespace Engineering.Application.Services.RequestGoodsSupplies.Models.CreateTemporaryRequestGoodsSupply;

public class CreateTemporaryRequestGoodsSupplyValidator : AbstractValidator<CreateTemporaryRequestGoodsSupplyRequest>
{
    public CreateTemporaryRequestGoodsSupplyValidator()
    {
        RuleFor(oo => oo.Type)
            .IsEnum(RequestGoodsSupplyErrors.InValidType);
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperationId);
    }
}