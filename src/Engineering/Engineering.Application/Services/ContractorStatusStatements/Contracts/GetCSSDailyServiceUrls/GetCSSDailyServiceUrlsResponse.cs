
namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSDailyServiceUrls;

public record GetCSSDailyServiceUrlsResponse(
    List<GetCSSDailyServiceUrlsModel> Data,
    int RowCount
);

public record GetCSSDailyServiceUrlsModel
{
    public long Id { get; set; }
    public string? ServiceInfoName { get; set; }
    public DateTime? CreatedDate { get; set; }
    public decimal? Volume { get; set; }
    public List<string>? Urls { get; set; }
}
