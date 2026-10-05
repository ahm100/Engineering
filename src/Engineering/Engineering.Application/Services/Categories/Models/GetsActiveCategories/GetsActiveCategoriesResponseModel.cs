namespace Engineering.Application.Services.Categories.Models.GetsActiveCategories;

public record GetsActiveCategoriesResponseModel
{
    public long Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryCode { get; set; } = string.Empty;
}