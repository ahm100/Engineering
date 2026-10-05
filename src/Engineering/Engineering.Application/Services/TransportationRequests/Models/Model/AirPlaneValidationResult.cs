using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Trips;

namespace Engineering.Application.Services.TransportationRequests.Models.Model;

public class AirplaneValidationResult
{
    public TransportationRequest? Airplane { get; set; }
    public long? CompanyId { get; set; }
    public Transportation? Transportation { get; set; }
    public Trip? Trip { get; set; }
    public List<CostCenter>? CostCenters { get; set; } = new();
    public List<Project>? Projects { get; set; } = new();
    public List<ProjectOperation>? ProjectOperations { get; set; } = new();
    public List<ProjectOperationDetail>? ProjectOperationDetails { get; set; } = new();
}
