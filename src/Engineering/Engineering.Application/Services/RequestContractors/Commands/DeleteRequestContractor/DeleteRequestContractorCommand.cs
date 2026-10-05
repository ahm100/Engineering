using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractors.Commands.DeleteRequestContractor;

public record DeleteRequestContractorCommand(RequestContractor RequestContractor) : ICommand<RequestContractor>;
