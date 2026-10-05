using Engineering.Application.Services.RequestRewards.Contracts.CreateRequestReward;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.UpdateDailyProjectOperation;

public record UpdateDailyProjectOperationRequest(
    long DailyProjectOperationId,
    List<ContractorServiceModel>? ContractorServices,
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
    List<UpdateDailyProjectOperationExpertModel>? Experts,
    List<UpdateDailyProjectOperationMachineryModel>? Machineries,
    List<UpdateDailyProjectOperationProductModel>? Products,
    CreateRequestRewardRequest? RequestReward
    ) : IHttpRequest;

public record ContractorServiceModel(
    long? Id,
    long ContractorServiceId,
    decimal Volume,
    decimal? ProjectServiceVolume,
    string? TimeSpant,
    long? ThirdPartyId,
    bool IsActive,
    bool IsDeleted
    );
