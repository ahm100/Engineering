using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetContractsByProjectId;

public record GetContractsByProjectIdResponse(
    List<GetContractsByProjectIdModel> Data,
    int RowCount
    );

public class GetContractsByProjectIdModel
{
    public long Id { get; set; }
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; }
    public long ContractNumber { get; set; }
    public DateTime StartDate { get; set; }
    public string StartDateShamsi => StartDate.ToShamsi();
    public DateTime EndDate { get; set; }
    public string EndDateShamsi => EndDate.ToShamsi();
    public decimal? ContractPrice { get; set; }
    public ContractorContractType ContractType { get; set; }
    public string ContractTypeDescription => ContractType.GetEnumDescription();
}

public class ContractsPdfModel
{
    public string Number { get; set; } = string.Empty;
    public string? Contractor { get; set; }
    public long ContractNumber { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string? ContractPrice { get; set; }
    public string ContractType { get; set; } = string.Empty;
}