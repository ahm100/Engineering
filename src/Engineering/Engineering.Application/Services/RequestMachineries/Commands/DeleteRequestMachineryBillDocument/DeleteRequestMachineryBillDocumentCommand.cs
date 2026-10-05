using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryBillDocument;

public record DeleteRequestMachineryBillDocumentCommand(long Id) : ICommand<RequestMachineryBillDocument>;
