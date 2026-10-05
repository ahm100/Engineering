using Engineering.Application.Services.RequestContractors.Models.CreateRequestContractor;
using Engineering.Application.Services.RequestContractors.Models.DeleteRequestContractor;
using Engineering.Application.Services.RequestContractors.Models.GetFilteredRequestContractors;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorById;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorHistories;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorStatus;
using Engineering.Application.Services.RequestContractors.Models.GetsRequestContractorExcelEnums;
using Engineering.Application.Services.RequestContractors.Models.GetsRequestContractorExcelExporter;
using Engineering.Application.Services.RequestContractors.Models.GroupRequestContractorStatusChanger;
using Engineering.Application.Services.RequestContractors.Models.RequestContractorGroupDelete;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorConfirmed;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorEndInquiry;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorInquiryConfirmed;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorInquiryRejected;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorPending;
using Engineering.Application.Services.RequestContractors.Models.SetRequestContractorRejected;
using Engineering.Application.Services.RequestContractors.Models.UpdateRequestContractor;

namespace Engineering.Application.Services.RequestContractors;

public partial interface IRequestContractorLogic
{
    Task<Result<CreateRequestContractorResponse?>> CreateRequestContractor(CreateRequestContractorRequest request, CT ct);
    Task<Result<DeleteRequestContractorResponse?>> DeleteRequestContractor(DeleteRequestContractorRequest request, CT ct);
    Task<Result<UpdateRequestContractorResponse?>> UpdateRequestContractor(UpdateRequestContractorRequest request, CT ct);
    Task<Result<RequestContractorGroupDeleteResponse?>> RequestContractorGroupDelete(RequestContractorGroupDeleteRequest request, CT ct);
    Task<Result<GroupRequestContractorStatusChangerResponse?>> GroupRequestContractorStatusChanger(GroupRequestContractorStatusChangerRequest request, CT ct);
    Task<Result<SetRequestContractorConfirmedResponse?>> SetRequestContractorConfirmed(SetRequestContractorConfirmedRequest request, CT ct);
    Task<Result<SetRequestContractorInquiryConfirmedResponse?>> SetRequestContractorInquiryConfirmed(SetRequestContractorInquiryConfirmedRequest request, CT ct);
    Task<Result<SetRequestContractorPendingResponse?>> SetRequestContractorPending(SetRequestContractorPendingRequest request, CT ct);
    Task<Result<SetRequestContractorInquiryRejectedResponse?>> SetRequestContractorInquiryRejected(SetRequestContractorInquiryRejectedRequest request, CT ct);
    Task<Result<SetRequestContractorRejectedResponse?>> SetRequestContractorRejected(SetRequestContractorRejectedRequest request, CT ct);
    Task<Result<SetRequestContractorEndInquiryResponse?>> SetRequestContractorEndInquiry(SetRequestContractorEndInquiryRequest request, CT ct);
    Task<Result<GetRequestContractorStatusResponse?>> GetRequestContractorStatus(GetRequestContractorStatusRequest request, CT ct);
    Task<Result<GetsRequestContractorExcelExporterResponse?>> GetsRequestContractorExcelExporter(GetsRequestContractorExcelExporterRequest request, CT ct);
    Task<Result<GetsRequestContractorExcelEnumsResponse?>> GetsRequestContractorExcelEnums(GetsRequestContractorExcelEnumsRequest request, CT ct);
    Task<Result<GetFilteredRequestContractorsResponse?>> GetFilteredRequestContractors(GetFilteredRequestContractorsRequest request, CT ct);
    Task<Result<GetRequestContractorHistoriesResponse?>> GetRequestContractorHistories(GetRequestContractorHistoriesRequest request, CT ct);
    Task<Result<GetRequestContractorByIdResponse?>> GetRequestContractorById(GetRequestContractorByIdRequest request, CT ct);
}
