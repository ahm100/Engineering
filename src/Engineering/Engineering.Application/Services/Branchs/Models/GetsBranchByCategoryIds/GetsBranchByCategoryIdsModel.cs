namespace Engineering.Application.Services.Branchs.Models.GetsBranchByCategoryIds;

public record GetsBranchByCategoryIdsModel
{
    public long Id { get; set; }
    public long CategoryId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public string BranchCode { get; set; } = string.Empty;
    public string AlternativeId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool HaveChild { get; set; }
    public int ChildCount { get; set; }
}