using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryDocument;

public record UpdateRequestMachineryDocumentCommand(long Id,
                                                    string Url) : ICommand<RequestMachineryDocument>;
