namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRGSById;

public class GetRGSByIdQueryValidator : AbstractValidator<GetRGSByIdQuery>
{
    public GetRGSByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}