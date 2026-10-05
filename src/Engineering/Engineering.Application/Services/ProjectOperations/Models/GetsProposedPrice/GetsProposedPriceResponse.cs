namespace Engineering.Application.Services.ProjectOperations.Models.GetsProposedPrice;

public record GetsProposedPriceResponse(
    List<GetsProposedPriceModel> Data,
    int RowCount);
