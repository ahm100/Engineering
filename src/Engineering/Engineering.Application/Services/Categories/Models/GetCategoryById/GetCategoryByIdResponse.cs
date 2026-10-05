namespace Engineering.Application.Services.Categories.Models.GetCategoryById;

public record GetCategoryByIdResponse
{
    public long Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryCode { get; set; } = string.Empty;
    public string AlternativeId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool HaveChild { get; set; }
    public int ChildCount { get; set; }
    public Guid? PreferentialReferenceCode { get; set; }
}