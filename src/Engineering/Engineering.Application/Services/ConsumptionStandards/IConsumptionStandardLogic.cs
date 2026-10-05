using Engineering.Application.Services.ConsumptionStandards.Models.ConsumptionStandards.GetsConsumptionStandardByOperationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Models.ConsumptionStandards.UpdateConsumptionStandards;
using Engineering.Application.Services.ConsumptionStandards.Models.Experts.CreateExpert;
using Engineering.Application.Services.ConsumptionStandards.Models.Experts.DisableExpert;
using Engineering.Application.Services.ConsumptionStandards.Models.Experts.ExpertGetsByOprationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Models.Experts.UpdateExpert;
using Engineering.Application.Services.ConsumptionStandards.Models.GetProductAllowedTypes;
using Engineering.Application.Services.ConsumptionStandards.Models.GetStandardProductTypes;
using Engineering.Application.Services.ConsumptionStandards.Models.Machinery.CreateMachinery;
using Engineering.Application.Services.ConsumptionStandards.Models.Machinery.DisableMachinery;
using Engineering.Application.Services.ConsumptionStandards.Models.Machinery.MachineryGetsByOprationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Models.Machinery.UpdateMachinery;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.CreateProduct;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.DisableProduct;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.GetsNonStandardProductByOprationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.GetsProductByOprationInfoId;
using Engineering.Application.Services.ConsumptionStandards.Models.Products.UpdateProduct;

namespace Engineering.Application.Services.ConsumptionStandards;

public interface IConsumptionStandardLogic
{
    Task<Result<CreateConsumptionStandardExpertResponse?>> CreateExpertStandard(
        CreateConsumptionStandardExpertRequest request, CT ct);

    Task<Result<DisableConsumptionStandardExpertResponse?>> DisableExpertStandard(
        DisableConsumptionStandardExpertRequest request, CT ct);

    Task<Result<UpdateConsumptionStandardExpertResponse?>> UpdateExpertStandard(
        UpdateConsumptionStandardExpertRequest request, CT ct);

    Task<Result<ExpertGetsByOprationInfoIdResponse?>> ExpertGetsByOperationInfoId(
        ExpertGetsByOprationInfoIdRequest request, CT ct);

    Task<Result<CreateConsumptionStandardProductResponse?>> CreateProductStandard(
        CreateConsumptionStandardProductRequest request, CT ct);

    Task<Result<DisableConsumptionStandardProductResponse?>> DisableProductStandard(
        DisableConsumptionStandardProductRequest request, CT ct);

    Task<Result<UpdateConsumptionStandardProductResponse?>> UpdateProductStandard(
        UpdateConsumptionStandardProductRequest request, CT ct);

    Task<Result<GetsProductByOprationInfoIdResponse?>> GetsProductByOperationInfoId(
        GetsProductByOprationInfoIdRequest request, CT ct);

    Task<Result<GetsNonStandardProductByOprationInfoIdResponse?>> GetsNonStandardProductByOprationInfoId(
        GetsNonStandardProductByOprationInfoIdRequest request, CT ct);

    Task<Result<GetProductAllowedTypesResponse?>> GetProductAllowedTypes(
        GetProductAllowedTypesRequest request, CT ct);

    Task<Result<GetStandardProductTypesResponse?>> GetStandardProductTypes(
        GetStandardProductTypesRequest request, CT ct);

    Task<Result<CreateConsumptionStandardMachineryResponse?>> CreateMachineryStandard(
        CreateConsumptionStandardMachineryRequest request, CT ct);

    Task<Result<DisableConsumptionStandardMachineryResponse?>> DisableMachineryStandard(
        DisableConsumptionStandardMachineryRequest request, CT ct);

    Task<Result<UpdateConsumptionStandardMachineryResponse?>> UpdateMachineryStandard(
        UpdateConsumptionStandardMachineryRequest request, CT ct);

    Task<Result<MachineryGetsByOprationInfoIdResponse?>> MachineryGetsByOperationInfoId(
        MachineryGetsByOprationInfoIdRequest request, CT ct);

    Task<Result<UpdateConsumptionStandardsResponse?>> UpdateConsumptionStandards(
        UpdateConsumptionStandardsRequest request, CT ct);

    Task<Result<GetsConsumptionStandardByOperationInfoIdResponse?>> GetsConsumptionStandardByOperationInfoId(
        GetsConsumptionStandardByOperationInfoIdRequest request, CT ct);

}