namespace Engineering.Application.Services.RequestGoodsSupplies.Models.CreateProjectRequestGoodsSuppliesDraft;

public class CreateProjectRequestGoodsSupplyDraftValidator : AbstractValidator<CreateProjectRequestGoodsSupplyDraftRequest>
{
    public CreateProjectRequestGoodsSupplyDraftValidator()
    {
        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.Id);
        RuleFor(oo => oo.Type)
            .IsEnum(RequestGoodsSupplyErrors.InValidType);
        RuleFor(oo => oo.IsDraft)
            .IsRequiredBool(GlobalCmts.IsLast);
    }
}