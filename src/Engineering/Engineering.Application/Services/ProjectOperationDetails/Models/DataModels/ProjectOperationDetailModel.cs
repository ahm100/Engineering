using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Responses;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailStatus;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;

public record ProjectOperationDetailModel(
    long Id,
    string? Code,
    long ProjectOperationId,
    long ProjectId,
    string? ProjectName,
    string? ProjectManagerName,
    long OperationInfoId,
    string? OperationInfoName,
    long OperationLocationId,
    string? PrivateName,
    string? PrivateCode,
    string? PublicName,
    string? PublicCode,
    string? StartDate,
    string? EndDate,
    decimal Length,
    bool LengthChangeable,
    decimal Width,
    bool WidthChangeable,
    decimal Height,
    bool HeightChangeable,
    decimal Weight,
    bool WeightChangeable,
    decimal Number,
    bool NumberChangeable,
    decimal FinalAmount,
    decimal? DuductionFinalAmount,
    decimal? TotalFinalAmount,
    GetsProjectOperationDetailStatusModel StatusModel,
    int Priority,
    int Day,
    int Hour,
    long? CreatedProductId,
    string? CreatedProductName,
    string? Description,
    List<string>? Urls,
    List<PlannerDataModel?> Planners,
    List<ImplementationAssistantDataModel?> ImplementationAssistants,
    List<TechnicalAssistantDataModel?> TechnicalAssistants,
    List<ContractorServiceDataModel?> ContractorServices,
    List<ExpertDataModel?> Experts,
    List<MachineryDataModel?> Machineries,
    List<ProductDataModel?> Products,
    List<DeductionDataModel?> Deductions,
    CreatorModel? Creator
    );


