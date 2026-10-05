using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Application.Services.ContractorStatusStatements.Models.GetFilteredContractorStatusStatement;

public record GetFilteredContractorStatusStatementRequest() : IHttpRequest
{
    public long? ContractorId { get; set; }
    public long? ProjectId { get; set; }
    public long? CostCenterId { get; set; }
    public long? ProjectManagerId { get; set; }
    public long? ContractorContractHeaderId { get; set; }
    public long? CreatorId { get; set; }
    public string? ContractNumber { get; set; }
    public string? Code { get; set; }
    public string? ContractorFilter { get; set; }
    public string? ManagerAmount { get; set; }
    public string? ManagerDescription { get; set; }
    public List<CSSStatus>? Statuses { get; set; }
    public bool IsPayment { get; set; }
    public bool? MultiPayment { get; set; }
    public bool? IsPrimaryManagerConfirmed { get; set; }
    public bool? IsFinalManagerConfirmed { get; set; }
    public bool? IsManager { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? FilterData { get; set; }
    public string[]? OrderBy { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }

}