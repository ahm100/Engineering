
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.OperationInfoDependency;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Requests;
using Engineering.Application.Services.OperationInfoServices.Models.CreateOperationInfoService;

namespace Engineering.Application.Services.OperationInfos.Models.UpdateOperationInfo;

public record UpdateOperationInfoRequest(
    long Id,
    List<long>? SeasonIds,
    List<long>? DeletedOperationInfoSeasonIds,
    List<long>? OperationInfoGroupIds,
    List<CreateOperationInfoServiceRequestModel>? ServiceInfos,
    string OperationInfoName,
    string OperationInfoCode,
    string? OperationLatinName,
    int? Priority,
    long UnitOfMeasurementId,
    decimal? BasePrice,
    OperationInfoDependencyRequestModel? OperationInfoDependency,
    List<UpdateOperationInfoExpertRequestModel>? ExpertStandards,
    List<UpdateOperationInfoGoodsRequestModel>? GoodsStandards,
    List<UpdateOperationInfoMachineryRequestModel>? MachineryStandards,
    List<UpdateOperationInfoActionsModel>? OperationInfoActions,
    bool ChangeUnitOfMeasurement,
    bool IsActive
     ) : IHttpRequest;

public record UpdateOperationInfoActionsModel(
    long? Id,
    long actionId,
    bool? IsDeleted,
    decimal? Price
    );