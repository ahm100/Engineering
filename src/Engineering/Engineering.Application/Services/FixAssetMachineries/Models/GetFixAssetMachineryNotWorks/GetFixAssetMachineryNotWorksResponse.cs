namespace Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryNotWorks;

public record GetFixAssetMachineryNotWorksResponse(
    List<GetFixAssetMachineryNotWorksModel> Data,
    int RowCount);

public record GetFixAssetMachineryNotWorksModel
{
    public long Id { get; set; }
    public long MachineryId { get; set; }
    public string MachineryName { get; set; } = string.Empty;
    public string MachineryCode { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public string? StartDateShamsi => TimeCalculator.ConvertToShamsi(StartDate);
    public TimeSpan? StartTime { get; set; }
    public DateTime EndDate { get; set; }
    public string? EndDateShamsi => TimeCalculator.ConvertToShamsi(EndDate);
    public TimeSpan? EndTime { get; set; }
    public string? Description { get; set; } = string.Empty;
    public List<GetsFixAssetNotWorkDocumentResponseModel>? Documents { get; set; }
}

public record GetsFixAssetNotWorkDocumentResponseModel(long Id, string Url);