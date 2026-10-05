namespace Engineering.Application.Services.ProjectOperations.Models.GetsTotalProjectOperationReporting;

public record GetsTotalProjectOperationReportingResponse
{
    public decimal TotalFinalAmounts { get; set; } = 0;
    public decimal TotalDeductionAmounts { get; set; } = 0;
    public decimal TotalAmounts => TotalFinalAmounts - TotalDeductionAmounts;
    public decimal DailyFinalAmounts { get; set; } = 0;
    public decimal TotalRemainingAmount => TotalAmounts - DailyFinalAmounts;
    public decimal ProjectOperationWorkload { get; set; } = 0;
    public decimal TotalRemainingWorkLoad => ProjectOperationWorkload - TotalAmounts;
    [JsonIgnore]
    public List<GetsTotalProjectOperationReportingResponseModel>? Models { get; set; }
}

public record GetsTotalProjectOperationReportingResponseModel
{
    public decimal ProjectOperationWorkload { get; set; } = 0;
    [JsonIgnore]
    public List<GetsTotalDeductionModel>? Deductions { get; set; }
    [JsonIgnore]
    public List<GetsTotalDailyModel>? Dailies { get; set; }
    [JsonIgnore]
    public List<GetsTotalDetailModel>? Details { get; set; }
}

public record GetsTotalDeductionModel
{
    public long Id { get; set; }
    public decimal Width { get; set; } = 0;
    public decimal Length { get; set; } = 0;
    public decimal Height { get; set; } = 0;
    public decimal Number { get; set; } = 0;
    public decimal Weight { get; set; } = 0;
    public decimal FinalAmount => Width * Length * Height * Number * Weight;
}
public record GetsTotalDailyModel
{
    public long Id { get; set; }
    public decimal Width { get; set; } = 0;
    public decimal Length { get; set; } = 0;
    public decimal Height { get; set; } = 0;
    public decimal Number { get; set; } = 0;
    public decimal Weight { get; set; } = 0;
    public decimal FinalAmount => Width * Length * Height * Number * Weight;
}
public record GetsTotalDetailModel
{
    public long Id { get; set; }
    public decimal Width { get; set; } = 0;
    public decimal Length { get; set; } = 0;
    public decimal Height { get; set; } = 0;
    public decimal Number { get; set; } = 0;
    public decimal Weight { get; set; } = 0;
    public decimal FinalAmount => Width * Length * Height * Number * Weight;
}