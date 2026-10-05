using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Trips;

namespace Engineering.Application.Services.TransportationRequests.Models.Model;

public class SnapValidationResult
{
    public TransportationRequest? Snap { get; set; }
    public long? CompanyId { get; set; }
    public Transportation? Transportation { get; set; }
    public Trip? Trip { get; set; }
    public List<CostCenter>? CostCenters { get; set; } = new();
    public List<Project>? Projects { get; set; } = new();
}
