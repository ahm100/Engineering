using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryDocument;

public record DeleteRequestMachineryDocumentCommand(long Id) : ICommand<RequestMachineryDocument>;
