namespace Engineering.Application.Services.Actions.Contracts.GetActionById;

public record GetActionByIdResponse
{
    public long Id { get; set; }
    public string ActionName { get; set; } = string.Empty;
    public string ActionCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}