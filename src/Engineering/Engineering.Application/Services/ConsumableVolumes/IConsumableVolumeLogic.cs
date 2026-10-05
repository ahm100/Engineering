using Engineering.Application.Services.ConsumableVolumes.Models.Experts.CreateExpertConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.DeleteExpertConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetExpertConsumableVolumeById;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetExpertsByProjectOperationId;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetExpertsByProjectOperationIds;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetsByProjectOperationDetailId;
using Engineering.Application.Services.ConsumableVolumes.Models.Experts.UpdateExpertConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.GetProjectOperationDetailVolumes;
using Engineering.Application.Services.ConsumableVolumes.Models.GetVolumeProductTypes;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.CreateMachineryConsumable;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.DeleteMachineryConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetFilteredTotalOfConsumebleMachineries;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetMachineriesByProjectOperationDetailId;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetMachineriesByProjectOperationId;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetMachineryConsumableVolumeById;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetsFilteredMachineriyVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Machineries.UpdateMachineryConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.CreateProductConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.DeleteProductConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.GetProductConsumableVolumeById;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.GetProductsByProjectOperationDetailId;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.GetProductsByProjectOperationId;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.GetsProductsByFiltered;
using Engineering.Application.Services.ConsumableVolumes.Models.Products.UpdateProductConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Models.UpdateConsumableVolumes;

namespace Engineering.Application.Services.ConsumableVolumes;

public interface IConsumableVolumeLogic
{
    Task<Result<UpdateConsumableVolumesResponse?>> UpdateConsumableVolumes(
        UpdateConsumableVolumesRequest request, CT ct);

    Task<Result<GetProjectOperationDetailVolumesResponse?>> GetProjectOperationDetailVolumes(
        GetProjectOperationDetailVolumesRequest request, CT ct);

    #region Experts

    Task<Result<CreateConsumableVolumeExpertResponse?>> CreateConsumableVolumeExpert(
        CreateConsumableVolumeExpertRequest request, CT ct);

    Task<Result<UpdateConsumableVolumeExpertResponse?>> UpdateConsumableVolumeExpert(
        UpdateConsumableVolumeExpertRequest request, CT ct);

    Task<Result<DeleteConsumableVolumeExpertResponse?>> DeleteConsumableVolumeExpert(
        DeleteConsumableVolumeExpertRequest request, CT ct);

    Task<Result<GetConsumableVolumeExpertByIdResponse?>> GetConsumableVolumeExpertById(
        GetConsumableVolumeExpertByIdRequest request, CT ct);

    Task<Result<GetExpertsByProjectOperationDetailIdResponse?>> GetExpertsByProjectOperationDetailId(
        GetExpertsByProjectOperationDetailIdRequest request, CT ct);

    Task<Result<GetExpertsByProjectOperationIdResponse?>> GetExpertsByProjectOperationId(
        GetExpertsByProjectOperationIdRequest request, CT ct);

    Task<Result<GetExpertsByProjectOperationIdsResponse?>> GetExpertsByProjectOperationIds(
        GetExpertsByProjectOperationIdsRequest request, CT ct);

    #endregion

    #region Machineries

    Task<Result<CreateConsumableVolumeMachineryResponse?>> CreateConsumableVolumeMachinery(
        CreateConsumableVolumeMachineryRequest request, CT ct);

    Task<Result<UpdateConsumableVolumeMachineryResponse?>> UpdateConsumableVolumeMachinery(
        UpdateConsumableVolumeMachineryRequest request, CT ct);

    Task<Result<DeleteConsumableVolumeMachineryResponse?>> DeleteConsumableVolumeMachinery(
        DeleteConsumableVolumeMachineryRequest request, CT ct);

    Task<Result<GetConsumableVolumeMachineryByIdResponse?>> GetConsumableVolumeMachineryById(
        GetConsumableVolumeMachineryByIdRequest request, CT ct);

    Task<Result<GetMachineriesByProjectOperationDetailIdResponse?>> GetMachineriesByProjectOperationDetailId(
        GetMachineriesByProjectOperationDetailIdRequest request, CT ct);

    Task<Result<GetMachineriesByProjectOperationIdResponse?>> GetMachineriesByProjectOperationId(
        GetMachineriesByProjectOperationIdRequest request, CT ct);

    Task<Result<GetsFilteredMachineriyVolumeResponse?>> GetsFilteredMachineriyVolume(
        GetsFilteredMachineriyVolumeRequest request, CT ct);

    Task<Result<GetFilteredTotalOfConsumebleMachineriesResponse?>> GetFilteredTotalOfConsumebleMachineries(
        GetFilteredTotalOfConsumebleMachineriesRequest request, CT ct);

    #endregion

    #region Products

    Task<Result<CreateConsumableVolumeProductResponse?>> CreateConsumableVolumeProduct(
        CreateConsumableVolumeProductRequest request, CT ct);

    Task<Result<UpdateConsumableVolumeProductResponse?>> UpdateConsumableVolumeProduct(
        UpdateConsumableVolumeProductRequest request, CT ct);

    Task<Result<DeleteConsumableVolumeProductResponse?>> DeleteConsumableVolumeProduct(
        DeleteConsumableVolumeProductRequest request, CT ct);

    Task<Result<GetConsumableVolumeProductByIdResponse?>> GetConsumableVolumeProductById(
        GetConsumableVolumeProductByIdRequest request, CT ct);

    Task<Result<GetProductsByProjectOperationDetailIdResponse?>> GetProductsByProjectOperationDetailId(
        GetProductsByProjectOperationDetailIdRequest request, CT ct);

    Task<Result<GetProductsByProjectOperationIdResponse?>> GetProductsByProjectOperationId(
        GetProductsByProjectOperationIdRequest request, CT ct);

    Task<Result<GetsProductsByFilteredResponse?>> GetsProductsByFiltered(
        GetsProductsByFilteredRequest request, CT ct);

    Task<Result<GetVolumeProductTypesResponse?>> GetVolumeProductTypes(
        GetVolumeProductTypesRequest request, CT ct);

    #endregion
}