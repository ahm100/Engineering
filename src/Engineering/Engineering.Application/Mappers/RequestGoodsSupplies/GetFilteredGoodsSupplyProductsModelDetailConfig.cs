using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsHistoryById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Application.Mappers.RequestGoodsSupplies;

public class GetFilteredGoodsSupplyProductsModelDetailConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, GetGoodsSupplyDetailProductsModelDetail>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.Name, s => s.Name)
            .Map(d => d.Code, s => s.Code)
            .Map(d => d.Measure, s => s.Group.Measure)
            .Map(d => d.BrandId, s => s.BrandId)
            .Map(d => d.Brand, s => s.Brand)
            .Map(d => d.BrandModelId, s => s.BrandModelId)
            .Map(d => d.BrandModel, s => s.BrandModel)
            .Map(d => d.IsActive, s => s.IsActive)
            .Map(d => d.Features, s => s.Features);

        config.NewConfig<RequestGoodsSupplyHistory, GetRequestGoodsHistoryByIdDetailModel>()
            .Map(d => d.Created, s => TimeCalculator.DatePiker(s.Created))
            .Map(d => d.Status, s => s.Status)
            .Map(d => d.StatusDescription, s => s.Status.GetEnumDescription())
            .Map(d => d.CreatorId, s => s.CreatorId)
            .Map(d => d.Description, s => s.Description);

        config.NewConfig<ProductFeatureResponse, ProductFeature>()
            .Map(d => d.FeatureId, s => s.FeatureId)
            .Map(d => d.FeatureName, s => s.FeatureName)
            .Map(d => d.Value, s => s.Value);
    }
}
