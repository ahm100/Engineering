
namespace Engineering.Application.Services.OperationInfoSeasons.Models.GetsByOperationInfoId;

public record OperationInfoSeasonsBranchModel(
    long Id,
    long CategoryId,
    string BranchName,
    string BranchCode,
    bool IsActive
    );
