namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSById;

public class GetRGSByIdValidator : AbstractValidator<GetRGSByIdRequest>
{
    public GetRGSByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}