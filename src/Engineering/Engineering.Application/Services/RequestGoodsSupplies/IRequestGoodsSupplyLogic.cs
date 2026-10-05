using Engineering.Application.Services.RequestGoodsSupplies.Contracts.CreatePRequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Contracts.UpdatePRequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRequestGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;
using Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRequestGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRGS;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSId;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSTypeId;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFilteredRequestGoodsSuppliesReports;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrProducts;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrRGS;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceTypeHistory;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsHistoryById;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyById;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyCreators;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyProductGroups;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyProducts;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyStatus;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyType;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSById;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSTypeByRGSId;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSupplyForManagement;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodSupplyRequester;
using Engineering.Application.Services.RequestGoodsSupplies.Models.PRGSupplyImport;
using Engineering.Application.Services.RequestGoodsSupplies.Models.RequestGoodsSuppliesGroupDelete;
using Engineering.Application.Services.RequestGoodsSupplies.Models.RGSStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplies.Models.RGSupplyDetailsStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplies.Models.RGSupplyImport;
using Engineering.Application.Services.RequestGoodsSupplies.Models.SetGoodsSupplyToCreated;
using Engineering.Application.Services.RequestGoodsSupplies.Models.SetOperationInfoSeasonToGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Models.UpdateRGSType;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies;

public interface IRequestGoodsSupplyLogic
{
    Task<Result<CreateRGSTypeResponse?>> CreateRGSType(
        CreateRGSTypeRequest request, CT ct);

    Task<Result<UpdateRGSTypeResponse?>> UpdateRGSType(
        UpdateRGSTypeRequest request, CT ct);

    Task<Result<DeleteRGSResponse?>> DeleteRGS(
        DeleteRGSRequest request, CT ct);

    Task<Result<RGSStatusChangerResponse?>> RGSStatusChanger(
        RGSStatusChangerRequest request, CT ct);

    Task<Result<CreateRequestGoodsSupplyResponse?>> CreateRequestGoodsSupply(
        CreateRequestGoodsSupplyRequest request, CT ct);

    Task<Result<RGSupplyImportResponse?>> RGSupplyImport(
        RGSupplyImportRequest request, CT ct);

    Task<Result<CreateProjectRequestGoodsSuppliesResponse?>> CreateProjectRequestGoodsSupply(
        CreateProjectRequestGoodsSuppliesRequest request, CT ct);

    Task<Result<PRGSupplyImportResponse?>> PRGSupplyImport(
        PRGSupplyImportRequest request, CT ct);

    Task<Result<UpdateRequestGoodsSupplyResponse?>> UpdateRequestGoodsSupply(
        UpdateRequestGoodsSupplyRequest request, CT ct);

    Task<Result<UpdateProjectRequestGoodsSuppliesResponse?>> UpdateProjectRequestGoodsSupply(
        UpdateProjectRequestGoodsSuppliesRequest request, CT ct);

    Task<Result<SetGoodsSupplyToCreatedResponse?>> SetGoodsSupplyToCreated(
        SetGoodsSupplyToCreatedRequest request, CT ct);

    Task<Result<DeleteRequestGoodsSupplyResponse?>> DeleteRequestGoodsSupply(
        DeleteRequestGoodsSupplyRequest request, CT ct);

    Task<Result<SetOperationInfoSeasonToGoodsSupplyResponse?>> SetOperationInfoSeasonToGoodsSupply(
        SetOperationInfoSeasonToGoodsSupplyRequest request, CT ct);

    Task<Result<RequestGoodsSuppliesGroupDeleteResponse?>> RequestGoodsSuppliesGroupDelete(
        RequestGoodsSuppliesGroupDeleteRequest request, CT ct);

    Task<Result<GetRequestGoodsSupplyByIdResponse?>> GetRequestGoodsSupplyById(
        GetRequestGoodsSupplyByIdRequest request, CT ct);

    Task<Result<GetRequestGoodsHistoryByIdResponse?>> GetRequestGoodsHistoryById(
        GetRequestGoodsHistoryByIdRequest request, CT ct);

    Task<Result<GetsRequestGoodSupplyRequesterResponse?>> GetsRequestGoodSupplyRequester(
        GetsRequestGoodSupplyRequesterRequest request, CT ct);

    Task<Result<GetRequestGoodsSupplyCreatorsResponse?>> GetRequestGoodsSupplyCreators(
        GetRequestGoodsSupplyCreatorsRequest request, CT ct);

    Task<Result<GetRequestGoodsSupplyProductGroupsResponse?>> GetRequestGoodsSupplyProductGroups(
        GetRequestGoodsSupplyProductGroupsRequest request, CT ct);

    Task<Result<GetRequestGoodsSupplyProductsResponse?>> GetRequestGoodsSupplyProducts(
        GetRequestGoodsSupplyProductsRequest request, CT ct);

    Task<Result<GetRequestGoodsSupplyTypeResponse?>> GetRequestGoodsSupplyType(
        GetRequestGoodsSupplyTypeRequest request, CT ct);

    Task<Result<GetRequestGoodsSupplyStatusResponse?>> GetRequestGoodsSupplyStatus(
        GetRequestGoodsSupplyStatusRequest request, CT ct);

    Task<Result<GetFilteredRequestGoodsSuppliesReportsResponse?>> GetFilteredRequestGoodsSuppliesReports(
        GetFilteredRequestGoodsSuppliesReportsRequest request, CT ct);

    Task<Result<GetFltrRGSupplyWithProductsResponse?>> GetFltrRGSupplyWithProducts(
        GetFltrRGSupplyWithProductsRequest request, CT ct);

    Task<Result<GetFltrProductsResponse?>> GetFltrProducts(
        GetFltrProductsRequest request, CT ct);

    Task<Result<RGSupplyDetailsStatusChangerResponse?>> RGSupplyDetailsStatusChanger(
        RGSupplyDetailsStatusChangerRequest request, CT ct);

    Task<Result<GetRGSByIdResponse?>> GetRGSById(
        GetRGSByIdRequest request, CT ct);

    Task<Result<GetDetailByRGSTypeIdResponse?>> GetDetailByRGSTypeId(
        GetDetailByRGSTypeIdRequest request, CT ct);

    Task<Result<GetReferenceTypeHistoryResponse?>> GetReferenceTypeHistory(
        GetReferenceTypeHistoryRequest request, CT ct);

    Task<Result<GetFltrRGSResponse?>> GetFltrRGS(
        GetFltrRGSRequest request, CT ct);

    Task<Result<GetRGSTypeByRGSIdResponse?>> GetRGSTypeByRGSId(
        GetRGSTypeByRGSIdRequest request, CT ct);

    Task<Result<GetDetailByRGSIdResponse?>> GetDetailByRGSId(
        GetDetailByRGSIdRequest request, CT ct);
}
