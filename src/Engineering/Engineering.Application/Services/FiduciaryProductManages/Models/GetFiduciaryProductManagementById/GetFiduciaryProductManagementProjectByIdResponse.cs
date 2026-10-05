namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;

public record GetFiduciaryProductManagementProjectByIdResponse
{
    public long Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
}
