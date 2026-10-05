using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.OperationInfoDependency;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Requests;

namespace Engineering.Application.Services.OperationInfos.Models.CreateOperationInfo;

public record CreateOperationInfoRequest(
    List<long> SeasonIds,
    List<CreateOperationInfoServicesModel>? ServiceInfos,
    List<long>? OperationInfoGroupIds,
    string OperationInfoName,
    string OperationInfoCode,
    string? OperationLatinName,
    int? Priority,
    decimal BasePrice,
    long UnitOfMeasurementId,
    bool IsPriceList,
    OperationInfoDependencyRequestModel? OperationInfoDependency,
    List<OperationInfoExpertRequestModel>? ExpertStandards,
    List<OperationInfoGoodsRequestModel>? GoodsStandards,
    List<OperationInfoMachineryRequestModel>? MachineryStandards,
    List<CreateOperationInfoActionsModel>? OperationInfoActions,
    bool IsActive
     ) : IHttpRequest;

public record CreateOperationInfoServicesModel(
    long ServiceId,
    string TimeSpant
    );

public record CreateOperationInfoActionsModel(
    long ActionId,
    decimal Price
    );
