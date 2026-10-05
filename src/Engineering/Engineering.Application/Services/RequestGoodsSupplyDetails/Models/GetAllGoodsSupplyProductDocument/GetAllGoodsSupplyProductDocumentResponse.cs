
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetAllGoodsSupplyProductDocument;

public record GetAllGoodsSupplyProductDocumentResponse(
    List<GetAllGoodsSupplyProductDocumentResponseModel>? GoodsSupplyProductDocuments
    );

public record GetAllGoodsSupplyProductDocumentResponseModel
{
    public long? Id { get; set; }
    public List<string>? Documents { get; set; }
}
