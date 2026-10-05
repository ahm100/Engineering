using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewards;

public record GetFilteredRequestRewardsRequest(long? CostCenterId,
                                               long? ProjectId,
                                               long? ThirdPartyId,
                                               long? EmployerContractId,
                                               DateTime? Fromdate,
                                               DateTime? Todate,
                                               RequestRewardStatus? Status,
                                               RequestRewardType? Type,
                                               bool? IsReward,
                                               long? ProjectOperationId,
                                               long? ProjectOperationDetailId,
                                               string? FilterData,
                                               string[]? OrderBy,
                                               int PageIndex,
                                               int PageSize) : IHttpRequest;
