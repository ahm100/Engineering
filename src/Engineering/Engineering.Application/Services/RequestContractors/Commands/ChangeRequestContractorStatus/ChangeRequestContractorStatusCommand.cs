using Engineering.Domain.Entities.RequestContractors;
using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Application.Services.RequestContractors.Commands.ChangeRequestContractorStatus;

public record ChangeRequestContractorStatusCommand(
    RequestContractor Entity,
    RequestContractorStatus Status,
    string? Description
    ) : ICommand<RequestContractor>;
