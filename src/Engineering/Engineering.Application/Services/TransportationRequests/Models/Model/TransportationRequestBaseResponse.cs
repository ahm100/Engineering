using Engineering.Application.Services.TransportationRequests.Commands.Create;
using Engineering.Domain.Entities.BillOfLadings;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Logistics;
using Engineering.Domain.Entities.MachineTypes;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Trips;

namespace Engineering.Application.Services.TransportationRequests.Models.Model;

public record TransportationRequestBaseResponse
{
    public TransportationContractor? TransportationContractor { get; set; }
    public Transportation? Transportation { get; set; }
    public Trip? Trip { get; set; }
    public MachineType? MachineType { get; set; }
    public BillOfLading? BillOfLading { get; set; }
    public List<CostCenter>? CostCenters { get; set; }
    public List<Project>? Projects { get; set; }
    public List<ProjectOperation>? ProjectOperations { get; set; }
    public List<ProjectOperationDetail>? ProjectOperationDetails { get; set; }
    public List<CreateTransportationWarehouseCommandModel>? warehousesCommand { get; set; }
    public List<UpdateTransportationWarehouseCommandModel>? updateWarehousesCommand { get; set; }
}
