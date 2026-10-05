using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFilteredRequestGoodsSuppliesReports;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyExcelExporter;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyReportsExcelExporter;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Mappers.RequestGoodsSupplies;

public class RequestGoodsSuppliesModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RequestGoodsSupply, GetsRequestGoodsSupplyExcelExporterResponseModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.RequestNumber, s => $"{s.SerialNumber}-{s.Id}")
            .Map(d => d.TypeDescription, s => s.Type.GetEnumDescription())
            .Map(d => d.TypeDescription, s => s.RequestGoodsSupplyDetails.Any() ? s.RequestGoodsSupplyDetails.FirstOrDefault()!.Importance!.GetEnumDescription() : "")
            .Map(d => d.StatusDescription, s => s.Status.GetEnumDescription())

            .Map(d => d.CostCenterName,
            s => s.ProjectOperation.Project.ProjectCostCenters
            .Select(x => x.CostCenter.CostCenterName)
            .FirstOrDefault())
            .Map(d => d.ProjectName, s => s.ProjectOperation.Project.ProjectName)
            .Map(d => d.OperationInfoName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
            .Map(d => d.MeasurementId, s => s.ProjectOperation.UnitOfMeasurementId)
            .Map(d => d.Workload, s => s.ProjectOperation.Workload)
            .Map(d => d.CreatedOn, s => TimeCalculator.ConvertToShamsi(s.Created))
            .Map(d => d.CreatorId, s => s.CreatorId)
            ;

        config.NewConfig<RequestGoodsSupplyDetail, GetFilteredRequestGoodsSupplyDetailsReportsModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.RequestGoodsSupplyId, s => s.RequestGoodsSupply.Id)
            .Map(d => d.ConsumableVolumeProductId, s => s.ConsumableVolumeProduct.Id)
            .Map(d => d.GroupId, s => s.ConsumableVolumeProduct.ProductGroupId)
            .Map(d => d.ProductId, s => s.ProductId)
            .Map(d => d.ProductNumber, s => s.ConsumableVolumeProduct.FinalValue)
            .Map(d => d.ConsumableVolumeProductId, s => s.ConsumableVolumeProduct.Id)
            .Map(d => d.Importance, s => s.Importance)
            .Map(d => d.Status, s => s.Status)
            .Map(d => d.PrivateName, s => s.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PrivateName)
            .Map(d => d.PrivateCode, s => s.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PrivateCode)
            .Map(d => d.PublicName, s => s.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PublicName)
            .Map(d => d.PublicCode, s => s.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PublicCode)
            .Map(d => d.ProjectOperationDetailDescription, s => s.ConsumableVolumeProduct.ProjectOperationDetail.Description)
            .Map(d => d.DelivaryDeadLine, s => s.DelivaryDeadLine)
            .Map(d => d.TotalPrice, s => s.TotalPrice)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.ManagementDescription, s => s.ManagementDescription)
            .Map(d => d.DestinationWarehouseId, s => s.DestinationWarehouseId)
            .Map(d => d.ContractorId, s => s.ContractorId)
            .Map(d => d.CheckGroup, s => s.CheckGroup)
            .Map(d => d.CustomerInvoiceNumber, s => s.CustomerInvoiceNumber)
            ;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        config.NewConfig<RequestGoodsSupply, GetFilteredRequestGoodsSuppliesReportsModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.CostCenterId,
            s => s.ProjectOperation.Project.ProjectCostCenters
            .Select(x => (long?)x.CostCenterId)
            .FirstOrDefault())

            .Map(d => d.CostCenterName,
            s => s.ProjectOperation.Project.ProjectCostCenters
            .Select(x => x.CostCenter.CostCenterName)
            .FirstOrDefault())
            .Map(d => d.CreatedOn, s => s.Created)
            .Map(d => d.CreatorId, s => s.CreatorId)
            .Map(d => d.MaxImportance, s => s.RequestGoodsSupplyDetails.Max(c => c.Importance))
            .Map(d => d.PrivateName, s => s.ProjectOperationDetail.OperationLocation.PrivateName)
            .Map(d => d.PrivateCode, s => s.ProjectOperationDetail.OperationLocation.PrivateCode)
            .Map(d => d.PublicName, s => s.ProjectOperationDetail.OperationLocation.PublicName)
            .Map(d => d.PublicCode, s => s.ProjectOperationDetail.OperationLocation.PublicCode)
            .Map(d => d.ProjectOperationDetailDescription, s => s.ProjectOperationDetail.Description)
            .Map(d => d.FinalAmount, s => s.ProjectOperationDetail.FinalAmount)
            .Map(d => d.OperationInfoName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
            .Map(d => d.OperationInfoCode, s => s.ProjectOperation.OperationInfo.OperationInfoCode)
            .Map(d => d.Workload, s => s.ProjectOperation.Workload)
            .Map(d => d.ProjectId, s => s.ProjectOperation.Project.Id)
            .Map(d => d.ProjectName, s => s.ProjectOperation.Project.ProjectName)
            .Map(d => d.RequestNumber, s => s.RequestSerialNumber)
            .Map(d => d.RequestedDate, s => s.RequestedDate)
            .Map(d => d.Status, s => s.Status)
            .Map(d => d.Type, s => s.Type)
            ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.   

        config.NewConfig<RequestGoodsSupplyDetail, GetsRequestGoodsSupplyDetailReportsExcelExporterModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.RequestGoodsSupplyId, s => s.RequestGoodsSupply.Id)
            .Map(d => d.ConsumableVolumeProductId, s => s.ConsumableVolumeProduct.Id)
            .Map(d => d.GroupId, s => s.ConsumableVolumeProduct.ProductGroupId)
            .Map(d => d.ProductId, s => s.ProductId)
            .Map(d => d.ProductNumber, s => s.ConsumableVolumeProduct.FinalValue)
            .Map(d => d.ConsumableVolumeProductId, s => s.ConsumableVolumeProduct.Id)
            .Map(d => d.Importance, s => s.Importance)
            .Map(d => d.Status, s => s.Status)
            .Map(d => d.PrivateName, s => s.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PrivateName)
            .Map(d => d.PrivateCode, s => s.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PrivateCode)
            .Map(d => d.PublicName, s => s.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PublicName)
            .Map(d => d.PublicCode, s => s.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PublicCode)
            .Map(d => d.ProjectOperationDetailDescription, s => s.ConsumableVolumeProduct.ProjectOperationDetail.Description)
            .Map(d => d.DelivaryDeadLine, s => TimeCalculator.ConvertToShamsi(s.DelivaryDeadLine))
            .Map(d => d.TotalPrice, s => s.TotalPrice)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.ManagementDescription, s => s.ManagementDescription)
            .Map(d => d.DestinationWarehouseId, s => s.DestinationWarehouseId)
            .Map(d => d.ContractorId, s => s.ContractorId)
            .Map(d => d.CheckGroup, s => s.CheckGroup)
            .Map(d => d.CustomerInvoiceNumber, s => s.CustomerInvoiceNumber)
            ;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        config.NewConfig<RequestGoodsSupply, GetsRequestGoodsSupplyReportsExcelExporterResponseModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.CostCenterId,
            s => s.ProjectOperation.Project.ProjectCostCenters
            .Select(x => (long?)x.CostCenterId)
            .FirstOrDefault())

            .Map(d => d.CostCenterName,
            s => s.ProjectOperation.Project.ProjectCostCenters
            .Select(x => x.CostCenter.CostCenterName)
            .FirstOrDefault())
            .Map(d => d.CreatedOn, s => TimeCalculator.ConvertToShamsi(s.Created))
            .Map(d => d.CreatorId, s => s.CreatorId)
            .Map(d => d.MaxImportance, s => s.RequestGoodsSupplyDetails.Max(c => c.Importance))
            .Map(d => d.PrivateName, s => s.ProjectOperationDetail.OperationLocation.PrivateName)
            .Map(d => d.PrivateCode, s => s.ProjectOperationDetail.OperationLocation.PrivateCode)
            .Map(d => d.PublicName, s => s.ProjectOperationDetail.OperationLocation.PublicName)
            .Map(d => d.PublicCode, s => s.ProjectOperationDetail.OperationLocation.PublicCode)
            .Map(d => d.ProjectOperationDetailDescription, s => s.ProjectOperationDetail.Description)
            .Map(d => d.FinalAmount, s => s.ProjectOperationDetail.FinalAmount)
            .Map(d => d.OperationInfoName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
            .Map(d => d.OperationInfoCode, s => s.ProjectOperation.OperationInfo.OperationInfoCode)
            .Map(d => d.Workload, s => s.ProjectOperation.Workload)
            .Map(d => d.ProjectId, s => s.ProjectOperation.Project.Id)
            .Map(d => d.ProjectName, s => s.ProjectOperation.Project.ProjectName)
            .Map(d => d.RequestNumber, s => s.RequestSerialNumber)
            .Map(d => d.Status, s => s.Status)
            .Map(d => d.Type, s => s.Type)
            ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

    }
}
