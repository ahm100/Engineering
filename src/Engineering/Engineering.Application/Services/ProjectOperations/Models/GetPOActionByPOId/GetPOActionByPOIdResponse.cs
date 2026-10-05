namespace Engineering.Application.Services.ProjectOperations.Models.GetPOActionByPOId;

public record GetPOActionByPOIdResponse(
    List<GetPOActionByPOIdModel> Data,
    int RowCount);

public record GetPOActionByPOIdModel
{
    public long Id { get; set; }
    public long OInfoActionId { get; set; }
    public decimal? Price { get; set; }
    public long ActionId { get; set; }
    public string ActionName { get; set; } = string.Empty;
    public string ActionCode { get; set; } = string.Empty;
}