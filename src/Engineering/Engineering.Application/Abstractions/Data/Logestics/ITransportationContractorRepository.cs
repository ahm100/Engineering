using Engineering.Application.Services.TransportationContractors.Contracts.GetsActiveTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsFilteredTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetTransportationContractorById;
using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Application.Abstractions.Data;

public interface ITransportationContractorRepository : IBaseRepository<TransportationContractor>
{
    Task<GetTransportationContractorByIdResponse?> GetTransportationContractorById(long id, CT ct);

    Task<TransportationContractor?> GetTransportationContractor(long id, CT ct);

    Task<TransportationContractor?> GetTransportationContractorForUpdate(long id, CT ct);

    Task<TransportationContractor?> GetTransportationContractorForImport(long id, CT ct);

    Task<(List<GetsActiveTransportationContractorResponseModel> Data, int RowCount)> GetAllActiveTransportationContractors(
        List<long>? ids,
        List<long>? thirdPartyIds,
        DateTime? startDate,
        DateTime? endDate,
        List<DeliveryMethod>? deliveryMethods,
        List<DeliveryType>? deliveryTypes,
        List<TransportationContractorCalculateType>? types,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsFilteredTransportationContractorResponseModel> Data, int RowCount)> GetFilteredTransportationContractors(
        List<long>? ids,
        List<long>? thirdPartyIds,
        DateTime? startDate,
        DateTime? endDate,
        List<DeliveryMethod>? deliveryMethods,
        List<DeliveryType>? deliveryTypes,
        List<TransportationContractorCalculateType>? types,
        PackingShippingType? packingShippingType,
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<TransportationContractor>> GetByIds(List<long> ids, CT ct);

    Task<List<TransportationContractor>> GetByIdsIncludeLess(List<long> ids, CT ct);
}