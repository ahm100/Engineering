using Engineering.Application.Services.TransportationRequests.Models.GetAirplaneById;
using Engineering.Application.Services.TransportationRequests.Models.GetById;
using Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportationById;
using Engineering.Application.Services.TransportationRequests.Models.GetsFiltered;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredAirplane;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredSnap;
using Engineering.Application.Services.TransportationRequests.Models.GetSnapById;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalAirplanePrice;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalSnapPrice;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalTransportationRequestPrice;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestHistory;
using Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportation;
using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Abstractions.Data.Transportations;

public interface ITransportationRequestRepository : IBaseRepository<TransportationRequest>
{
    Task<long> RequestNumberCreator(
        bool? isCredit,
        long? companyId,
        CT ct);

    Task<List<TransportationRequest>> GetsByRequestIdAsync(
        long requestById,
        DateTime startDate,
        DateTime endDate,
        TransportationRequestStatus status,
        CT ct);
    Task<TransportationRequest?> GetById(long id, CT ct);

    Task<TransportationRequest?> GetByIdNoInclude(
        long id, CT ct);

    Task<TransportationRequest?> GetLogesticById(long id, CT ct);
    Task<TransportationRequest?> GetByIdlessInclude(long id, CT ct);
    Task<TransportationRequest?> GetByIdIncludeLess(long id, CT ct);
    Task<TransportationRequest?> GetByPackingId(long id, CT ct);
    Task<GetTransportationRequestByIdResponse?> GetByIdWithoutInclude(long id, CT ct);
    Task<GetAirplaneByIdResponse?> GetAirPlaneByIdWithoutInclude(long id, CT ct);
    Task<GetSnapByIdResponse?> GetSnapByIdWithoutInclude(long id, CT ct);
    Task<TransportationRequest?> GetByIdForPayment(long id, CT ct);
    Task<List<TransportationRequest>?> GetByIdsForPayment(List<long> ids, CT ct);
    Task<TransportationRequest?> GetByIdForChangeStatus(long id, CT ct);
    Task<TransportationRequest?> FindForDelete(long id, CT ct);
    Task<TransportationRequest?> GetSnapsWithRefrenceId(long refrenceId, CT ct);

