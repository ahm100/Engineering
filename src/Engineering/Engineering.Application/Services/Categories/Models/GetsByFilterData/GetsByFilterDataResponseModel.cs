using Engineering.Application.Services.Categories.Models.CategoryModels;

namespace Engineering.Application.Services.Categories.Models.GetsByFilterData;

public record GetsByFilterDataResponseModel
{
    public long Id { get; set; }
    public string AlternativeId { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public List<BranchModel>? Branchs { get; set; }
}