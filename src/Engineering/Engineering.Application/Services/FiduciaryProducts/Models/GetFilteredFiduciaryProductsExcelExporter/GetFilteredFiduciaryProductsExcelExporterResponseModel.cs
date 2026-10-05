using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductsExcelExporter;

public record GetFilteredFiduciaryProductsExcelExporterResponseModel
{
    public long Id { get; set; }
    public long? RequestNumber { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public long? ProjectOperationId { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public long? ThirdPartyId { get; set; }
    public string? ThirdParty { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? LastDescription { get; set; } = string.Empty;
    public string? StatusDescription { get; set; } = string.Empty;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public FiduciaryProductStatus? Status { get; set; }
    public string StatusTitle => Status?.GetEnumDescription();
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}

public record GetFilteredFiduciaryProductDetailsExcelExporterModel
{
    public long Id { get; set; }
    public long FiduciaryProductId { get; set; }
    public long? ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public string? ProductCode { get; set; } = string.Empty;
    public string? ProductBrand { get; set; } = string.Empty;
    public string? ProductBrandModel { get; set; } = string.Empty;
    public int? LoanCount { get; set; }
    public int? LoanDays { get; set; }
    public int? ConfirmedLoanDays { get; set; }
    public long? MeasureunitId { get; set; }
    public string? MeasureunitName { get; set; } = string.Empty;
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; } = string.Empty;
    public decimal? DailyLateFine { get; set; }
    public decimal? ConfirmedDailyLateFine { get; set; }
    public string? ProductDescription { get; set; }
    public string? LastDescription { get; set; }
    public string? StatusDescription { get; set; }
    public FiduciaryProductDetailStatus? Status { get; set; }
    public string? StatusTitle => Status?.GetEnumDescription();
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
}