using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryDocument;

public record CreateRequestMachineryDocumentCommand(
    string Url,
    RequestMachinery RequestMachinery
    ) : ICommand<RequestMachineryDocument>;
