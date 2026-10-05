namespace Engineering.Application.Services.ContractorMachineries.Models.GetFltrByContractorIds;

public record GetFltrByContractorIdsResponse(
    List<GetFltrByContractorIdsModel> Data,
    int RowCount);
public record GetFltrByContractorIdsModel
{
    public long Id { get; set; }
    public long ContractorId { get; set; }
    public long? MachineryGroupId { get; set; }
    public string? MachineryGroupName { get; set; } = string.Empty;
    public string? MachineryGroupCode { get; set; } = string.Empty;
    public long? MachineryId { get; set; }
    public string? MachineryName { get; set; } = string.Empty;
    public string? MachineryCode { get; set; } = string.Empty;
}