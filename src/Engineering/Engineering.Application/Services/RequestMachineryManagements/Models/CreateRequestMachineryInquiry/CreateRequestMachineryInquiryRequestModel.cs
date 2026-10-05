using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineryManagements.Models.CreateRequestMachineryInquiry;

public record CreateRequestMachineryInquiryRequestModel(long? Id,
                                                        long ThirdPartyId,
                                                        long CurrencyId,
                                                        int Count,
                                                        List<CreateRequestMachineryInquiryDocumentRequest>? Documents,
                                                        RequestMachineryUnit Unit,
                                                        decimal UnitPrice,
                                                        decimal? TotalPrice,
                                                        string? Description,
                                                        decimal InquiryRequestedTime,
                                                        bool? IsDeleted);
