using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Responses;
using Engineering.Application.Services.OperationInfoSeasons.Models.GetsByOperationInfoId;

namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

public record OperationInfoModel(
    long Id,
    string OperationInfoName,
    string OperationInfoCode,
    string? OperationLatinName,
    int? Priority,
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
    bool IsActive,
    long? CompanyId,
    string? CompanyNameFa
    );
