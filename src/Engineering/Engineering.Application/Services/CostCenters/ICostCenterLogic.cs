using Engineering.Application.Services.CostCenters.Models.ActiveCostCenter;
using Engineering.Application.Services.CostCenters.Models.CodeCreator;
using Engineering.Application.Services.CostCenters.Models.CostCenterGroupDelete;
using Engineering.Application.Services.CostCenters.Models.CreateCostCenter;
using Engineering.Application.Services.CostCenters.Models.Delete;
using Engineering.Application.Services.CostCenters.Models.GetActiveCostCenters;
using Engineering.Application.Services.CostCenters.Models.GetCompaniesWork;
using Engineering.Application.Services.CostCenters.Models.GetContractorCostCenters;
using Engineering.Application.Services.CostCenters.Models.GetCostCenterByCode;
using Engineering.Application.Services.CostCenters.Models.GetCostCenterById;
using Engineering.Application.Services.CostCenters.Models.GetCostCenterByName;
using Engineering.Application.Services.CostCenters.Models.GetCostCenterHistories;
using Engineering.Application.Services.CostCenters.Models.GetCostCenters;
using Engineering.Application.Services.CostCenters.Models.GetFilteredCostCenterCities;
using Engineering.Application.Services.CostCenters.Models.GetsActiveAuthorizedCostCenter;
using Engineering.Application.Services.CostCenters.Models.GetsActiveMainWarehouseCostCenter;
using Engineering.Application.Services.CostCenters.Models.GetsAuthorizedCostCenter;
using Engineering.Application.Services.CostCenters.Models.GetsByAuthorizedRoleId;
using Engineering.Application.Services.CostCenters.Models.GetsByAuthorizedUserId;
using Engineering.Application.Services.CostCenters.Models.GetsByCityId;
using Engineering.Application.Services.CostCenters.Models.GetsByEmployerId;
using Engineering.Application.Services.CostCenters.Models.GetsByNameOrCode;
using Engineering.Application.Services.CostCenters.Models.GetsByTypeId;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterByContractorId;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterByIds;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterByProjectManagerId;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterByWarehouse;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterExcelEnum;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterExcelExporter;
using Engineering.Application.Services.CostCenters.Models.InactiveCostCenter;
using Engineering.Application.Services.CostCenters.Models.StateChangerCostCenters;
using Engineering.Application.Services.CostCenters.Models.UpdateCostCenter;

namespace Engineering.Application.Services.CostCenters;

public interface ICostCenterLogic
{
    ///Commands
    Task<Result<CreateCostCenterResponse?>> CreateCostCenter(
        CreateCostCenterRequest request, CT ct);

    Task<Result<UpdateCostCenterResponse?>> UpdateCostCenter(
        UpdateCostCenterRequest request, CT ct);

    Task<Result<InactiveCostCenterResponse?>> InactiveCostCenter(
        InactiveCostCenterRequest request, CT ct);

    Task<Result<ActiveCostCenterResponse?>> ActiveCostCenter(
        ActiveCostCenterRequest request, CT ct);

    Task<Result<CostCenterCodeCreatorResponse?>> CodeCreator(
        CostCenterCodeCreatorRequest request, CT ct);

    Task<Result<StateChangerCostCentersResponse?>> StateChangerCostCenters(
        StateChangerCostCentersRequest request, CT ct);

    Task<Result<DeleteCostCenterResponse?>> DeleteCostCenter(
        DeleteCostCenterRequest request, CT ct);

    Task<Result<CostCenterGroupDeleteResponse?>> CostCenterGroupDelete(
        CostCenterGroupDeleteRequest request, CT ct);

    ///Queries
    Task<Result<GetCostCenterByIdResponse?>> GetCostCenterById(
        GetCostCenterByIdRequest request, CT ct);

    Task<Result<GetCostCenterByNameResponse?>> GetCostCenterByName(
        GetCostCenterByNameRequest request, CT ct);

    Task<Result<GetActiveCostCentersResponse?>> GetActiveCostCenters(
        GetActiveCostCentersRequest request, CT ct);

    Task<Result<GetsActiveMainWarehouseCostCenterResponse?>> GetsActiveMainWarehouseCostCenter(
        GetsActiveMainWarehouseCostCenterRequest request, CT ct);

    Task<Result<GetContractorCostCentersResponse?>> GetContractorCostCenters(
        GetContractorCostCentersRequest request, CT ct);

    Task<Result<GetsActiveAuthorizedCostCenterResponse?>> GetsActiveAuthorizedCostCenter(
        GetsActiveAuthorizedCostCenterRequest request, CT ct);

    Task<Result<GetsAuthorizedCostCenterResponse?>> GetsAuthorizedCostCenter(
        GetsAuthorizedCostCenterRequest request, CT ct);

    Task<Result<GetCostCenterByCodeResponse?>> GetCostCenterByCode(
        GetCostCenterByCodeRequest request, CT ct);

    Task<Result<GetCostCentersResponse?>> GetCostCenters(
        GetCostCentersRequest request, CT ct);

    Task<Result<GetsCostCenterByNameOrCodeResponse?>> GetsByNameOrCode(
        GetsCostCenterByNameOrCodeRequest request, CT ct);

    Task<Result<GetsByAuthorizedUserIdResponse?>> GetsByAuthorizedUserId(
        GetsByAuthorizedUserIdRequest request, CT ct);

    Task<Result<GetsByAuthorizedRoleIdResponse?>> GetsByAuthorizedRoleId(
        GetsByAuthorizedRoleIdRequest request, CT ct);

    Task<Result<GetsCostCenterByEmployerIdResponse?>> GetsByEmployerId(
        GetsCostCenterByEmployerIdRequest request, CT ct);

    Task<Result<GetsCostCenterByContractorIdResponse?>> GetsCostCenterByContractorId(
        GetsCostCenterByContractorIdRequest request, CT ct);

    Task<Result<GetsCostCenterByProjectManagerIdResponse?>> GetsCostCenterByProjectManagerId(
        GetsCostCenterByProjectManagerIdRequest request, CT ct);

    Task<Result<GetsByCityIdResponse?>> GetsByCityId(
        GetsByCityIdRequest request, CT ct);

    Task<Result<GetsByTypeIdResponse?>> GetsByTypeId(
        GetsByTypeIdRequest request, CT ct);

    Task<Result<GetsCostCenterByIdsResponse?>> GetsCostCenterByIds(
        GetsCostCenterByIdsRequest request, CT ct);

    Task<Result<GetsCostCenterByWarehouseResponse?>> GetsCostCenterByWarehouse(
        GetsCostCenterByWarehouseRequest request, CT ct);

    Task<Result<GetFilteredCostCenterCitiesResponse?>> GetFilteredCostCenterCities(
        GetFilteredCostCenterCitiesRequest request, CT ct);

    Task<Result<GetsCostCenterExcelEnumResponse?>> GetsCostCenterExcelEnum(
        GetsCostCenterExcelEnumRequest request, CT ct);

    Task<Result<GetsCostCenterExcelExporterResponse?>> GetsCostCenterExcelExporter(
        GetsCostCenterExcelExporterRequest request, CT ct);

    Task<Result<GetCostCenterHistoriesResponse?>> GetCostCenterHistories(
        GetCostCenterHistoriesRequest request, CT ct);

    Task<Result<GetCompaniesWorkResponse?>> GetCompaniesWork(
        GetCompaniesWorkRequest request, CT ct);
}