namespace Engineering.Application.Services.TelegramChats.Queries.GetByRequestGoodsSupplyId;

public class GetByRequestGoodsSupplyIdQueryValidator : AbstractValidator<GetByRequestGoodsSupplyIdQuery>
{
    public GetByRequestGoodsSupplyIdQueryValidator()
    {
        RuleFor(oo => oo.RequestGoodsSupplyId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}