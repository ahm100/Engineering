using Engineering.Application.Services.FiduciaryProducts.Models.CreateFiduciaryProduct;
using Engineering.Application.Services.FiduciaryProducts.Models.DeleteFiduciaryProduct;
using Engineering.Application.Services.FiduciaryProducts.Models.FiduciaryProductGroupDelete;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFiduciaryProductById;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFiduciaryProductDetailStatus;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFiduciaryProductStatus;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductDetailHistories;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductHistories;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProducts;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductsExcelEnums;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductsExcelExporter;
using Engineering.Application.Services.FiduciaryProducts.Models.UpdateFiduciaryProduct;

namespace Engineering.Application.Services.FiduciaryProducts;

public interface IFiduciaryProductLogic
{
    Task<Result<CreateFiduciaryProductResponse?>> CreateFiduciaryProduct(
        CreateFiduciaryProductRequest request, CT ct);

    Task<Result<UpdateFiduciaryProductResponse?>> UpdateFiduciaryProduct(
        UpdateFiduciaryProductRequest request, CT ct);

    Task<Result<DeleteFiduciaryProductResponse?>> DeleteFiduciaryProductAsync(
        DeleteFiduciaryProductRequest request, CT ct);

    Task<Result<FiduciaryProductGroupDeleteResponse?>> FiduciaryProductGroupDelete(
        FiduciaryProductGroupDeleteRequest request, CT ct);

    Task<Result<GetFiduciaryProductStatusResponse?>> GetFiduciaryProductStatus(
        GetFiduciaryProductStatusRequest request, CT ct);

    Task<Result<GetFiduciaryProductDetailStatusResponse?>> GetFiduciaryProductDetailStatus(
        GetFiduciaryProductDetailStatusRequest request, CT ct);

    Task<Result<GetFilteredFiduciaryProductHistoriesResponse?>> GetFilteredFiduciaryProductHistories(
        GetFilteredFiduciaryProductHistoriesRequest request, CT ct);

    Task<Result<GetFilteredFiduciaryProductDetailHistoriesResponse?>> GetFilteredFiduciaryProductDetailHistories(
        GetFilteredFiduciaryProductDetailHistoriesRequest request, CT ct);

    Task<Result<GetFilteredFiduciaryProductsResponse?>> GetFilteredFiduciaryProducts(
        GetFilteredFiduciaryProductsRequest request, CT ct);

    Task<Result<GetFiduciaryProductByIdResponse?>> GetFiduciaryProductById(
        GetFiduciaryProductByIdRequest request, CT ct);

    Task<Result<GetFilteredFiduciaryProductsExcelExporterResponse?>> GetFilteredFiduciaryProductsExcelExporter(
        GetFilteredFiduciaryProductsExcelExporterRequest request, CT ct);

    Task<Result<GetFilteredFiduciaryProductsExcelEnumsResponse?>> GetFilteredFiduciaryProductsExcelEnums(
        GetFilteredFiduciaryProductsExcelEnumsRequest request, CT ct);
}
