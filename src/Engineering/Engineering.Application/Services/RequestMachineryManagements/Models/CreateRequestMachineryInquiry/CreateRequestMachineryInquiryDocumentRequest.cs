namespace Engineering.Application.Services.RequestMachineryManagements.Models.CreateRequestMachineryInquiry;

public record CreateRequestMachineryInquiryDocumentRequest(long? Id,
                                                          string Url,
                                                          bool IsDeleted);
