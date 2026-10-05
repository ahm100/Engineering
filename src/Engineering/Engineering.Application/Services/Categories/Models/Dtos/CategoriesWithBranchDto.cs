namespace Engineering.Application.Services.Categories.Models.Dtos;

public record CategoriesWithBranchDto(List<CategoryWithBranchDto> Data, int RowCount);