namespace Engineering.Application.Services.Actions.Contracts.GetsActiveActions;

public record GetsActiveActionsResponse(
    List<GetsActiveActionsModel> Data,
    int RowCount);

public record GetsActiveActionsModel
{
    public long Id { get; set; }
    public string ActionName { get; set; } = string.Empty;
    public string ActionCode { get; set; } = string.Empty;
}