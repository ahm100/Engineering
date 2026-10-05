namespace Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetGoodsManagerAssignmentHistory;

public record GetGoodsManagerAssignmentHistoryResponse(
    List<GetGoodsManagerAssignmentHistoryModel> Data,
    int RowCount);

public record GetGoodsManagerAssignmentHistoryModel(
    long Id,
    long? OrganizationId,
    long? ProductId,
    string? Description,
    long CreatorId,
    DateTime Created,
    string? CreatedShamsi) : IUserAuditable
{
    public string? Creator { get; set; }
    public long? UpdaterId { get; set; }
    public string? Updater { get; set; }
}