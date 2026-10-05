namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetFilteredRequestGoodsSuppliesReports;

public record GetFilteredRequestGoodsSuppliesReportsResponse(
    List<GetFilteredRequestGoodsSuppliesReportsModel> Data,
    int RowCount
    );

