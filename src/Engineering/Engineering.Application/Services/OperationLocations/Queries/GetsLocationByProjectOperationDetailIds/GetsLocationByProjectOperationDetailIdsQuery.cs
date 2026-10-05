
namespace Engineering.Application.Services.OperationLocations.Queries.GetsLocationByProjectOperationDetailIds;

public record GetsLocationByProjectOperationDetailIdsQuery(
    List<long> ProjectOperationDetailIds
    ) : IQuery<DataResult<List<GetsLocationByProjectOperationDetailIdsModel>>>;


public record GetsLocationByProjectOperationDetailIdsModel
{
    public long Id { get; set; }
    public long ProjectOperationDetailId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}
