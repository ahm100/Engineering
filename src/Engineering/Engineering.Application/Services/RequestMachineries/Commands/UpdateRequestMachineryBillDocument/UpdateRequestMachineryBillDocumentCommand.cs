using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryBillDocument;

public record UpdateRequestMachineryBillDocumentCommand(long Id,
                                                        string Url) : ICommand<RequestMachineryBillDocument>;
