namespace Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementByIds;

public record GetAdvertisementByIdsResponse(
    List<GetAdvertisementByIdsModel> Data,
    int RowCount);

public class GetAdvertisementByIdsModel
{
    public long Id { get; set; }
    public string TitleFa { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string DescriptionFa { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
    public string TechnicalCode { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string? CreatedShamsi => Created.ToShamsi();
    public long CreatorId { get; set; }
    public string? Creator { get; set; }
    public bool IsActive { get; set; }
    public List<string>? DocumentUrls { get; set; }
}