
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplyProductStatusChanger;

public class RequestGoodsSupplyProductStatusChangerValidator : AbstractValidator<RequestGoodsSupplyProductStatusChangerModelRequest>
{
    public RequestGoodsSupplyProductStatusChangerValidator()
    {
        When(x => x.Id != null, () =>
        {
            RuleFor(c => c.Id!.Value)
                .IsPositive(GlobalCmts.Id);
        });

        RuleFor(c => c.Status)
            .IsEnum(GlobalCmts.Status);
    }
}
