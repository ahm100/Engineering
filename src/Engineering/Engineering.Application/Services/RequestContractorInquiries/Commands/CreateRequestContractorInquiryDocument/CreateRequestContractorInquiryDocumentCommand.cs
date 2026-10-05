using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractorInquiries.Commands.CreateRequestContractorInquiryDocument;

public record CreateRequestContractorInquiryDocumentCommand(
    string Url,
    RequestContractorInquiry RequestContractorInquiry) : ICommand<RequestContractorInquiryDocument>;
