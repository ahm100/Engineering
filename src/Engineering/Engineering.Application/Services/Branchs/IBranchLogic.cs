using Engineering.Application.Services.Branchs.Models.ActiveBranch;
using Engineering.Application.Services.Branchs.Models.BranchExcelImports;
using Engineering.Application.Services.Branchs.Models.BranchGroupDelete;
using Engineering.Application.Services.Branchs.Models.CodeCreator;
using Engineering.Application.Services.Branchs.Models.CreateBranch;
using Engineering.Application.Services.Branchs.Models.DisableBranch;
using Engineering.Application.Services.Branchs.Models.GetBranchByCode;
using Engineering.Application.Services.Branchs.Models.GetBranchById;
using Engineering.Application.Services.Branchs.Models.GetBranchByName;
using Engineering.Application.Services.Branchs.Models.GetsActiveBranchs;
using Engineering.Application.Services.Branchs.Models.GetsBranchByCategoryIds;
using Engineering.Application.Services.Branchs.Models.GetsBranchByFilterData;
using Engineering.Application.Services.Branchs.Models.GetsBranchExcelEnum;
using Engineering.Application.Services.Branchs.Models.GetsBranchExcelExporter;
using Engineering.Application.Services.Branchs.Models.GetsBranchs;
using Engineering.Application.Services.Branchs.Models.GetsByCategoryId;
using Engineering.Application.Services.Branchs.Models.InactiveBranch;
using Engineering.Application.Services.Branchs.Models.StateChangerBranchs;
using Engineering.Application.Services.Branchs.Models.UpdateBranch;

namespace Engineering.Application.Services.Branchs;

public interface IBranchLogic
{
    ///Commands
    Task<Result<CreateBranchResponse?>> CreateBranch(
        CreateBranchRequest request, CT ct);

    Task<Result<ActiveBranchResponse?>> ActiveBranch(
        ActiveBranchRequest request, CT ct);

    Task<Result<UpdateBranchResponse?>> UpdateBranch(
        UpdateBranchRequest request, CT ct);

    Task<Result<DisableBranchResponse?>> DisableBranch(
        DisableBranchRequest request, CT ct);

    Task<Result<InactiveBranchResponse?>> InactiveBranch(
        InactiveBranchRequest request, CT ct);

    Task<Result<BranchCodeCreatorResponse?>> BranchCodeCreator(
        BranchCodeCreatorRequest request, CT ct);

    Task<Result<BranchGroupDeleteResponse?>> BranchGroupDelete(
        BranchGroupDeleteRequest request, CT ct);

    Task<Result<BranchExcelImportsResponse?>> BranchExcelImports(
        BranchExcelImportsRequest request, CT ct);

    Task<Result<StateChangerBranchsResponse?>> StateChangerBranchs(
        StateChangerBranchsRequest request, CT ct);

    ///Queries
    Task<Result<GetsBranchsResponse?>> GetsBranch(
        GetsBranchsRequest request, CT ct);

    Task<Result<GetBranchByIdResponse?>> GetBranchById(
        GetBranchByIdRequest request, CT ct);

    Task<Result<GetBranchByCodeResponse?>> GetBranchByCode(
        GetBranchByCodeRequest request, CT ct);

    Task<Result<GetBranchByNameResponse?>> GetBranchByName(
        GetBranchByNameRequest request, CT ct);

    Task<Result<GetsByCategoryIdResponse?>> GetsByCategoryId(
        GetsByCategoryIdRequest request, CT ct);

    Task<Result<GetsActiveBranchsResponse?>> GetsActiveBranch(
        GetsActiveBranchsRequest request, CT ct);

    Task<Result<GetsBranchByFilterDataResponse?>> GetsBranchByFilterData(
        GetsBranchsRequest request, CT ct);

    Task<Result<GetsBranchExcelEnumResponse?>> GetsBranchExcelEnum(
        GetsBranchExcelEnumRequest request, CT ct);

    Task<Result<GetsBranchExcelExporterResponse?>> GetsBranchExcelExporter(
        GetsBranchExcelExporterRequest request, CT ct);

    Task<Result<GetsBranchByCategoryIdsResponse?>> GetsBranchByCategoryIds(
        GetsBranchByCategoryIdsRequest request, CT ct);

}