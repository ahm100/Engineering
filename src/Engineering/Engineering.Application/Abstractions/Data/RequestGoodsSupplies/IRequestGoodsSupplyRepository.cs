using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrRGS;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSById;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSupplyForManagement;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

public interface IRequestGoodsSupplyRepository : IBaseRepository<RequestGoodsSupply>
{
    Task<RequestGoodsSupply?> GetRequestGoodsSupplyById(
        long id,
        CT ct);

    Task<RequestGoodsSupply?> GetById(
        long id, CT ct);

    Task<RequestGoodsSupply?> GetRequestGoodsSupplyByIdForSeason(
        long id,
        CT ct);

    Task<RequestGoodsSupply?> GetRequestGoodsSupplySummary(
        long id,
        CT ct);

    Task<RequestGoodsSupply?> GetRequestGoodsSupplyForChangeStatus(
        long id,
        CT ct);

    Task<RequestGoodsSupply?> GetRequestGoodsSupplyByIdForChangeStatus(
        long id,
        CT ct);

    Task<(List<RequestGoodsSupply> Data, int RowCount)> GetFilteredRequestGoodsSupplies(
        List<long>? ids,
        List<long>? detailIds,
        List<long>? managementIds,
        List<long>? costCenterIds,
        List<long>? projectIds,
        long? cityId,
        long? projectManagerId,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        string? filterData,
        List<long>? creatorIds,
        List<long>? productIds,
        List<GoodsSupplyStatus>? statuses,
        List<GoodsSupplyStatus>? removeStatuses,
        List<GoodsSupplyType>? types,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterDescription,
        string? filterPublicName,
        string? filterOperationInfoName,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<RequestGoodsSupply> Data, int RowCount)> GetsRequestGoodsSupply(
        List<long>? costCenterIds,
        List<long>? projectIds,
        long? cityId,
        long? projectManagerId,
        long? projectOperationId,
        long? projectOperationDetailId,
        string? filterData,
        long? creatorId,
        List<GoodsSupplyStatus>? statuses,
        List<GoodsSupplyStatus>? removeStatuses,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<RequestGoodsSupply> Data, int RowCount)> GetFilteredRequestGoodsSupplyManagement(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        long? projectManagerId,
        List<long>? creatorIds,
        List<long>? productIds,
        DateTime? fromDate,
        DateTime? toDate,
        List<GoodsSupplyStatus>? statuses,
        List<GoodsSupplyStatus>? removeStatuses,
        List<GoodsSupplyType>? types,
        List<GoodsSupplyType>? removeTypes,
        string? filterData,
        long? companyId,
        string? customerInvoiceNumber,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<RequestGoodsSupply> Data, int RowCount)> GetFilteredRequestGoodsSupplies(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        long projectManagerId,
        List<long>? creatorIds,
        List<long>? productIds,
        DateTime? fromDate,
        DateTime? toDate,
        List<GoodsSupplyStatus>? statuses,
        List<GoodsSupplyStatus>? removeStatuses,
        List<GoodsSupplyType>? types,
        string? filterData,
        long? companyId,
        string? customerInvoiceNumber,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<RequestGoodsSupply>?> GetRequestGoodsSupplyByDate(
        DateTime startDate,
        DateTime endDate,
        GoodsSupplyType? type,
        List<long> projectOperationIds,
        CT ct);

    Task<List<long>> GetsRequestGoodSupplyRequester(CT ct);

    Task<(List<long> Data, int RowCount)> GetsFilteredCreators(
     List<long>? costCenterIds,
     List<long>? projectIds,
     List<long>? projectOperationIds,
     List<long>? projectOperationDetailIds,
     List<long>? requestGoodsSupplyIds,
     CT ct);

    Task<(List<RequestGoodsSupply> Data, int RowCount)> GetsFilteredProductGroup(
     long? costCenterId,
     long? projectId,
     long? projectOperationId,
     long? projectOperationDetailId,
     VolumeProductType? productType,
     CT ct);

    Task<(List<long> Data, int RowCount)> GetsFilteredProducts(
     List<long>? costCenterIds,
     List<long>? projectIds,
     List<long>? projectOperationIds,
     List<long>? projectOperationDetailIds,
     List<long>? requestGoodsSupplyIds,
     List<long>? productGroupIds,
     CT ct);

    Task<(List<RequestGoodsSupply> Data, int RowCount)> GetFilteredRequestGoodsSuppliesReports(
        List<long>? ids,
        List<long>? detailIds,
        long? costCenterId,
        long? projectId,
        long? projectOperationId,
        long? projectOperationDetailId,
        List<GoodsSupplyStatus>? statuses,
        List<long>? productIds,
        GoodsSupplyManagementType? type,
        VolumeProductType? productType,
        long? productGroupId,
        DateTime? fromDate,
        DateTime? toDate,
        long? creatorId,
        long? companyId,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<string?> GetProjectNameRequestGoodsSupply(
        long id, CT ct);

    Task<bool> DoesProjectProductHaveRequestGoodsSupply(
        List<long> projectProductIds, CT ct);

    Task<List<GetFltrRGSupplyWithProductsModel>> GetFltrRGSWithProducts(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? productGroupIds,
        List<long>? productIds,
        List<long>? creatorIds,
        List<long>? managementIds,
        List<GoodsSupplyType>? types,
        List<GoodsSupplyDetailStatus>? statuses,
        List<GoodsSupplyDetailStatus>? removeStatuses,
        DateTime? fromDate,
        DateTime? toDate,
        long? projectManagerId,
        long? thirdPartyId,
        bool checkThirdParty,
        long? cityId,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        long? companyId, CT ct);

    Task<RequestGoodsSupply?> GetRequestProducts(
    long requestGoodsSupplyId,
    CT ct);

    Task<long?> GetProjectManagerId(
    long Id, CT ct);

    Task<GetRGSByIdResponse?> GetRGSById(
    long id, CT ct);

    Task<(List<GetFltrRGSModel>? Data, int RowCount)> GetFltrRGS(
    List<long>? projectIds,
    long? cityId,
    long? projectManagerId,
    List<GoodsSupplyStatus>? statuses,
    List<GoodsSupplyType>? types,
    List<long>? creatorIds,
    DateTime? fromDate,
    DateTime? toDate,
    string? filterData,
    int pageIndex,
    int pageSize, CT ct);

    Task<int> GetLastCodeSerialByPrefix(
    string prefix,
    CT ct);
}
