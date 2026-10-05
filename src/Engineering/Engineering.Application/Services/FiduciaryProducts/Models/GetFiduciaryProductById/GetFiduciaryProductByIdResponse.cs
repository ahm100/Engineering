using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProducts.Models.GetFiduciaryProductById;

public record GetFiduciaryProductByIdResponse
{
    public long Id { get; set; }
    public long? RequestNumber { get; set; }
    public GetFiduciaryProductByIdCostCenterModel? CostCenter { get; set; }
    public GetFiduciaryProductByIdProjectModel? Project { get; set; }
    public GetFiduciaryProductByIdProjectOperationModel? ProjectOperation { get; set; }
    public GetFiduciaryProductByIdThirdPartyModel? ThirdParty { get; set; }
    public string? LastDescription { get; set; }
    public string? StatusDescription { get; set; }
    public string? Description { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public FiduciaryProductStatus? Status { get; set; }
    public string? StatusTitle => Status?.GetEnumDescription();
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public bool IsEditable
    {
        get
        {
            if (Status is FiduciaryProductStatus.New || Status is FiduciaryProductStatus.Rejected)
                return true;
            else
                return false;
        }
    }
    public List<GetFiduciaryProductByIdDetailModel>? Details { get; set; } = new();
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}

public record GetFiduciaryProductByIdDetailModel
{
    public long Id { get; set; }
    public long? ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public string? ProductCode { get; set; } = string.Empty;
    public string? ProductBrand { get; set; } = string.Empty;
    public string? ProductBrandModel { get; set; } = string.Empty;
    public int LoanCount { get; set; }
    public int LoanDays { get; set; }
    public int? ConfirmedLoanDays { get; set; }
    public long? MeasureunitId { get; set; }
    public string? MeasureunitName { get; set; } = string.Empty;
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; } = string.Empty;
    public decimal DailyLateFine { get; set; }
    public decimal? ConfirmedDailyLateFine { get; set; }
    public string? ProductDescription { get; set; }
    public string? LastDescription { get; set; }
    public string? StatusDescription { get; set; }
    public FiduciaryProductDetailStatus? Status { get; set; }
    public string? StatusTitle => Status?.GetEnumDescription();
    public bool IsEditable
    {
        get
        {
            if (Status is FiduciaryProductDetailStatus.New || Status is FiduciaryProductDetailStatus.Rejected)
                return true;
            else
                return false;
        }
    }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public List<GetFiduciaryProductByIdDetailManagementModel>? Managements { get; set; }
}

public record GetFiduciaryProductByIdDetailManagementModel
{
    public long Id { get; set; }
    public long? InvoiceId { get; set; }
    public int ConfirmedLoanCount { get; set; }
    public GetFiduciaryProductByIdDetailManagementWarehouseModel? Warehouse { get; set; }
    public FiduciaryProductDetailManagementStatus? Status { get; set; }
    public string? StatusTitle => Status?.GetEnumDescription();
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public List<GetFiduciaryProductByIdDetailManagementReturnedModel>? Returns { get; set; }
}

public record GetFiduciaryProductByIdDetailManagementReturnedModel()
{
    public long Id { get; set; }
    public long? InvoiceId { get; set; }
    public string? Description { get; set; }
    public int? ReturnCount { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string? ReturnDateShamsi => TimeCalculator.ConvertToShamsi(ReturnDate);
    public int? LateDay { get; set; }
    public decimal? LateFine { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public FiduciaryProductDetailReturnType? Type { get; set; }
    public string? TypeTitle => Type?.GetEnumDescription();
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
}

public record GetFiduciaryProductByIdThirdPartyModel
{
    public long Id { get; set; }
    public string? FullName { get; set; } = string.Empty;
}

public record GetFiduciaryProductByIdProjectModel
{
    public long Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
}

public record GetFiduciaryProductByIdProjectOperationModel
{
    public long Id { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
}

public record GetFiduciaryProductByIdDetailManagementWarehouseModel
{
    public long Id { get; set; }
    public string? Code { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public double? RealQuantity { get; set; }
    public int ConfirmedLoanCount { get; set; }
}

public record GetFiduciaryProductByIdCostCenterModel
{
    public long Id { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
}
