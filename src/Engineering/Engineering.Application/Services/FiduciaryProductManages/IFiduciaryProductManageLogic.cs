using Engineering.Application.Services.FiduciaryProductManages.Models.CreateFiduciaryProductDetailReturn;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailManagementStatus;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnByDetailId;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnDocumentById;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnType;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFilteredFiduciaryProductManageWarehouses;
using Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductConfirmed;
using Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductPending;
using Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductRejected;
using Engineering.Application.Services.FiduciaryProducts.Models.SetFiduciaryProductDetailDelivary;
using Engineering.Application.Services.FiduciaryProducts.Models.SetFiduciaryProductDetailNoDelivary;

namespace Engineering.Application.Services.FiduciaryProductManages;

public interface IFiduciaryProductManageLogic
{
    Task<Result<GetFiduciaryProductDetailReturnTypeResponse?>> GetFiduciaryProductDetailReturnType(
        GetFiduciaryProductDetailReturnTypeRequest request, CT ct);

    Task<Result<SetFiduciaryProductRejectedResponse?>> SetFiduciaryProductRejected(
        SetFiduciaryProductRejectedRequest request, CT ct);

    Task<Result<SetFiduciaryProductPendingResponse?>> SetFiduciaryProductPending(
        SetFiduciaryProductPendingRequest request, CT ct);

    Task<Result<SetFiduciaryProductConfirmedResponse?>> SetFiduciaryProductConfirmed(
        SetFiduciaryProductConfirmedRequest request, CT ct);

    Task<Result<SetFiduciaryProductDetailDelivaryResponse?>> SetFiduciaryProductDetailDelivary(
        SetFiduciaryProductDetailDelivaryRequest request, CT ct);

    Task<Result<SetFiduciaryProductDetailNoDelivaryResponse?>> SetFiduciaryProductDetailNoDelivary(
        SetFiduciaryProductDetailNoDelivaryRequest request, CT ct);

    Task<Result<CreateFiduciaryProductDetailReturnResponse?>> CreateFiduciaryProductDetailReturn(
        CreateFiduciaryProductDetailReturnRequest request, CT ct);

    Task<Result<GetFiduciaryProductDetailReturnByDetailIdResponse?>> GetFiduciaryProductDetailReturnByDetailId(
        GetFiduciaryProductDetailReturnByDetailIdRequest request, CT ct);

    Task<Result<GetFiduciaryProductDetailReturnDocumentByIdResponse?>> GetFiduciaryProductDetailReturnDocumentById(
        GetFiduciaryProductDetailReturnDocumentByIdRequest request, CT ct);

    Task<Result<GetFiduciaryProductManagementByIdResponse?>> GetFiduciaryProductManagementById(
        GetFiduciaryProductManagementByIdRequest request, CT ct);

    Task<Result<GetFilteredFiduciaryProductManageWarehousesResponse?>> GetFilteredFiduciaryProductManageWarehouses(
        GetFilteredFiduciaryProductManageWarehousesRequest request, CT ct);

    Task<Result<GetFiduciaryProductDetailManagementStatusResponse?>> GetFiduciaryProductDetailManagementStatus(
        GetFiduciaryProductDetailManagementStatusRequest request, CT ct);
}
