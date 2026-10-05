using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoAction;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Responses;
using Engineering.Application.Services.OperationInfoSeasons.Models.GetsByOperationInfoId;

namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfoById;

public record GetOperationInfoByIdResponse(
    long Id,
    string OperationInfoName,
    string OperationInfoCode,
    string? OperationLatinName,
    int? Priority,
    decimal BasePrice,
    bool HaveStandard,
    OperationInfoMeasurementModel? MeasurementData,
    OperationInfoDependencyModel? DependencyData,
    List<ServiceInfoDataModel>? ServiceInfoData,
    List<OperationInfoExpertDataModel>? ExpertData,
    List<OperationInfoGoodsDataModel>? GoodsData,
    List<OperationInfoMachineryDataModel>? MachineryData,
    List<OperationInfoSeasonsCategoryModel>? CategoryData,
    List<OperationInfoSeasonsBranchModel>? BranchData,
    List<OperationInfoSeasonsSeasonModel>? SeasonData,
    List<OperationInfoGroupDataModel>? GroupData,
    List<GetOperationInfoActionModel>? ActionData,
    bool IsActive,
    bool HasChanged,
    long? CompanyId,
    string? CompanyNameFa
);
