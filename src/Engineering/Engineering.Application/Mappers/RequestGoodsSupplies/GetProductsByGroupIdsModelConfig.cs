using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.GetProductByGroupIds;

namespace Engineering.Application.Mappers.RequestGoodsSupplies;

public class GetProductsByGroupIdsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetProductsByGroupIdsModel, GetGoodsSupplyDetailProductsModelDetail>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Code, s => s.Code)
            .Map(d => d.Description, s => s.Desciption)
            .Map(d => d.Measure, s => s.MeasureUnitTitle)
            .Map(d => d.Urls, s => s.Urls)
            .Map(d => d.Brand, s => s.BrandName)
            .Map(d => d.BrandModel, s => s.BrandModelName)
            .Map(d => d.IsActive, s => s.IsActive)
            .Map(d => d.IsActive, s => s.IsActive);
    }
}