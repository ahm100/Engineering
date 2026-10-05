using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractById;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHeaderById;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractOperationHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEOProductHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEOServiceHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEmployers;

namespace Engineering.Application.Services.EmployerContracts;

public partial class EContractHeaderLogic
{
    public async Task<Result<bool>> VerifyCodeHandler(
        long? id, string code, long companyId, CT ct)
    {
        try
        {
            var result = await _headRepo.VerifyCode(id, code, companyId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<bool>> VerifyEContractCodeHandler(
        long? id, string code, long companyId, CT ct)
    {
        try
        {
            var result = await _contractRepo.VerifyCode(id, code, companyId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetEContractHeaderByIdResponse?>> GetEContractHeaderByIdHandler(
        long id, CT ct)
    {
        try
        {
            var result = await _headRepo.GetEContractHeaderById(id, ct);
            if (result is null)
                return Result.Failure<GetEContractHeaderByIdResponse>(EContractErrors.NotFound);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetEContractHeaderByIdResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetEContractByIdResponse?>> GetEContractByIdHandler(
        long id, CT ct)
    {
        try
        {
            var result = await _contractRepo.GetEContractById(id, ct);
            if (result is null)
                return Result.Failure<GetEContractByIdResponse>(EContractErrors.NotFound);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetEContractByIdResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetFltrEContractHeadsModel>>?>> GetFltrEContractHeadsHandler(
        GetFltrEContractHeadsRequest request, CT ct)
    {
        try
        {
            var result = await _headRepo.GetFltrEContractHeads(
                request.CostCenterIds,
                request.ProjectIds,
                request.EmployerIds,
                request.Types,
                request.StartDate,
                request.EndDate,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetFltrEContractHeadsModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetFltrEContractHeadsModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetFltrEContractHeadsModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetFltrEContractsModel>>?>> GetFltrEContractsHandler(
        GetFltrEContractsRequest request, CT ct)
    {
        try
        {
            var result = await _contractRepo.GetFltrEContracts(
                request.HeadId,
                request.Ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.EmployerIds,
                request.Types,
                request.Statuses,
                request.RemoveStatuses,
                request.IsPrimaryManager,
                request.IsFinalManager,
                request.StartDate,
                request.EndDate,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetFltrEContractsModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetFltrEContractsModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetFltrEContractsModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<List<long>?>> GetFltrEmployersHandler(
        GetFltrEmployersRequest request, CT ct)
    {
        try
        {
            var result = await _contractRepo.GetFltrEmployers(
                request.CostCenterIds,
                request.ProjectIds,
                ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<long>?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<string>> CodeCreateHandler(
        long contractHeadId, CT ct)
    {
        try
        {
            var result = await _contractRepo.CodeCreator(contractHeadId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<string>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<List<GetEContractHistoryModel>>> GetEContractHistoryHandler(
        GetEContractHistoryRequest request, CT ct)
    {
        try
        {
            var result = await _contractHistoryRepo.GetEContractHistory(request.Id,
                request.PageIndex,
                request.PageSize,
                ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetEContractHistoryModel>>(SharedErrors.UnknownError)!;
        }
    }

    public async Task<Result<List<GetEContractOperationHistoryModel>>> GetEContractOperationHistoryHandler(
        GetEContractOperationHistoryRequest request, CT ct)
    {
        try
        {
            var result = await _contractOperationHistoryRepo.GetEContractOperationHistory(request.Id,
                request.PageIndex,
                request.PageSize,
                ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetEContractOperationHistoryModel>>(SharedErrors.UnknownError)!;
        }
    }

    public async Task<Result<List<GetEOProductHistoryModel>>> GetEOProductHistoryHandler(
        GetEOProductHistoryRequest request, CT ct)
    {
        try
        {
            var result = await _employerOperationProductHistoryRepo.GetEOProductHistory(
                request.Id,
                request.PageIndex,
                request.PageSize,
                ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetEOProductHistoryModel>>(SharedErrors.UnknownError)!;
        }
    }

    public async Task<Result<List<GetEOServiceHistoryModel>>> GetEOServiceHistoryHandler(
        GetEOServiceHistoryRequest request, CT ct)
    {
        try
        {
            var result = await _employerOperationServiceHistoryRepo.GetEOServiceHistory(
                request.Id,
                request.PageIndex,
                request.PageSize,
                ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetEOServiceHistoryModel>>(SharedErrors.UnknownError)!;
        }
    }
}