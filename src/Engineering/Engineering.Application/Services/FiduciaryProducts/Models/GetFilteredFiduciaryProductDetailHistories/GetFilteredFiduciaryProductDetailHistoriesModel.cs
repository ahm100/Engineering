using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductDetailHistories;

public record GetFilteredFiduciaryProductDetailHistoriesModel
{
    public long Id { get; set; }
    public long? ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public string? ProductCode { get; set; } = string.Empty;
    public string? ProductBrand { get; set; } = string.Empty;
    public string? ProductBrandModel { get; set; } = string.Empty;
    public int? LoanCount { get; set; }
    public int? LoanDays { get; set; }
    public long? MeasureunitId { get; set; }
    public string? MeasureunitName { get; set; } = string.Empty;
    public string? ProductDescription { get; set; } = string.Empty;
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; } = string.Empty;
    public decimal? DailyLateFine { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public List<long>? WarehouseIds { get; set; }
    public string? Warehouse { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public FiduciaryProductDetailStatus? Status { get; set; }
    public string? StatusTitle => Status?.GetEnumDescription();
    public string? StatusDescription { get; set; }
    public string? LastDescription { get; set; }
}