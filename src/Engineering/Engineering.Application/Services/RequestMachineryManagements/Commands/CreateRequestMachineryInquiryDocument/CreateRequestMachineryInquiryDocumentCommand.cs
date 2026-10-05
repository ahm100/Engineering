using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiryDocument;

public record CreateRequestMachineryInquiryDocumentCommand(string Url,
                                                          RequestMachineryInquiry RequestMachineryInquiry) : ICommand<RequestMachineryInquiryDocument>;
