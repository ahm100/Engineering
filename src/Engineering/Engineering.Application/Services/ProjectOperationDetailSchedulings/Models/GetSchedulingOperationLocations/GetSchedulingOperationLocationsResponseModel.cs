namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.GetSchedulingProjectOperationDetails;

public record GetSchedulingOperationLocationsResponseModel
{
    public long OperationLocationId { get; set; }
    public string PublicName { get; set; } = string.Empty;
    public string PublicCode { get; set; } = string.Empty;
}
