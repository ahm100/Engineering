using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.RequestContractors;
using Engineering.Domain.Entities.ServiceInfos;

namespace Engineering.Application.Services.RequestContractors.Commands.CreateRequestContractor;

public record CreateRequestContractorCommand(
    ProjectOperationDetail ProjectOperationDetail,
    ServiceInfo ServiceInfo,
    decimal Volume,
    string? Description,
    long? CompanyId) : ICommand<RequestContractor>;

