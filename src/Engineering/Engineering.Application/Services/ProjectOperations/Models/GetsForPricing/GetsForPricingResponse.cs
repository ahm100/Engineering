
namespace Engineering.Application.Services.ProjectOperations.Models.GetsForPricing;

public record GetsForPricingResponse(
    List<GetsForPricingModel> Data,
    int RowCount);
