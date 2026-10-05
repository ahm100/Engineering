using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;

public record GetFiduciaryProductManagementByIdModel
{
    public long Id { get; set; }
    public long FiduciaryProductDetailId { get; set; }
    public long? ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public string? ProductCode { get; set; } = string.Empty;
    public string? BrandName { get; set; } = string.Empty;
    public string? BrandModel { get; set; } = string.Empty;
    public int LoanCount { get; set; }
    public int LoanDays { get; set; }
    public int? ConfirmedLoanDays { get; set; }
    public long? MeasureunitId { get; set; }
    public string? MeasureunitName { get; set; } = string.Empty;
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; } = string.Empty;
    public decimal DailyLateFine { get; set; }
    public decimal? ConfirmedDailyLateFine { get; set; }
    public string? Description { get; set; }
    public FiduciaryProductDetailStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public long? WarehouseId { get; set; }
    public string? WarehouseCode { get; set; } = string.Empty;
    public string? WarehouseName { get; set; } = string.Empty;
    public DateTime? DeliverDate { get; set; }
    public string? DeliverDateshamsi => TimeCalculator.ConvertToShamsi(DeliverDate);
}
