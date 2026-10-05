using Engineering.Application.Services.BillOfLadings.Contracts.ChangeBillOfLadingState;
using Engineering.Application.Services.BillOfLadings.Contracts.GetBillOfLadingById;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsActiveBillOfLading;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelExporter;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsFilteredBillOfLading;
using Engineering.Application.Services.BillOfLadings.Contracts.UpdateBillOfLading;
using Engineering.Domain.Entities.BillOfLadings;

namespace Engineering.Application.Services.BillOfLadings;

public partial class BillOfLadingLogic : IBillOfLadingLogic
{

    public async Task<Result<BillOfLading?>> CreateBillOfLadingHandle(
        string name, string code, bool isActive, long companyId, CT ct)
    {
        try
        {
            var entity = new BillOfLading(name, code, isActive, companyId);
            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<BillOfLading?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<BillOfLading?>> UpdateBillOfLadingHandle(
        UpdateBillOfLadingRequest request, CT ct)
    {
        try
        {
            var entity = await _repository.GetBillOfLadingById(request.Id, ct);
            if (entity is null)
                return Result.Failure<BillOfLading>(BillOfLadingErrors.BillOfLadingWithIdNotFound);

            entity.SetName(request.BillOfLadingName);
            entity.SetCode(request.BillOfLadingCode);
            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive)
                    entity.SetActive();
                else
                    entity.SetDeactivate();
            }

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<BillOfLading>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<List<BillOfLading>?>> DeleteBillOfLadingsHandle(
        List<long> ids, CT ct)
    {
        try
        {
            var entities = await _repository.GetBillOfLadings(ids, ct);
            foreach (var entity in entities)
            {
                if (entity is null)
                    return Result.Failure<List<BillOfLading>>(BillOfLadingErrors.BillOfLadingWithIdNotFound);
                if (entity.TransportationRequests.Any())
                    return Result.Failure<List<BillOfLading>>(BillOfLadingErrors.CanNottDelete);

                entity.SoftDelete();
                await _repository.Update(entity);
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<BillOfLading>>(SharedErrors.UnknownError);
        }
    }
    public async Task<Result<string?>> BillOfLadingCodeCreatorHandle(
        long companyId, CT ct)
    {
        try
        {
            var result = await _repository.CodeCreator(companyId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<string?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<BillOfLading?>> ChangeBillOfLadingStateHandle(
         ChangeBillOfLadingStateRequest request, CT ct)
    {
        try
        {
            var entities = await _repository.GetBillOfLadings(request.Ids, ct);
            if (entities.Count < request.Ids.Count)
                return Result.Failure<BillOfLading>(BillOfLadingErrors.BillOfLadingChildNotFound);

            foreach (var entity in entities)
            {
                if (request.IsActive)
                {
                    if (entity.IsActive)
                        return Result.Failure<BillOfLading>(GlobalErrors.IsActive);
                    entity.SetActive();
                }
                else
                {
                    if (!entity.IsActive)
                        return Result.Failure<BillOfLading>(GlobalErrors.InActive);
                    if (entity.TransportationRequests.Any())
                        return Result.Failure<BillOfLading>(BillOfLadingErrors.CanNottInActive);

                    entity.SetDeactivate();
                }
                await _repository.Update(entity);
            }

            return entities.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<BillOfLading>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<BillOfLading?>> GetBillOfLadingByCodeHandle(
        string billOfLadingCode, long? companyId, CT ct)
    {
        try
        {
            var item = await _repository.GetBillOfLadingByCode(
                billOfLadingCode, companyId, ct);

            return item ?? Result.Failure<BillOfLading?>(BillOfLadingErrors.BillOfLadingWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<BillOfLading?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<BillOfLading?>> GetBillOfLadingByIdHandle(
        long id, CT ct)
    {
        try
        {
            var res = await _repository.GetBillOfLadingById(id, ct);
            return res ?? Result.Failure<BillOfLading?>(BillOfLadingErrors.BillOfLadingWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<BillOfLading?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetBillOfLadingByIdResponse?>> GetBillOfLadingByIdForResponseHandle(
    long id, CT ct)
    {
        try
        {
            var res = await _repository.GetBillOfLadingByIdForResponse(id, ct);
            return res ?? Result.Failure<GetBillOfLadingByIdResponse?>(BillOfLadingErrors.BillOfLadingWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetBillOfLadingByIdResponse?>(SharedErrors.UnknownError);
        }
    }


    public async Task<Result<BillOfLading?>> GetBillOfLadingByNameHandle(
        string billOfLadingName, long? companyId, CT ct)
    {
        try
        {
            var item = await _repository.GetBillOfLadingByName(billOfLadingName, companyId, ct);
            return item ?? Result.Failure<BillOfLading?>(BillOfLadingErrors.BillOfLadingWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<BillOfLading?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetsActiveBillOfLadingResponseModel>>?>> GetsActiveBillOfLadingHandle(
        GetsActiveBillOfLadingRequest request, long? companyId, CT ct)
    {
        try
        {
            var result = await _repository.GetsActiveBillOfLading(
                request.FilterData,
                request.BillOfLadingCode,
                request.BillOfLadingName,
                companyId,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any()
                ? new DataResult<List<GetsActiveBillOfLadingResponseModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                }
                : Result.Failure<DataResult<List<GetsActiveBillOfLadingResponseModel>>>(BillOfLadingErrors.BillOfLadingWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsActiveBillOfLadingResponseModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<List<BillOfLading>?>> GetBillOfLadingsHandle(
        List<long> ids, CT ct)
    {
        try
        {
            var result = await _repository.GetBillOfLadings(ids, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<BillOfLading>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<bool>> GetsBillOfLadingByNamesOrCodesHandle(
        List<string> names, List<string> codes, long? companyId, CT ct)
    {
        try
        {
            var item = await _repository.GetsBillOfLadingByNamesOrCodes(
                names, codes, companyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetsFilteredBillOfLadingResponseModel>>?>> GetsFilteredBillOfLadingHandle(
        GetsFilteredBillOfLadingRequest request, List<long>? ids, long? companyId, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredBillOfLading(
                ids, request.FilterData, request.BillOfLadingCode, request.BillOfLadingName,
                request.IsActive, request.OrderBy, companyId, request.PageIndex, request.PageSize, ct);

            return items.Data.Any()
                ? new DataResult<List<GetsFilteredBillOfLadingResponseModel>> { Data = items.Data, RowCount = items.RowCount }
                : Result.Failure<DataResult<List<GetsFilteredBillOfLadingResponseModel>>>(BillOfLadingErrors.FilteredBillOfLadingNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsFilteredBillOfLadingResponseModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetsBillOfLadingExcelExporterModel>>?>> GetsFilteredBillOfLadingForExcelHandle(
        GetsFilteredBillOfLadingRequest request, List<long>? ids, long? companyId, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredBillOfLadingForExcel(
                ids, request.FilterData, request.BillOfLadingCode, request.BillOfLadingName,
                request.IsActive, request.OrderBy, companyId, request.PageIndex, request.PageSize, ct);

            return items.Data.Any()
                ? new DataResult<List<GetsBillOfLadingExcelExporterModel>> { Data = items.Data, RowCount = items.RowCount }
                : Result.Failure<DataResult<List<GetsBillOfLadingExcelExporterModel>>>(BillOfLadingErrors.FilteredBillOfLadingNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsBillOfLadingExcelExporterModel>>>(SharedErrors.UnknownError);
        }
    }
}