namespace Engineering.Application.Services.Actions.Contracts.GetFilteredActions;

public record GetFilteredActionsResponse(
    List<GetFilteredActionsModel> Data,
    int RowCount);

public record GetFilteredActionsModel
{
    public long Id { get; set; }
    public string ActionName { get; set; } = string.Empty;
    public string ActionCode { get; set; } = string.Empty;
}