using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnByDetailId;

public record GetFiduciaryProductDetailReturnByDetailIdModel
{
    public long FiduciaryProductDetailReturnId { get; set; }
    public FiduciaryProductDetailReturnType? Type { get; set; }
    public string? TypeDescription => Type?.GetEnumDescription();
    public DateTime? ReturnDate { get; set; }
    public string? ReturnDateShamsi => TimeCalculator.ConvertToShamsi(ReturnDate);
    public int? LateDay { get; set; }
    public decimal? LateFine { get; set; }
    public int? ReturnCount { get; set; }
    public long? CurrencyId { get; set; }
    public string? Description { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public List<string>? Documents { get; set; }

}