using Engineering.Application.Services.RequestRewards.Contracts.CreateRequestReward;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.CreateDailyProjectOperation;

public record CreateDailyProjectOperationRequest(
    long ProjectOperationDetailId,
    ProjectOperationDetailStatus Status,
    string? StatusDescription,
    DateTime StartDate,
    DateTime EndDate,
    decimal Length,
    decimal Width,
    decimal Height,
    decimal Weight,
    decimal Number,
    List<string>? DocumentUrls,
    string? Description,
    long? LegacyId,
    List<CreateDailyContractorServiceModel>? ContractorServices,
    List<CreateDailyProjectOperationExpertModel>? Experts,
    List<CreateDailyProjectOperationMachineryModel>? Machineries,
    List<CreateDailyProjectOperationProductModel>? Products,
    CreateRequestRewardRequest? RequestReward
    ) : IHttpRequest;

public record CreateDailyContractorServiceModel(
    long ContractorServiceId,
    decimal Volume,
    decimal? ProjectServiceVolume,
    long? ThirdPartyId,
    string? TimeSpant,
    bool IsActive
    );
