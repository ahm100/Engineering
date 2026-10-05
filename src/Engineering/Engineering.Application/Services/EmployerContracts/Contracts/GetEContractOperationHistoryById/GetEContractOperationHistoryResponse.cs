namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEContractOperationHistory;
public record GetEContractOperationHistoryResponse(
    List<GetEContractOperationHistoryModel> Data,
    int RowCount);

public class GetEContractOperationHistoryModel
{
    public long Id { get; set; }
    public decimal? Price { get; set; }
    public decimal Workload { get; set; }
    public string? Description { get; set; }
};