using Engineering.Application.Services.RequestRewards.Contracts.CloseRequestReward;
using Engineering.Application.Services.RequestRewards.Contracts.ConfirmRequestReward;
using Engineering.Application.Services.RequestRewards.Contracts.CreateRequestReward;
using Engineering.Application.Services.RequestRewards.Contracts.DeleteRequestRewardByIds;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredFiduciaryProductsExcelEnums;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewards;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewardsExcelEnums;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewardsExcelExporter;
using Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardById;
using Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardStatus;
using Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardType;
using Engineering.Application.Services.RequestRewards.Contracts.PendingRequestReward;
using Engineering.Application.Services.RequestRewards.Contracts.RejectRequestReward;

namespace Engineering.Application.Services.RequestRewards;

public interface IRequestRewardLogic
{
    Task<Result<CreateRequestRewardResponse?>> CreateRequestReward(CreateRequestRewardRequest request, CT ct);
    Task<Result<ConfirmRequestRewardResponse?>> ConfirmRequestReward(ConfirmRequestRewardRequest request, CT ct);
    Task<Result<RejectRequestRewardResponse?>> RejectRequestReward(RejectRequestRewardRequest request, CT ct);
    Task<Result<GetFilteredRequestRewardsResponse?>> GetFilteredRequestRewards(GetFilteredRequestRewardsRequest request, CT ct);
    Task<Result<GetRequestRewardByIdResponse?>> GetRequestRewardById(GetRequestRewardByIdRequest request, CT ct);
    Task<Result<CloseRequestRewardResponse?>> CloseRequestReward(CloseRequestRewardRequest request, CT ct);
    Task<Result<PendingRequestRewardResponse?>> PendingRequestReward(PendingRequestRewardRequest request, CT ct);
    Task<Result<GetRequestRewardTypeResponse?>> GetRequestRewardType(GetRequestRewardTypeRequest request, CT ct);
    Task<Result<GetRequestRewardStatusResponse?>> GetRequestRewardStatus(GetRequestRewardStatusRequest request, CT ct);

    Task<Result<GetFilteredRequestRewardsExcelExporterResponse?>> GetFilteredRequestRewardsExcelExporter(GetFilteredRequestRewardsExcelExporterRequest request, CT ct);
    Task<Result<GetFilteredRequestRewardsExcelEnumsResponse?>> GetFilteredRequestRewardsExcelEnums(GetFilteredRequestRewardsExcelEnumsRequest request, CT ct);
    Task<Result<DeleteRequestRewardByIdsResponse?>> DeleteRequestRewardByIds(DeleteRequestRewardByIdsRequest request, CT ct);
}
