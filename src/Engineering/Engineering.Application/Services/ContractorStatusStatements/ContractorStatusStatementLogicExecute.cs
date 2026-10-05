using Engineering.Application.Services.ContractorStatusStatements.Contracts;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSDailyServiceUrls;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSPayments;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetIntegratedCSS.Service;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetIntegratedCSSByProjectId;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetContractorStatusStatementById;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetFilteredContractorStatusStatement;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementDiscountById;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementHistory;
using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.ContractorStatusStatements;

public partial class ContractorStatusStatementLogic : IContractorStatusStatementLogic
{

    public async Task<Result<List<GetCStatementSContractsModel>?>> GetCStatementSContractsExecute(
        GetCStatementSContractsRequest request, CT ct)
    {
        try
        {
            var result = await _cssDetailRepository.GetCStatementSContracts(request.Id, ct);

            return result is not null ? result :
                Result.Failure<List<GetCStatementSContractsModel>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetCStatementSContractsModel>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<List<GetCStatementFContractsModel>?>> GetCStatementFContractsExecute(
        GetCStatementFContractsRequest request, CT ct)
    {
        try
        {
            var result = await _cssDetailRepository.GetCStatementFContracts(request.Id, ct);

            return result is not null ? result :
                Result.Failure<List<GetCStatementFContractsModel>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetCStatementFContractsModel>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<ContractorStatusStatement?>> GetContractorStatusStatementExecute(
        long id, IncludeType includeType, CT ct)
    {
        try
        {
            ContractorStatusStatement? entity = null;
            entity = includeType switch
            {
                IncludeType.Delete => await _cssRepository.GetCSSForDelete(id, ct),
                IncludeType.Update => await _cssRepository.GetContractorStatusStatementById(id, ct),
                IncludeType.Full => await _cssRepository.GetContractorStatusStatementByIdFullInclude(id, ct),
                IncludeType.Hallf => await _cssRepository.GetContractorStatusStatementHeaderById(id, ct),
                IncludeType.Less => await _cssRepository.GetContractorStatusStatementByIdIncludeLess(id, ct),
                IncludeType.Non => await _cssRepository.GetContractorStatusStatementByIdNoInclude(id, ct),
                _ => await _cssRepository.GetContractorStatusStatementById(id, ct),
            };
            if (entity is null)
                return Result.Failure<ContractorStatusStatement>(CSSErrors.NotFound);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorStatusStatement>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetFilteredContractorStatusStatementModel>>?>> GetFilteredContractorStatusStatementExecute(
        GetFilteredContractorStatusStatementRequest request, List<long>? ids, List<long>? contractorIds, CT ct)
    {
        try
        {
            var result = await _cssRepository.GetFilteredContractorStatusStatement(
                ids,
                request.ContractorId,
                contractorIds,
                request.CostCenterId,
                request.ProjectId,
                request.ProjectManagerId,
                request.ContractorContractHeaderId,
                request.CreatorId,
                request.ContractNumber,
                request.Code,
                request.ManagerAmount,
                request.ManagerDescription,
                request.Statuses,
                request.IsPayment,
                request.MultiPayment,
                request.StartDate,
                request.EndDate,
                request.IsFinalManagerConfirmed,
                request.IsPrimaryManagerConfirmed,
                request.IsManager,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetFilteredContractorStatusStatementModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetFilteredContractorStatusStatementModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetFilteredContractorStatusStatementModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetContractorStatusStatementByIdResponse?>> GetModeledContractorStatusStatementByIdExecute(
        long id, CT ct)
    {
        try
        {
            var result = await _cssRepository.GetModeledContractorStatusStatementById(id, ct);
            if (result is null)
                return Result.Failure<GetContractorStatusStatementByIdResponse>(CSSErrors.ContractorStatusStatementWithIdNotFound);

            var sumDaily = await _cssServiceDailyRepository.GetTotalPriceByCSSId(id, ct);
            result.ServiceAmount = sumDaily ?? 0;

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetContractorStatusStatementByIdResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<long>>?>> GetsContractorStatusStatementContractorIdsExecute(
        GetFilteredContractorStatusStatementRequest request, CT ct)
    {
        try
        {
            var result = await _cssRepository.GetsContractorStatusStatementContractorIds(
                request.CostCenterId,
                request.ProjectId,
                request.ProjectManagerId,
                request.ContractorContractHeaderId,
                request.ContractNumber,
                request.Code,
                request.Statuses,
                request.StartDate,
                request.EndDate,
                request.FilterData,
                ct);

            return result.Data.Any() ?
                new DataResult<List<long>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<long>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<long>>>(SharedErrors.UnknownError);
        }
    }
    public async Task<Result<DataResult<List<GetModeledContractorStatusStatementByIdDetail>>?>> GetsContractorStatusStatementDetailExecute(
        long id, CT ct)
    {
        try
        {
            var result = await _cssDetailRepository.GetsContractorStatusStatementDetail(
                id, 0, 0, ct);

            return result.Data.Any() ?
                new DataResult<List<GetModeledContractorStatusStatementByIdDetail>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetModeledContractorStatusStatementByIdDetail>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetModeledContractorStatusStatementByIdDetail>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<ContractorStatusStatementDiscount>>?>> GetsContractorStatusStatementDiscountByIdExecute(
        GetsContractorStatusStatementDiscountByIdRequest request, CT ct)
    {
        try
        {
            var result = await _cssDiscountRepository.GetsContractorStatusStatementDiscountById(
                request.ContractorStatusStatementId,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<ContractorStatusStatementDiscount>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ContractorStatusStatementDiscount>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ContractorStatusStatementDiscount>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetsContractorStatusStatementHistoryModel>>?>> GetsContractorStatusStatementHistoryExecute(
        GetsContractorStatusStatementHistoryRequest request, CT ct)
    {
        try
        {
            var result = await _cssHistoryRepository.GetsContractorStatusStatementHistory(
                request.Id, request.PageIndex, request.PageSize, ct);

            var response = new DataResult<List<GetsContractorStatusStatementHistoryModel>>()
            {
                Data = result.Data ?? new List<GetsContractorStatusStatementHistoryModel>(0),
                RowCount = result.RowCount
            };

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsContractorStatusStatementHistoryModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetsDraftableContractorStatusStatementModel>>?>> GetsDraftableContractorStatusStatementExecute(
        long contractorId, long projectId, int pageIndex, int pageSize, CT ct)
    {
        try
        {
            var result = await _cssRepository.GetsDraftableContractorStatusStatement(
                contractorId, projectId, pageIndex, pageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsDraftableContractorStatusStatementModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsDraftableContractorStatusStatementModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsDraftableContractorStatusStatementModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<PaidContractorStatusStatementModel>>?>> GetsPaidContractorStatusStatementExecute(
        long contractorId, long projectId, CT ct)
    {
        try
        {
            var result = await _cssRepository.GetsPaidContractorStatusStatement(
                contractorId, projectId, 0, 0, ct);

            return result.Data.Any() ?
                new DataResult<List<PaidContractorStatusStatementModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<PaidContractorStatusStatementModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<PaidContractorStatusStatementModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<List<RequestGoodsSupplyProduct>?>> GetsRequestGoodsSupplyProductByContractorIdExecute(
        DateTime startDate,
        DateTime endDate,
        long contractorId,
        long projectId, CT ct)
    {
        try
        {
            var result = await _supplyProductRepository.GetsRequestGoodsSupplyProductByContractorId(
                startDate, endDate, GoodsSupplyType.Contractor, contractorId, projectId, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<RequestGoodsSupplyProduct>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<decimal?>> PaymentConfirmationCSSAmountsExecute(long contractorId, long projectId, CT ct)
    {
        try
        {
            var result = await _cssRepository.PaymentConfirmationCSSAmounts(contractorId, projectId, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<decimal?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetsIntegratedCSSModel>>?>> GetsIntegratedCSSExecute(
        GetsIntegratedCSSRequest request, List<long>? contractorIds, CT ct)
    {
        try
        {
            var result = await _cssRepository.GetsIntegratedCSS(
                request.ContractorId,
                contractorIds,
                request.CostCenterId,
                request.ProjectId,
                request.ContractorContractHeaderId,
                request.Statuses,
                request.IsPrimaryManager,
                request.IsFinalManager,
                request.FilterData,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsIntegratedCSSModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsIntegratedCSSModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsIntegratedCSSModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<List<GetCSSDailyServiceUrlsModel>?>> GetCSSDailyServiceUrlsExecute(
        GetCSSDailyServiceUrlsRequest request, CT ct)
    {
        try
        {
            var result = await _cssServiceDailyRepository.GetCSSDailyServiceUrls(request.Id, ct);

            return result.Any() ?
                result : Result.Failure<List<GetCSSDailyServiceUrlsModel>>(SharedErrors.UnknownError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetCSSDailyServiceUrlsModel>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetCSSPaymentsModel>>?>> GetCSSPaymentsExecute(
        GetCSSPaymentsRequest request, CT ct)
    {
        try
        {
            var result = await _cssPaymentRepository.GetCSSPayments(
                request.Id, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetCSSPaymentsModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetCSSPaymentsModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetCSSPaymentsModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetIntegratedCSSByProjectIdModel>>?>> GetIntegratedCSSByProjectIdExecute(
         GetIntegratedCSSByProjectIdRequest request, List<long>? contractorIds, CT ct)
    {
        try
        {
            var result = await _cssRepository.GetsIntegratedCSSByProjectId(
                request.ContractorId,
                contractorIds,
                request.CostCenterId,
                request.ProjectId,
                request.ContractorContractHeaderId,
                request.Statuses,
                request.IsPrimaryManager,
                request.IsFinalManager,
                request.FilterData,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetIntegratedCSSByProjectIdModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                }
                : Result.Failure<DataResult<List<GetIntegratedCSSByProjectIdModel>>>(SharedErrors.ItemNotFound);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetIntegratedCSSByProjectIdModel>>>(SharedErrors.UnknownError)!;
        }
    }

}
