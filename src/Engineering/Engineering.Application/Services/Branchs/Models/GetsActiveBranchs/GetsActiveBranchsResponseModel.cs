namespace Engineering.Application.Services.Branchs.Models.GetsActiveBranchs;

public record GetsActiveBranchsResponseModel
{
    public long Id { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public string BranchCode { get; set; } = string.Empty;
}