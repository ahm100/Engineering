
namespace Engineering.Application.Services.Branchs.Queries.GetAllManagerGoods;

public record GetAllManagerGoodsQuery(
    List<long> OrganizationIds
    ) : IQuery<List<long>>;