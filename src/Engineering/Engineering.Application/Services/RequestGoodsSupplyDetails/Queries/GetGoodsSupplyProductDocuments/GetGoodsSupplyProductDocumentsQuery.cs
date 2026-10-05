namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetGoodsSupplyProductDocuments;

public record GetGoodsSupplyProductDocumentsQuery(
    long Id
    ) : IQuery<List<string>?>;