    Task<(List<GetsFilteredTransportationRequestResponseModel> Data, int RowCount)> GetsFilteredTransportationRequest(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? costGroupIds,
        List<long>? costCategoryIds,
        TransportationRequestStatus? transportationRequestStatus,
        TransportationPaymentType? paymentType,
        long? tripId,
        long? billOfLadingId,
        long? transportationId,
        long? requestById,
        DateTime? startDate,
        DateTime? endDate,
        DateTime? fromDate,
        DateTime? toDate,
        long? requestNumber,
        decimal? fromPrice,
        decimal? toPrice,
        string? driverName,
        List<long>? driverIds,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsWarehouseTransportationResponseModel> Data, int RowCount)> GetsWarehouseTransportation(
        List<long>? ids,
        long? contractorId,
        List<TransportationRequestStatus>? transportationRequestStatus,
        long? transportationId,
        long? requestById,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsAggregateWarehouseTransportationResponseModel> Data, int RowCount)> GetsAggregateWarehouseTransportation(
        List<long>? ids,
        List<long>? productIds,
        long? contractorId,
        List<TransportationRequestStatus>? transportationRequestStatus,
        long? transportationId,
        long? machineTypeId,
        long? requestById,
        DateTime? fromDate,
        DateTime? toDate,
        string? freightNumber,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<GetsAggregateWarehouseTransportationByIdResponse> GetsAggregateWarehouseTransportationById(
       long id,
       CT ct);

    Task<(List<GetsFilteredSnapResponseModel> Data, int RowCount)> GetsFilteredSnap(
         List<long>? ids,
         List<long>? costCenterIds,
         List<long>? projectIds,
         List<long>? projectOperationIds,
         List<long>? projectOperationDetailIds,
         List<long>? costGroupIds,
         List<long>? costCategoryIds,
         List<long>? tripIds,
         List<long>? transportationIds,
         List<long>? passengerIds,
         long? requestById,
         TransportationRequestStatus? transportationRequestStatus,
         TransportationPaymentType? paymentType,
         DateTime? startDate,
         DateTime? endDate,
         DateTime? fromCreateDate,
         DateTime? toCreateDate,
         long? requestNumber,
         decimal? fromPrice,
         decimal? toPrice,
         string? driverName,
         string? filterData,
         string[]? orderBy,
         int pageIndex,
         int pageSize, CT ct);

    Task<(List<GetsFilteredAirplaneResponseModel> Data, int RowCount)> GetsFilteredAirplane(
         List<long>? ids,
         List<long>? costCenterIds,
         List<long>? projectIds,
         List<long>? projectOperationIds,
         List<long>? projectOperationDetailIds,
         List<long>? costGroupIds,
         List<long>? costCategoryIds,
         List<long>? tripIds,
         List<long>? transportationIds,
         List<long>? passengerIds,
         long? requestById,
         TransportationRequestStatus? transportationRequestStatus,
         TransportationPaymentType? paymentType,
         DateTime? startDate,
         DateTime? endDate,
         DateTime? fromCreateDate,
         DateTime? toCreateDate,
         long? requestNumber,
         decimal? fromPrice,
         decimal? toPrice,
         string? driverName,
         string? filterData,
         string[]? orderBy,
         int pageIndex,
         int pageSize, CT ct);

    Task<(List<long> Data, int RowCount)> GetsFilteredRequester(CT ct);


    Task<GetsTotalTransportationRequestPriceResponse> GetsTotalTransportationRequestPrice(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? costGroupIds,
        List<long>? costCategoryIds,
        TransportationRequestStatus? transportationRequestStatus,
        TransportationPaymentType? paymentType,
        long? tripId,
        long? billOfLadingId,
        long? transportationId,
        long? requestById,
        DateTime? startDate,
        DateTime? endDate,
        long? requestNumber,
        decimal? fromPrice,
        decimal? toPrice,
        string? driverName,
        List<long>? driverIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<GetsTotalSnapPriceResponse> GetsTotalSnapPrice(
         List<long>? ids,
         List<long>? costCenterIds,
         List<long>? projectIds,
         List<long>? projectOperationIds,
         List<long>? projectOperationDetailIds,
         List<long>? costGroupIds,
         List<long>? costCategoryIds,
         List<long>? tripIds,
         List<long>? transportationIds,
         List<long>? passengerIds,
         long? requestById,
         TransportationRequestStatus? transportationRequestStatus,
         TransportationPaymentType? paymentType,
         DateTime? startDate,
         DateTime? endDate,
         DateTime? fromCreateDate,
         DateTime? toCreateDate,
         long? requestNumber,
         decimal? fromPrice,
         decimal? toPrice,
         string? driverName,
         string? filterData,
         int pageIndex,
         int pageSize, CT ct);

    Task<GetsTotalAirplanePriceResponse> GetsTotalAirplanePrice(
         List<long>? ids,
         List<long>? costCenterIds,
         List<long>? projectIds,
         List<long>? projectOperationIds,
         List<long>? projectOperationDetailIds,
         List<long>? costGroupIds,
         List<long>? costCategoryIds,
         List<long>? tripIds,
         List<long>? transportationIds,
         List<long>? passengerIds,
         long? requestById,
         TransportationRequestStatus? transportationRequestStatus,
         TransportationPaymentType? paymentType,
         DateTime? startDate,
         DateTime? endDate,
         DateTime? fromCreateDate,
         DateTime? toCreateDate,
         long? requestNumber,
         decimal? fromPrice,
         decimal? toPrice,
         string? driverName,
         string? filterData,
         int pageIndex,
         int pageSize, CT ct);

    Task<GetsTransportationRequestHistoryResponse> GetsTransportationRequestHistory(
         long id,
         CT ct);

    Task<List<TransportationRequest>?> GetByIds(List<long> ids, CT ct);

    Task<int> GetCountOfYearRequest(CT ct);

    Task<string?> GetLastFreightNumber(long contractorId, CT ct);

}