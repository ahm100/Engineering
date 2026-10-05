using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductHistories;

public class GetFilteredFiduciaryProductHistoriesModel
{
    public long Id { get; set; }
    public long? ThirdPartyId { get; set; }
    public string? ThirdParty { get; set; } = string.Empty;
    public long? RequestNumber { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public FiduciaryProductStatus Status { get; set; }
    public string StatusTitle => Status.GetEnumDescription();
    public string? Description { get; set; } = string.Empty;
    public string? StatusDescription { get; set; } = string.Empty;
    public string? LastDescription { get; set; } = string.Empty;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
}
