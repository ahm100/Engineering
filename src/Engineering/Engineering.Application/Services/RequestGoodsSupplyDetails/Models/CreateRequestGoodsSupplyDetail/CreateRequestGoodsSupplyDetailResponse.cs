namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.CreateRequestGoodsSupplyDetail;

public class CreateRequestGoodsSupplyDetailResponse
{
    public List<long> RequestGoodsSupplyDetailIds { get; set; } = new();
}

public record CreateRequestGoodsSupplyDetailResponseModel(
    long Id
    );
