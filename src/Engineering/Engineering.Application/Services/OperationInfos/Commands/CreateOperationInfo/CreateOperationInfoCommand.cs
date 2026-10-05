using Engineering.Application.Services.OperationInfos.Models.CreateOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.OperationInfoDependency;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Services;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ServiceInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.OperationInfos.Commands.CreateOperationInfo;

public record CreateOperationInfoCommand(
    List<Season>? Seasons,
    List<OperationInfoServiceModel>? ServiceInfos,
    List<OperationInfoGroup>? OperationInfoGroups,
    string OperationInfoName,
    string OperationInfoCode,
    string? OperationLatinName,
    bool IsPriceList,
    int? Priority,
    long UnitOfMeasurementId,
    decimal BasePrice,
    OperationInfoDependencyRequestModel? OperationInfoDependency,
    bool IsActive,
    List<OperationInfoExpertServiceModel?> ExpertStandards,
    List<OperationInfoGoodsServiceModel?> GoodsStandards,
    List<OperationInfoMachineryServiceModel?> MachineryStandards,
    List<CreateOperationInfoActionsModel>? OperationInfoActions,
    long? CompanyId
    ) : ICommand<OperationInfo>;

public record OperationInfoServiceModel(
    ServiceInfo ServiceInfo,
    long TimeSpant
    );
