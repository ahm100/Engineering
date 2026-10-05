using Engineering.Application.Services.Seasons.Models.ActiveSeason;
using Engineering.Application.Services.Seasons.Models.CodeCreator;
using Engineering.Application.Services.Seasons.Models.CreateSeason;
using Engineering.Application.Services.Seasons.Models.DisableSeason;
using Engineering.Application.Services.Seasons.Models.GetActiveSeasons;
using Engineering.Application.Services.Seasons.Models.GetsByBranchId;
using Engineering.Application.Services.Seasons.Models.GetsByBranchIdWhithOperationInfo;
using Engineering.Application.Services.Seasons.Models.GetSeasonByCode;
using Engineering.Application.Services.Seasons.Models.GetSeasonById;
using Engineering.Application.Services.Seasons.Models.GetSeasonByName;
using Engineering.Application.Services.Seasons.Models.GetSeasons;
using Engineering.Application.Services.Seasons.Models.GetsSeasonByBranchIds;
using Engineering.Application.Services.Seasons.Models.GetsSeasonExcelEnum;
using Engineering.Application.Services.Seasons.Models.GetsSeasonExcelExporter;
using Engineering.Application.Services.Seasons.Models.InactiveSeason;
using Engineering.Application.Services.Seasons.Models.SeasonExcelImports;
using Engineering.Application.Services.Seasons.Models.SeasonGroupDelete;
using Engineering.Application.Services.Seasons.Models.StateChangerSeasons;
using Engineering.Application.Services.Seasons.Models.UpdateSeason;

namespace Engineering.Application.Services.Seasons;

public interface ISeasonLogic
{
    ///Commands
    Task<Result<CreateSeasonResponse?>> CreateSeason(CreateSeasonRequest request, CT ct);
    Task<Result<SeasonExcelImportsResponse?>> SeasonExcelImports(SeasonExcelImportsRequest request, CT ct);
    Task<Result<DisableSeasonResponse?>> DisableSeason(DisableSeasonRequest request, CT ct);
    Task<Result<UpdateSeasonResponse?>> UpdateSeason(UpdateSeasonRequest request, CT ct);
    Task<Result<InactiveSeasonResponse?>> InactiveSeason(InactiveSeasonRequest request, CT ct);
    Task<Result<ActiveSeasonResponse?>> ActiveSeason(ActiveSeasonRequest request, CT ct);
    Task<Result<SeasonCodeCreatorResponse?>> CodeCreator(SeasonCodeCreatorRequest request, CT ct);
    Task<Result<StateChangerSeasonsResponse?>> StateChangerSeasons(StateChangerSeasonsRequest request, CT ct);
    Task<Result<SeasonGroupDeleteResponse?>> SeasonGroupDelete(SeasonGroupDeleteRequest request, CT ct);

    ///Queries
    Task<Result<GetSeasonByIdResponse?>> GetSeasonById(GetSeasonByIdRequest request, CT ct);
    Task<Result<GetSeasonByNameResponse?>> GetSeasonByName(GetSeasonByNameRequest request, CT ct);
    Task<Result<GetSeasonByCodeResponse?>> GetSeasonByCode(GetSeasonByCodeRequest request, CT ct);
    Task<Result<GetActiveSeasonsResponse?>> GetActiveSeasons(GetActiveSeasonsRequest request, CT ct);
    Task<Result<GetSeasonsResponse?>> GetSeasons(GetSeasonsRequest request, CT ct);
    Task<Result<GetsByBranchIdResponse?>> GetsByBranchId(GetsByBranchIdRequest request, CT ct);
    Task<Result<GetsByBranchIdWhithOperationInfoResponse?>> GetsByBranchIdWhithOperationInfo(GetsByBranchIdWhithOperationInfoRequest request, CT ct);
    Task<Result<GetsSeasonByBranchIdsResponse?>> GetsSeasonByBranchIds(GetsSeasonByBranchIdsRequest request, CT ct);
    Task<Result<GetsSeasonExcelExporterResponse?>> GetsSeasonExcelExporter(GetsSeasonExcelExporterRequest request, CT ct);
    Task<Result<GetsSeasonExcelEnumResponse?>> GetsSeasonExcelEnum(GetsSeasonExcelEnumRequest request, CT ct);
}