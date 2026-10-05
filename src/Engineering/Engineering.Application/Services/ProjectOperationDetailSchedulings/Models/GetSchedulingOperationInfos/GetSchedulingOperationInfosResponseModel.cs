namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.GetSchedulingProjectOperations;

public record GetSchedulingOperationInfosResponseModel
{
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
}
