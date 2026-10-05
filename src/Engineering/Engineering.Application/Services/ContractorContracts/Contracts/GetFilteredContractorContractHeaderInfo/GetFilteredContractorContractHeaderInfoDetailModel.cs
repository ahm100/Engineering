namespace Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractHeaderInfo;

public record GetFilteredContractorContractHeaderInfoDetailModel
{
    public long ProjectOperationId { get; set; }
    public string OperationInfoCode { get; set; } = string.Empty;
    public string OperationInfoName { get; set; } = string.Empty;
    public decimal OperationWorkload { get; set; }
    public string? OperationUnitOfMeasurement { get; set; } = string.Empty;
    public long ProjectOperationDetailServiceId { get; set; }
    public string ServiceInfoName { get; set; } = string.Empty;
    public decimal ServiceInfoVolume { get; set; }
    public string? ServiceInfoUnitOfMeasurement { get; set; } = string.Empty;
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public string OperationLocationPublicCode { get; set; } = string.Empty;

}
