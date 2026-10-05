using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetFilteredRequestMachineryStatusStatement;
using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetRequestMachineryStatusStatementById;

public record GetRequestMachineryStatusStatementByIdResponse
{
    public long Id { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenter { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? Project { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public string? ContractorNickname { get; set; } = string.Empty;
    public string? ContractorIBAN { get; set; } = string.Empty;
    public DateTime? FromDate { get; set; }
    public string? FromDateShamsi => TimeCalculator.ConvertToShamsi(FromDate);
    public DateTime? ToDate { get; set; }
    public string? ToDateShamsi => TimeCalculator.ConvertToShamsi(ToDate);
    public DateTime? PaymentDate { get; set; }
    public string? PaymentDateShamsi => TimeCalculator.ConvertToShamsi(PaymentDate);
    public RequestMachineryStatusStatementStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public decimal TotalRequestedCount { get; set; }
    public decimal TotalFinalPrice { get; set; }
    public decimal? ContractorFinalPrice { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? PaymentDescription { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public long? CategoryId { get; set; }
    public string? Category { get; set; }
    public long? BranchId { get; set; }
    public string? Branch { get; set; }
    public long? SeasonId { get; set; }
    public string? Season { get; set; }
    public List<GetFilteredRequestMachineryStatusStatementDetailModel>? Detail { get; set; } = new();
}