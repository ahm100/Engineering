using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractors.Commands.UpdateRequestContractor;

public record UpdateRequestContractorCommand(
    RequestContractor RequestContractor,
    decimal Volume,
    string? Description) : ICommand<RequestContractor>;
