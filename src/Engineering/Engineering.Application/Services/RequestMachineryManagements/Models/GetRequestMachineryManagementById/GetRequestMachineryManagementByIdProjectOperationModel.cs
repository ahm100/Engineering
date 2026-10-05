namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryManagementById;

public record GetRequestMachineryManagementByIdProjectOperationModel
{
    public long Id { get; set; }
    public long ProjectOperationId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
}
