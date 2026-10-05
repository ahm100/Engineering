namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryManagementById;

public record GetRequestMachineryManagementByIdProjectOperationDetailModel
{
    public long Id { get; set; }
    public long ProjectOperationDetailId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
}