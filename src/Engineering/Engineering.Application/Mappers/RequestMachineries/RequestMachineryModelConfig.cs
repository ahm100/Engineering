using Engineering.Application.Services.RequestMachineries.Models.GetsMachineryRequesteExcelExporter;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryInquiries;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetsRequestMachineryManagementExcelExporter;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Mappers.RequestMachineries;

public class RequestMachineryModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RequestMachinery, GetsMachineryRequesteExcelExporterResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.Created, s => TimeCalculator.ConvertToShamsi(s.Created))
           .Map(d => d.MachineryGroupName, s => s.Machinery.MachineriesGroup.GroupName)
           .Map(d => d.MachineryName, s => s.Machinery.MachineryName)
           .Map(d => d.TimeRequired, s => s.TimeRequired.ToString())
           .Map(d => d.StatusDescription, s => s.Status.GetEnumDescription())
           .Map(d => d.UnitDescription, s => s.Unit.GetEnumDescription())
           .Map(d => d.RequestNumber, s => s.RequestNumber)
           .Map(d => d.RequestCount, s => s.RequestCount)
           .Map(d => d.FromDate, s => TimeCalculator.ConvertToShamsi(s.FromDate))
           .Map(d => d.ToDate, s => TimeCalculator.ConvertToShamsi(s.ToDate))

           .Map(d => d.CostCenterName,
           s => s.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterName)
           .FirstOrDefault())

           .Map(d => d.CreatorId, s => s.CreatorId)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.ProjectOperations, s => string.Join(",", s.ProjectOperations.Select(oo => oo.ProjectOperation).Select(oo => oo.OperationInfo.OperationInfoName).ToList()))
           .Map(d => d.ProjectOperationDetails, s => string.Join(",", s.ProjectOperationDetails.Select(oo => oo.ProjectOperationDetail).Select(oo => oo.OperationLocation.PublicName).ToList()))
           .Map(d => d.ConfirmDate, s => s.Histories.Any(x => x.RequestMachinery.Status == RequestMachineryStatus.Confirmed) ?
                TimeCalculator.DatePiker(s.Histories.Where(x => x.RequestMachinery.Status == RequestMachineryStatus.Confirmed).FirstOrDefault()!.Created) : "")
           .Map(d => d.ConfirmUserId, s => s.Histories.Any(x => x.RequestMachinery.Status == RequestMachineryStatus.Confirmed) ?
                s.Histories.Where(x => x.RequestMachinery.Status == RequestMachineryStatus.Confirmed).FirstOrDefault()!.CreatorId : 0)
           ;

        config.NewConfig<RequestMachinery, GetsRequestMachineryManagementExcelExporterResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.Created, s => TimeCalculator.ConvertToShamsi(s.Created))
           .Map(d => d.MachineryGroupName, s => s.Machinery.MachineriesGroup.GroupName)
           .Map(d => d.MachineryName, s => s.Machinery.MachineryName)
           .Map(d => d.TimeRequired, s => s.TimeRequired.ToString())
           .Map(d => d.StatusDescription, s => s.Status.GetEnumDescription())
           .Map(d => d.UnitDescription, s => s.Unit.GetEnumDescription())
           .Map(d => d.RequestNumber, s => s.RequestNumber)
           .Map(d => d.RequestCount, s => s.RequestCount)
           .Map(d => d.FromDate, s => TimeCalculator.ConvertToShamsi(s.FromDate))
           .Map(d => d.ToDate, s => TimeCalculator.ConvertToShamsi(s.ToDate))

           .Map(d => d.CostCenterName,
           s => s.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterName)
           .FirstOrDefault())

           .Map(d => d.CreatorId, s => s.CreatorId)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.ProjectOperations, s => string.Join(",", s.ProjectOperations.Select(oo => oo.ProjectOperation).Select(oo => oo.OperationInfo.OperationInfoName).ToList()))
           .Map(d => d.ProjectOperationDetails, s => string.Join(",", s.ProjectOperationDetails.Select(oo => oo.ProjectOperationDetail).Select(oo => oo.OperationLocation.PublicName).ToList()))
           .Map(d => d.ConfirmDate, s => s.Histories.Any(x => x.RequestMachinery.Status == RequestMachineryStatus.Confirmed) ?
                TimeCalculator.DatePiker(s.Histories.Where(x => x.RequestMachinery.Status == RequestMachineryStatus.Confirmed).FirstOrDefault()!.Created) : "")
           .Map(d => d.ConfirmUserId, s => s.Histories.Any(x => x.RequestMachinery.Status == RequestMachineryStatus.Confirmed) ?
                s.Histories.Where(x => x.RequestMachinery.Status == RequestMachineryStatus.Confirmed).FirstOrDefault()!.CreatorId : 0)
           .Map(d => d.AppointmentId, s => s.OperatorAppoinmentUserId)
           ;

        config.NewConfig<RequestMachinery, GetRequestMachineryInquiriesResponse>()

            .Map(d => d.CostCenterName,
            s => s.Project.ProjectCostCenters
            .Select(x => x.CostCenter.CostCenterName)
            .FirstOrDefault())
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.RequestNumber, s => s.RequestNumber)
           .Map(d => d.MachineryGroupName, s => s.Machinery.MachineriesGroup.GroupName)
           .Map(d => d.MachineryName, s => s.Machinery.MachineryName)
           .Map(d => d.TimeRequired, s => s.TimeRequired.ToString())
           .Map(d => d.StatusDescription, s => s.Status.GetEnumDescription())
           .Map(d => d.UnitDescription, s => s.Unit.GetEnumDescription())
           .Map(d => d.RequestCount, s => s.RequestCount)
           .Map(d => d.FromDate, s => s.FromDate)
           .Map(d => d.ToDate, s => s.ToDate)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.Unit, s => s.Unit)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;

    }
}
