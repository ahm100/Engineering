
namespace Engineering.Application.Services.TransportationRequests.Models.GetById;

public record GetTransportationRequestCostCenter(
    long Id,
    long CostCenterId,
    string CostCenterName
    );
