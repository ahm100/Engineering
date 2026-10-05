using Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractHeaderInfo;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderInfo;

public record GetFilteredContractorContractHeaderInfoResponse
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public List<GetFilteredContractorContractHeaderInfoDetailModel> Details { get; set; } = new();
    public int RowCount { get; set; }
}
