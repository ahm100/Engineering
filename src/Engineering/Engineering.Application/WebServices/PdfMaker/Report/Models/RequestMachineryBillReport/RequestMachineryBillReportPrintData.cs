namespace Financial.Application.WebServices.PdfMaker.Report.Models.RequestMachineryBillReport;
public record RequestMachineryBillReportPrintData
{
    public string? RequestNumber { get; set; }
    public string? BillNumber { get; set; }
    public string? CompanyNameFa { get; set; }
    public string? Contractor { get; set; }
    public string? Supplier { get; set; }
    public string? MachineryGroupName { get; set; }
    public string? MachineryName { get; set; }
    public string? CostCenterName { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectOperations { get; set; }
    public string? QrCodeUrl { get; set; }
    public string? BillDate { get; set; }
    public string? RequestCreator { get; set; }
    public string? DriverFullName { get; set; }
    public string? NumberPlates { get; set; }
    public string? FromTime { get; set; }
    public string? ToTime { get; set; }
    public string? OperationDuration { get; set; }
    public string? UnitPrice { get; set; }
    public string? TotalPrice { get; set; }
    public string? Description { get; set; }
}
