using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetFilteredRequestMachineryStatusStatement;

public record GetFilteredRequestMachineryStatusStatementModel
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
    public DateTime Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public List<GetFilteredRequestMachineryStatusStatementDetailModel>? Detail { get; set; } = new();
}
public record GetFilteredRequestMachineryStatusStatementDetailModel()
{
    public long Id { get; set; }
    public long StatusStatementId { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenter { get; set; } = string.Empty;
    public long? MachineryId { get; set; }
    public string? Machinery { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? Project { get; set; } = string.Empty;
    public string? ProjectOperations { get; set; } = string.Empty;
    public string? ProjectOperationDetails { get; set; } = string.Empty;
    public long? RequestMachineryId { get; set; }
    public string? RequestMachineryNumber { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public DateTime? FromDate { get; set; }
    public string? FromDateShamsi => TimeCalculator.ConvertToShamsi(FromDate);
    public DateTime? ToDate { get; set; }
    public string? ToDateShamsi => TimeCalculator.ConvertToShamsi(ToDate);
    public RequestMachineryStatusStatementUnit Unit { get; set; }
    public string UnitDescription => Unit.GetEnumDescription();
    public decimal RequestedCount { get; set; }
    public decimal FinalPrice { get; set; }
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; }
    public string? TimeRequired { get; set; }
    public long? OperatorId { get; set; }
    public string? Operator { get; set; }
}