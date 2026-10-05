using Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservationById;
using Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservations;
using Engineering.Domain.Entities.FixAssetMachineries;

namespace Engineering.Application.Mappers.MachineryReservations;

public class MachineryReservationsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MachineryReservation, GetMachineryReservationByIdResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.MachineryCode, s => s.FixAssetMachinery.Machinery.MachineryCode)
           .Map(d => d.MachineryId, s => s.FixAssetMachinery.Machinery.Id)
           .Map(d => d.MachineryName, s => s.FixAssetMachinery.Machinery.MachineryName)
           .Map(d => d.RequestMachineryId, s => s.RequestMachinery.Id)
           .Map(d => d.ProjectId, s => s.RequestMachinery.Project.Id)
           .Map(d => d.Project, s => s.RequestMachinery.Project.ProjectName)

           .Map(d => d.CostCenterId,
           s => s.RequestMachinery.Project.ProjectCostCenters
           .Select(x => (long?)x.CostCenterId)
           .FirstOrDefault())

           .Map(d => d.CostCenter,
           s => s.RequestMachinery.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterName)
           .FirstOrDefault())
           .Map(d => d.FixAssetMachineryId, s => s.FixAssetMachinery.Id)
           .Map(d => d.FixAssetMachineryType, s => s.FixAssetMachinery.FixAssetMachineryType)
           .Map(d => d.MachineryReservationUnit, s => s.Unit)
           .Map(d => d.MachineryReservationStatus, s => s.Status)
           .Map(d => d.StartDate, s => s.StartDate)
           .Map(d => d.StartTime, s => s.StartDate.TimeOfDay)
           .Map(d => d.EndDate, s => s.EndDate)
           .Map(d => d.EndTime, s => s.EndDate.TimeOfDay)
           .Map(d => d.Description, s => s.Description)
           ;

        config.NewConfig<MachineryReservation, GetMachineryReservationsModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.MachineryCode, s => s.FixAssetMachinery.Machinery.MachineryCode)
           .Map(d => d.MachineryId, s => s.FixAssetMachinery.Machinery.Id)
           .Map(d => d.MachineryName, s => s.FixAssetMachinery.Machinery.MachineryName)
           .Map(d => d.RequestMachineryId, s => s.RequestMachinery.Id)
           .Map(d => d.ProjectId, s => s.RequestMachinery.Project.Id)
           .Map(d => d.Project, s => s.RequestMachinery.Project.ProjectName)

           .Map(d => d.CostCenterId,
           s => s.RequestMachinery.Project.ProjectCostCenters
           .Select(x => (long?)x.CostCenterId)
           .FirstOrDefault())

           .Map(d => d.CostCenter,
           s => s.RequestMachinery.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterName)
           .FirstOrDefault())
           .Map(d => d.FixAssetMachineryId, s => s.FixAssetMachinery.Id)
           .Map(d => d.FixAssetMachineryType, s => s.FixAssetMachinery.FixAssetMachineryType)
           .Map(d => d.MachineryReservationUnit, s => s.Unit)
           .Map(d => d.StartDate, s => s.StartDate)
           .Map(d => d.StartTime, s => s.StartDate.TimeOfDay)
           .Map(d => d.EndDate, s => s.EndDate)
           .Map(d => d.EndTime, s => s.EndDate.TimeOfDay)
           .Map(d => d.Description, s => s.Description)
           ;

    }
}