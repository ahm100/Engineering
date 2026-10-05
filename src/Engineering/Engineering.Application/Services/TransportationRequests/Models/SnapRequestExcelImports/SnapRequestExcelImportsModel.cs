namespace Engineering.Application.Services.TransportationRequests.Models.SnapRequestExcelImports;

public record SnapRequestExcelImportsModel
{
    public string StartDate { get; set; } = string.Empty;
    public string? StartTime { get; set; }
    public string? SnapRequesterCode { get; set; }
    public string? SnapRequesterPhoneNumber { get; set; }
    public string TripCode { get; set; } = string.Empty;
    public string CostCenterCodes { get; set; } = string.Empty;
    public string? ProjectCodes { get; set; } = string.Empty;
    public string? TransportationCostGroupCode { get; set; }
    public string? TransportationCostCategoryCode { get; set; }
    public string? StartingCityCode { get; set; } = string.Empty;
    public string? DestinationCityCode { get; set; } = string.Empty;
    public string? SecondDestinationCityCode { get; set; }
    public string StartingCityAddress { get; set; } = string.Empty;
    public string DestinationAddress { get; set; } = string.Empty;
    public string? SecondDestinationAddress { get; set; } = string.Empty;
    public string? FareAmount { get; set; }
    public string? StopRate { get; set; }
    public string? Description { get; set; }
    public bool ReturnToStart { get; set; } = false;
    public bool PersonalPayment { get; set; } = false;
    public string? RecipientName { get; set; } = string.Empty;
}

public record SnapRequestDates(DateTime StartDate, DateTime EndDate);
