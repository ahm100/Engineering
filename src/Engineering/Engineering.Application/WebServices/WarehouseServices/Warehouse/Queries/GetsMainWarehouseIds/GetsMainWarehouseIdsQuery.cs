
namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetsMainWarehouseIds;

public record GetsMainWarehouseIdsQuery(
    ) : IQuery<DataResult<List<long>?>>;

