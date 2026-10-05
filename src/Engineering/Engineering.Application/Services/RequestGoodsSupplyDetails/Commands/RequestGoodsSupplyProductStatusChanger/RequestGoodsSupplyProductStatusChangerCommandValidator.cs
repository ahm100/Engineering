
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.RequestGoodsSupplyProductStatusChanger;

public class RequestGoodsSupplyProductStatusChangerCommandValidator : AbstractValidator<RequestGoodsSupplyProductStatusChangerCommand>
{
    public RequestGoodsSupplyProductStatusChangerCommandValidator()
    {
        RuleFor(c => c.Entity)
            .NotNull()
            .WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyId);
        RuleFor(c => c.Status)
            .IsEnum(GlobalCmts.Status);
    }
}
