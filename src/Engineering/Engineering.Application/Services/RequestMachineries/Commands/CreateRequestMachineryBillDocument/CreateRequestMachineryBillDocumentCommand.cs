using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryBillDocument;

public record CreateRequestMachineryBillDocumentCommand(
    string Url,
    RequestMachinery RequestMachinery
    ) : ICommand<RequestMachineryBillDocument>;
