using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByIds;

namespace Engineering.Application.Mappers.RequestGoodsSupplies;

public class GetGoodsSupplyProductsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FilteredGroup, GetGoodsSupplyDetailProductsModel>()
            .Map(d => d.GroupId, s => s.Id)
            .Map(d => d.GroupName, s => s.Name)
            .Map(d => d.GroupCode, s => s.Code)
            .Map(d => d.Urls, s => s.Urls)
            .Map(d => d.MeasureUnitId, s => s.MeasureUnitId)
            .Map(d => d.GroupMeasure, s => s.MeasureUnitName);

    }
}
