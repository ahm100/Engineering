using Engineering.Application.Services.Adjustments.Contracts;
using Engineering.Application.Services.Adjustments.Contracts.CreateAdjustmentIndex;
using Engineering.Application.Services.Adjustments.Contracts.DeleteAdjustmentIndex;
using Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexById;
using Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexes;
using Engineering.Application.Services.Adjustments.Contracts.UpdateAdjustmentIndex;

namespace Engineering.Application.Services.Adjustments;

public interface IAdjustmentLogic
{
    Task<Result<AdjustmentExcelImportsResponse>> AdjustmentExcelImports(AdjustmentExcelImportsRequest request, CT ct);
    Task<Result<CreateAdjustmentIndexResponse?>> CreateAdjustmentIndex(CreateAdjustmentIndexRequest request, CT ct);
    Task<Result<UpdateAdjustmentIndexResponse?>> UpdateAdjustmentIndex(UpdateAdjustmentIndexRequest request, CT ct);
    Task<Result<GetAdjustmentIndexByIdResponse?>> GetAdjustmentIndexById(GetAdjustmentIndexByIdRequest request, CT ct);
    Task<Result<GetAdjustmentIndexesResponse?>> GetAdjustmentIndexes(GetAdjustmentIndexesRequest request, CT ct);
    Task<Result<DeleteAdjustmentIndexResponse?>> DeleteAdjustmentIndex(DeleteAdjustmentIndexRequest request, CT ct);
}

