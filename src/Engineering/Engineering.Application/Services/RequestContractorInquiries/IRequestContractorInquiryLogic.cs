using Engineering.Application.Services.RequestContractorInquiries.Models.CreateRequestContractorInquiry;
using Engineering.Application.Services.RequestContractorInquiries.Models.GetRequestContractorInquiryByRequestId;
using Engineering.Application.Services.RequestContractorInquiries.Models.UpdateRequestContractorInquiry;
using Engineering.Application.Services.RequestContractors.Models.DeleteRequestContractorInquiry;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorInquiryById;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorType;

namespace Engineering.Application.Services.RequestContractorInquiries;

public partial interface IRequestContractorInquiryLogic
{
    Task<Result<CreateRequestContractorInquiryResponse?>> CreateRequestContractorInquiry(CreateRequestContractorInquiryRequest request, CT ct);
    Task<Result<UpdateRequestContractorInquiryResponse?>> UpdateRequestContractorInquiry(UpdateRequestContractorInquiryRequest request, CT ct);
    Task<Result<DeleteRequestContractorInquiryResponse?>> DeleteRequestContractorInquiry(DeleteRequestContractorInquiryRequest request, CT ct);
    Task<Result<GetRequestContractorInquiryByIdResponse?>> GetRequestContractorInquiryById(GetRequestContractorInquiryByIdRequest request, CT ct);
    Task<Result<GetRequestContractorInquiryByRequestIdResponse?>> GetRequestContractorInquiryByRequestId(GetRequestContractorInquiryByRequestIdRequest request, CT ct);
    Task<Result<GetRequestContractorTypeResponse?>> GetRequestContractorType(GetRequestContractorTypeRequest request, CT ct);
}
