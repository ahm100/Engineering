using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.UpdateRequestMachineryInquiryDocument;

public record UpdateRequestMachineryInquiryDocumentCommand(long Id,
                                                          string Url) : ICommand<RequestMachineryInquiryDocument>;
