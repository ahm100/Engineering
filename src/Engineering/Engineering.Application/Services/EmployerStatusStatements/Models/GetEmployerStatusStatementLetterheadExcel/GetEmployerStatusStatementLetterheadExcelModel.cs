
namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementLetterheadExcel;

public record GetEmployerStatusStatementLetterheadExcelModel
{
    public long Id { get; set; }
    public Domain.Entities.EmployerStatusStatements.Enums.EmployerStatusStatementStatus SendStatusType { get; set; }
    public string SendStatusTypeTitle => SendStatusType.GetEnumDescription();
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime Created { get; set; }
    public string? StatusStatementCode { get; set; } = string.Empty;
    public string? LastEmployerStatusStatementCode { get; set; } = string.Empty;
    public long? EmployerId { get; set; }
    public string? EmployerName { get; set; }
    public long EmployerContractId { get; set; }
    public string? EmployerContractCode { get; set; }
    public string? Description { get; set; }
    public long CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectCode { get; set; }
    public decimal PercentageOfWorkDone { get; set; }
    public decimal CalculatedAmount { get; set; }
    public long CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
    public required List<GetEmployerStatusStatementLetterheadExcelProjectOperation> ProjectOperations { get; set; }

}

public record GetEmployerStatusStatementLetterheadExcelProjectOperation
{
    public long Id { get; set; }
    public long ProjectOperationId { get; set; }
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public long EmployerContractId { get; set; }
    public string? EmployerContractCode { get; set; }
    public long UnitOfMeasurementId { get; set; }
    public string? UnitOfMeasurementName { get; set; }
    public long OperationInfoId { get; set; }
    public string? OperationInfoName { get; set; }
    public string? OperationInfoCode { get; set; }
    public decimal Workload { get; set; }
    public decimal DoneWorkVolume => DailyProjectOperationDetails.Sum(x => x.Volume);
    public string? Description { get; set; }
    public List<GetEmployerStatusStatementDailyProjectOperationDetail>? DailyProjectOperationDetails { get; set; }
}

public record GetEmployerStatusStatementDailyProjectOperationDetail
{
    public long Id { get; set; }
    public long ProjectOperationId { get; set; }
    public long ProjectOperationDetailId { get; set; }
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public long OperationInfoId { get; set; }
    public string? OperationInfoName { get; set; }
    public string? OperationInfoCode { get; set; }
    public long OperationLocationId { get; set; }
    public string? PublicName { get; set; } = string.Empty;
    public string? PrivateName { get; set; } = string.Empty;
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public decimal Number { get; set; }
    public decimal Volume => Length * Width * Height * Weight * Number;
}
