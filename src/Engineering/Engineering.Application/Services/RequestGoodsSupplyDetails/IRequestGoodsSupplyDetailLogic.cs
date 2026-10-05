using Engineering.Application.Services.RequestGoodsSupplies.Contracts.CreatePRequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.CreateRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.DeleteRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.DeleteRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetAllGoodsSupplyProductDocument;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductDocuments;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductHistoryById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProjectDetailData;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProjectOperationDetailsByRequestId;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsDetailHistoryById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyDetailImportance;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyDetailStatus;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyProductById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyDetailBySupplyProductId;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyDetailStatus;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProductByIdWithScale;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsProjectOperationDetailData;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProductExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProductExcelExporter;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsTotalPriceRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplyProductGroupStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplyProductStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateProjectRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateRequestGoodsSupplyProduct;
using Engineering.Application.Services.TelegramChats.TelegramServices.Models;
using Engineering.Application.WebServices.MessageSender.MessageSenders.Models.CreateMessage;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails;

public interface IRequestGoodsSupplyDetailLogic
{
    Task<Result<UpdateRequestGoodsSupplyProductResponse?>> UpdateRequestGoodsSupplyProduct(
        UpdateRequestGoodsSupplyProductRequest request, CT ct);
    Task<Result<CreateRequestGoodsSupplyDetailResponse?>> CreateRequestGoodsDetailSupply(
        CreateRequestGoodsSupplyDetailModelRequest request, CT ct);
    Task<Result<CreateRequestGoodsSupplyDetailResponse?>> CreateProjectRequestGoodsDetailSupply(
        CreateProjectRequestGoodsSupplyDetailModelRequest request,
        long projectId, CT ct);
    Task<Result<CreateRequestGoodsSupplyDetailResponseModel?>> ProcessCreateRequestDetail(
        CreateRequestGoodsSupplyDetailModel request,
        RequestGoodsSupply value,
        RequestGoodsSupplyProduct? product, CT ct);
    Task<Result<CreateRequestGoodsSupplyDetailResponseModel?>> ProcessCreateProjectRequestDetail(
        CreateProjectRequestGoodsSupplyDetailModel request,
        RequestGoodsSupply value,
        RequestGoodsSupplyProduct? product,
        long projectId, CT ct);
    Task<Result<UpdateRequestGoodsSupplyDetailResponse?>> UpdateRequestGoodsDetailSupply(
        UpdateRequestGoodsSupplyDetailRequest request, CT ct);
    Task<Result<UpdateProjectRequestGoodsSupplyDetailResponse?>> UpdateProjectRequestGoodsDetailSupply(
        UpdateProjectRequestGoodsSupplyDetailRequest request, Project project, CT ct);
    Task<Result<RequestGoodsSupplyProductStatusChangerResponse?>> RequestGoodsSupplyProductStatusChanger(
        RequestGoodsSupplyProductStatusChangerModelRequest request, bool groupChange, CT ct);
    Task<Result<RequestGoodsSupplyProductGroupStatusChangerResponse?>> RequestGoodsSupplyProductGroupStatusChanger(
        RequestGoodsSupplyProductGroupStatusChangerRequest request, CT ct);
    Task<Result<DeleteRequestGoodsSupplyDetailResponse?>> DeleteRequestGoodsSupplyDetail(
        DeleteRequestGoodsSupplyDetailModelRequest request, CT ct);
    Task<Result<GetGoodsSupplyProductHistoryByIdResponse?>> GetGoodsSupplyProductHistoryById(
        GetGoodsSupplyProductHistoryByIdRequest request, CT ct);
    Task<Result<DeleteRequestGoodsSupplyProductResponse?>> DeleteRequestGoodsSupplyProduct(
        DeleteRequestGoodsSupplyProductModelRequest request, CT ct);
    Task<Result<GetsTotalPriceRequestGoodsSupplyProductResponse?>> GetsTotalPriceRequestGoodsSupplyProduct(
        GetsTotalPriceRequestGoodsSupplyProductRequest request, CT ct);
    Task<Result<GetsGoodsSupplyDetailStatusResponse?>> GetsGoodsSupplyDetailStatus(
        GetsGoodsSupplyDetailStatusRequest request, CT ct);
    Task<Result<GetsGoodsSupplyDetailBySupplyProductIdResponse?>> GetsGoodsSupplyDetailBySupplyProductId(
        GetsGoodsSupplyDetailBySupplyProductIdRequest request, CT ct);
    Task<Result<GetsGoodsSupplyProductByIdWithScaleResponse?>> GetsGoodsSupplyProductByIdWithScale(
        GetsGoodsSupplyProductByIdWithScaleRequest request, CT ct);
    Task<Result<GetsGoodsSupplyProductResponse?>> GetsGoodsSupplyProduct(
        GetsGoodsSupplyProductRequest request, CT ct);
    Task<Result<GetGoodsSupplyProductDocumentsResponse?>> GetGoodsSupplyProductDocuments(
        GetGoodsSupplyProductDocumentsRequest request, CT ct);
    Task<Result<GetRequestGoodsSupplyProductByIdResponse?>> GetRequestGoodsSupplyProductById(
        GetRequestGoodsSupplyProductByIdRequest request, CT ct);
    Task<Result<GetGoodsSupplyProductByIdResponse?>> GetGoodsSupplyProductById(
        GetGoodsSupplyProductByIdRequest request, CT ct);
    Task<Result<GetsRequestGoodsSupplyProductResponse?>> GetsRequestGoodsSupplyProduct(
        GetsRequestGoodsSupplyProductRequest request, CT ct);
    Task<Result<GetProjectOperationDetailsByRequestIdResponse?>> GetProjectOperationDetailsByRequestId(
        GetProjectOperationDetailsByRequestIdRequest request, CT ct);
    Task<Result<GetGoodsSupplyDetailProductsResponse?>> GetGoodsSupplyProducts(
        GetGoodsSupplyDetailProductsRequest request, CT ct);
    Task<Result<GetGoodsSupplyDetailProductsResponse?>> GetProjectGoodsSupplyProducts(
        GetGoodsSupplyDetailProductsRequest request, CT ct);
    Task<Result<GetsProjectOperationDetailDataResponse?>> GetsProjectOperationDetailData(
        GetsProjectOperationDetailDataRequest request, CT ct);
    Task<Result<GetProjectDetailDataResponse?>> GetProjectDetailData(
        GetProjectDetailDataRequest request, CT ct);
    Task<Result<GetRequestGoodsDetailHistoryByIdResponse?>> GetRequestGoodsDetailHistoryById(
        GetRequestGoodsDetailHistoryByIdRequest request, CT ct);
    Task<Result<GetRequestGoodsSupplyDetailStatusResponse?>> GetRequestGoodsSupplyDetailStatus(
        GetRequestGoodsSupplyDetailStatusRequest request, CT ct);
    Task<Result<GetRequestGoodsSupplyDetailImportanceResponse?>> GetRequestGoodsSupplyDetailImportance(
        GetRequestGoodsSupplyDetailImportanceRequest request, CT ct);
    Task<Result<GetsRequestGoodsSupplyProductExcelEnumsResponse?>> GetsRequestGoodsSupplyProductExcelEnums(
        GetsRequestGoodsSupplyProductExcelEnumsRequest request, CT ct);
    Task<Result<GetsRequestGoodsSupplyProductExcelExporterResponse?>> GetsRequestGoodsSupplyProductExcelExporter(
        GetsRequestGoodsSupplyProductExcelExporterRequest request, CT ct);
    Task<Result<GetAllGoodsSupplyProductDocumentResponse?>> GetAllGoodsSupplyProductDocument(
        GetAllGoodsSupplyProductDocumentRequest request, CT ct);

    Task<List<ProductModel>> GetProductModels(
        List<long>? productIds,
        RequestGoodsSupplyProduct goodsSupply, CT ct);
    (string FullName, string TelegramId) GetCreatorDetails(IEnumerable<FilteredUserResponseModel>? creatorResponse);
    Task NotifyTelegramChats(RequestGoodsSupplyProduct? goodsSupplyProduct, string? description, CT ct);
    Task<Result<CreateMessageResponse?>> SendNotification(string? reciever, string? content, CT ct);
}
