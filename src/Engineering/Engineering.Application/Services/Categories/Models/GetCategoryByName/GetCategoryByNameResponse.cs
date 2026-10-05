namespace Engineering.Application.Services.Categories.Models.GetCategoryByName;

public record GetCategoryByNameResponse
{
    public long Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryCode { get; set; } = string.Empty;
    public string AlternativeId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}