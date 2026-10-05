namespace Engineering.Application.Services.Contracts.Contracts.GetAvailableContractTypeDetailSources;

public class GetAvailableContractTypeDetailSourcesModel
{
    public long SourceId { get; set; }

    public long ProjectOperationId { get; set; }
    public string ProjectOperationName { get; set; } = string.Empty;
    public string ProjectOperationCode { get; set; } = string.Empty;

    public long ProjectOperationDetailId { get; set; }
    public string? ProjectOperationDetailCode { get; set; }
    public string? ProjectOperationDetailDescription { get; set; }

    public string SourceTitle { get; set; } = string.Empty;
    public string? SourceCode { get; set; }
    public string? SourceDescription { get; set; }

    public decimal SourceQuantity { get; set; }
    public decimal AvailableQuantity { get; set; }

    public long? UnitOfMeasurementId { get; set; }

    public decimal? EstimatedUnitPrice { get; set; }

    public bool IsUnitOfMeasurementReadOnly { get; set; }
}
