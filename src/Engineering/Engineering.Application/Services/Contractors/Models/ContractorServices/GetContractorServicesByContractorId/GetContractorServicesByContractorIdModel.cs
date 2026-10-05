namespace Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorServicesByContractorId;

public record GetContractorServicesByContractorIdModel
{
    public long Id { get; set; }
    public GetContractorServicesByContractorIdServiceInfoModel? ServiceInfo { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}

