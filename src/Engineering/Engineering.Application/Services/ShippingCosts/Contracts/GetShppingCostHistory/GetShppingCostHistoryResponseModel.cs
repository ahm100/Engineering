namespace Engineering.Application.Services.ShippingCosts.Contracts.GetShppingCostHistory;

public record GetShippingCostHistoryResponseModel
{
    public long Id { get; set; }
    public long ShippingCostId { get; set; }
    public long? MachinTypeId { get; set; }
    public string? MachinTypeName { get; set; }
    public string? MachinTypeCode { get; set; }
    public long? SourceCityId { get; set; }
    public string? SourceCity { get; set; }
    public long? DestinationCityId { get; set; }
    public string? DestinationCity { get; set; }
    public long? RegionId { get; set; }
    public string? Region { get; set; }
    public int? Count { get; set; }
    public decimal? LoadWeight { get; set; }
    public decimal? Price { get; set; }
    public decimal? Tax { get; set; }
    public bool? IsActive { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public DateTime? FromDate { get; set; }
    public string? FromDateShamsi => TimeCalculator.ConvertToShamsi(FromDate);
    public DateTime? ToDate { get; set; }
    public string? ToDateShamsi => TimeCalculator.ConvertToShamsi(ToDate);
    public long? ShippingThirdPartyId { get; set; }
    public string? ShippingThirdParty { get; set; }
    public long? ThirdPartyCompanyId { get; set; }
    public string? ThirdPartyCompanyCode { get; set; }
    public string? ThirdPartyCompanyNameFa { get; set; }
    public string? ThirdPartyCompanyNameEn { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? Latitude { get; set; }
}