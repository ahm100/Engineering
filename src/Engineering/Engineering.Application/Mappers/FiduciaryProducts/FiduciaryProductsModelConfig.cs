using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProducts;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductsExcelExporter;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Mappers.FiduciaryProducts;

public class FiduciaryProductsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FiduciaryProduct, GetFilteredFiduciaryProductsExcelExporterResponseModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.CompanyId, s => s.CompanyId)
            .Map(d => d.Created, s => TimeCalculator.ConvertToShamsi(s.Created))
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

        config.NewConfig<FiduciaryProduct, GetFiduciaryProductManagementByIdResponse>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.RequestNumber, s => s.Id)
            .Map(d => d.Created, s => s.Created)
            .Map(d => d.Status, s => s.Status)
            .Map(d => d.StatusTitle, s => s.Status.GetEnumDescription())
            .Map(d => d.Description, s => s.Description)
           .Map(d => d.CostCenterId,
           s => s.Project.ProjectCostCenters
           .Select(x => (long?)x.CostCenterId)
           .FirstOrDefault())
           .Map(d => d.CostCenterName,
           s => s.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterName)
           .FirstOrDefault())
            .Map(d => d.ProjectName, s => s.Project.ProjectName)
            .Map(d => d.OperationInfoName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
            .Map(d => d.Created, s => s.Project.Id);
    }
}