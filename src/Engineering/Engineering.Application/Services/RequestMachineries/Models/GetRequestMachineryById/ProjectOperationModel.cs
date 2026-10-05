namespace Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryById;

public record ProjectOperationModel
{
    public long Id { get; set; }
    public long ProjectOperationId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public string? MeasurementName { get; set; } = string.Empty;
}
