using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Mappers.FiduciaryProductManagements;

public class FiduciaryProductManagementsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FiduciaryProductDetailManagement, GetFiduciaryProductManagementByIdModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.ProductId, s => s.FiduciaryProductDetail.ProductId)
            .Map(d => d.MeasureunitId, s => s.FiduciaryProductDetail.MeasureUnitId)
            .Map(d => d.CurrencyId, s => s.FiduciaryProductDetail.CurrencyId)
            .Map(d => d.Status, s => s.FiduciaryProductDetail.Status)
            .Map(d => d.StatusDescription, s => s.FiduciaryProductDetail.Status.GetEnumDescription())
            .Map(d => d.DailyLateFine, s => s.FiduciaryProductDetail.DailyLateFine)
            .Map(d => d.LoanCount, s => s.FiduciaryProductDetail.LoanCount)
            .Map(d => d.Status, s => s.Status)
            .Map(d => d.LoanDays, s => s.FiduciaryProductDetail.LoanDays)
            .Map(d => d.Description, s => s.FiduciaryProductDetail.Description)
            .Map(d => d.ConfirmedDailyLateFine, s => s.FiduciaryProductDetail.ConfirmedDailyLateFine)
            .Map(d => d.ConfirmedLoanDays, s => s.FiduciaryProductDetail.ConfirmedLoanDays)
            .Map(d => d.WarehouseId, s => s.WarehouseId)
            .Map(d => d.FiduciaryProductDetailId, s => s.FiduciaryProductDetail.Id)
            .Map(d => d.DeliverDate, s => s.FiduciaryProductDetail.DeliverDate)
            ;

        config.NewConfig<FiduciaryProduct, GetFilteredFiduciaryProductsModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.CompanyId, s => s.CompanyId)
            .Map(d => d.Created, s => s.Created)
            .Map(d => d.CreatorId, s => s.CreatorId)
            .Map(d => d.ThirdPartyId, s => s.ThirdPartyId)
            .Map(d => d.StatusDescription, s => s.Status.GetEnumDescription())
           .Map(d => d.CostCenterId,
           s => s.Project.ProjectCostCenters
           .Select(x => (long?)x.CostCenterId)
           .FirstOrDefault())
           .Map(d => d.CostCenterName,
           s => s.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterName)
           .FirstOrDefault())
            .Map(d => d.ProjectName, s => s.Project.ProjectName)
            .Map(d => d.Status, s => s.Status)
            .Map(d => d.OperationInfoName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
            .Map(d => d.ProjectId, s => s.Project.Id)
            .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id);
    }
}