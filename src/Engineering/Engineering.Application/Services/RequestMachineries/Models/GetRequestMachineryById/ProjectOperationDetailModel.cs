namespace Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryById;

public record ProjectOperationDetailModel
{
    public long Id { get; set; }
    public long ProjectOperationDetailId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
}