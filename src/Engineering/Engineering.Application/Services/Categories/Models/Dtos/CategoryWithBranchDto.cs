using Engineering.Application.Services.Categories.Models.CategoryModels;

namespace Engineering.Application.Services.Categories.Models.Dtos;

public class CategoryWithBranchDto
{
    public long Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryCode { get; set; } = string.Empty;
    public string AlternativeId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool HaveChild { get; set; }
    public int ChildCount { get; set; }
    public Guid? PreferentialReferenceCode { get; set; }
    public List<BranchModel>? Branchs { get; set; }
}