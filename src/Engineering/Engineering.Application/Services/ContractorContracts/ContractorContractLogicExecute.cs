using Engineering.Application.Services.BillOfLadings.Contracts.GetCCHVersionsById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCCByHeaderId;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetConfirmedCCDailyServices;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorPriceHistory;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice;
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts;

public partial class ContractorContractLogic
{

    public async Task<Result<DataResult<List<GetsFilteredContractorContractHeaderModel>>?>> GetContractorContractHeaderByFilterExecute(
        GetsFilteredContractorContractHeaderRequest request, long? companyId, List<long>? ids, CT ct)
    {
        try
        {
            var contractType = (ContractorContractType?)request.ContractorContractTypeId;

            var result = await _repository.GetContractorContractHeaderByFilter(
                ids,
                request.ContractorId,
                request.CostCenterId,
                request.ProjectIds,
                request.ProjectManagerId,
                request.ProjectOperationIds,
                request.FromDate,
                request.ToDate,
                request.Statuses,
                request.RemoveStatuses,
                contractType,
                request.FilterData,
                companyId,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsFilteredContractorContractHeaderModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsFilteredContractorContractHeaderModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsFilteredContractorContractHeaderModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetContractorPriceHistoryModel>>?>> GetContractorPriceHistoryExecute(
        GetContractorPriceHistoryRequest request, CT ct)
    {
        try
        {
            var result = await _contractorContractPriceHistoryRepo.GetContractorPriceHistory(
                request.Id,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetContractorPriceHistoryModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetContractorPriceHistoryModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetContractorPriceHistoryModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetSuggestedServicePriceResponse?>> GetSuggestedServicePriceExecute(
        List<long>? ids, GetSuggestedServicePriceRequest request, CT ct)
    {
        try
        {
            var result = await _contractorContractPriceRepo.GetSuggestedServicePrice(
                ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.ContractorIds,
                request.ProjectOperationIds,
                request.ServiceInfoIds,
                request.FilterData,
                request.StartDate,
                request.EndDate,
                request.MaxPrice,
                request.MinPrice,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetSuggestedServicePriceResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<List<GetContractorContractHeaderByIdResponse>?>> GetCCHVersionByCCHIdExcecute(
         GetCCHVersionByCCHIdRequest request, CT ct)
    {
        try
        {
            var data = new List<GetContractorContractHeaderByIdResponse>();
            var result = await _contractorContractHeaderVersionRepo.GetCCHVersionByCCHId(request.Id, ct);
            if (result is null)
                return Result.Failure<List<GetContractorContractHeaderByIdResponse>?>(ContractorContractErrors.VNotFound);

            foreach (var item in result!)
            {
                var version = JsonConvert.DeserializeObject<GetContractorContractHeaderByIdResponse>(item.Content);
                version!.VersionId = item.Id;
                version!.Version = item.Version;
                version.HaveAVersion = true;
                data.Add(version!);
            }

            return data;
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetContractorContractHeaderByIdResponse>?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<List<GetDraftedFixCCsModel>?>> GetDraftedFixCCsExecute(
        GetDraftedFixCCsRequest request, long companyId, CT ct)
    {
        if (request is null)
            return Result.Failure<List<GetDraftedFixCCsModel>>(
                GlobalErrors.ValueIsNull);


        var validation = await request.IsValidAsync<
            GetDraftedFixCCsValidator,
            GetDraftedFixCCsRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<List<GetDraftedFixCCsModel>>(
                validation.Error!);

        try
        {
            var result = await _cCRepository.GetDraftedFixCCs(
                request.ProjectId,
                request.ContractorId,
                request.StartDate,
                request.EndDate,
                companyId,
                ct);

            if (result is not null && result.HasAny())
            {
                var dailies = await _dailyServiceRepository.GetDraftedFixDailies(
                    result.SelectMany(x => x.PODContractorServiceIds).ToList(), ct);

                foreach (var item in result)
                    item.DailyServices = dailies.Where(x => item.PODContractorServiceIds
                        .Contains(x.ProjectOperationDetailContractorServiceId)).ToList();
            }

            return result is not null ? result :
                Result.Failure<List<GetDraftedFixCCsModel>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetDraftedFixCCsModel>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetConfirmedCCDailyServicesModel>>?>> GetConfirmedCCDailyServicesExecute(
        GetConfirmedCCDailyServicesRequest request, CT ct)
    {
        try
        {
            var result = await _cCDetailServiceRepository.GetConfirmedCCDailyServices(
                request.Ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.ContractorIds,
                request.ServiceInfoIds,
                request.MeasurUnitIds,
                request.StartDate,
                request.EndDate,
                request.FilterData,
                request.FilterServiceInfo,
                request.OrderBy,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetConfirmedCCDailyServicesModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetConfirmedCCDailyServicesModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetConfirmedCCDailyServicesModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<List<GetDraftedServiceCCsModel>?>> GetDraftedServiceCCsExecute(
        GetDraftedServiceCCsRequest request, long companyId, CT ct)
    {
        if (request is null)
            return Result.Failure<List<GetDraftedServiceCCsModel>>(
                GlobalErrors.ValueIsNull);

        var validation = await request.IsValidAsync<
            GetDraftedServiceCCsValidator,
            GetDraftedServiceCCsRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<List<GetDraftedServiceCCsModel>>(
                validation.Error!);

        try
        {
            var result = await _cCRepository.GetDraftedServiceCCs(
                request.ProjectId,
                request.ContractorId,
                request.StartDate,
                request.EndDate,
                companyId,
                ct);

            if (result is not null && result.HasAny())
            {
                var dailies = await _dailyServiceRepository.GetDraftedServiceDailies(
                    result.SelectMany(x => x.PODContractorServiceIds).ToList(), request.StartDate, request.EndDate, ct);

                foreach (var item in result)
                {
                    var key = item.PODContractorServiceIds.ToList();
                    var contractorDailies = dailies.Where(x => key.Contains(x.ProjectOperationDetailContractorServiceId)).ToList();
                    foreach (var dailie in contractorDailies)
                    {
                        decimal unitPrice = 0;
                        var prices = item.Prices!.Where(x => x.ContractorContractDetailId == dailie.ContractorContractDetailId).ToList();
                        if (prices.Any())
                        {
                            var date = dailie.WorkDateMiladi!.Value.Date;
                            var rangePrice = prices.FirstOrDefault(d => d.StartDate.Date <= date && d.EndDate.Date >= date);
                            if (rangePrice is not null)
                                unitPrice = rangePrice.Price;
                            else
                            {
                                var activePrice = prices.FirstOrDefault(d => d.IsActive);
                                if (activePrice is not null)
                                    unitPrice = activePrice.Price;
                                else
                                {
                                    var price = prices.FirstOrDefault();
                                    if (price is not null)
                                        unitPrice = price!.Price;
                                    else
                                        return Result.Failure<List<GetDraftedServiceCCsModel>?>(ContractorContractErrors.ServicePriceNotFound);
                                }
                            }
                        }
                        dailie.UnitPrice = unitPrice;
                        dailie.TotalAmount = unitPrice * (dailie.Volume ?? 0);
                    }

                    item.DailyServices = contractorDailies;
                }
            }

            return result is not null ? result :
                Result.Failure<List<GetDraftedServiceCCsModel>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetDraftedServiceCCsModel>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetCCHByIdResponse?>> GetCCHByIdQuery(
        long id, long companyId, CT ct)
    {
        try
        {
            var result = await _repository.GetCCHById(id, companyId, ct);
            return result is null ?
                Result.Failure<GetCCHByIdResponse>(ContractorContractErrors.ContractorContractWithIdNotFound) :
                result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCCHByIdResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetCCByHeaderIdResponse?>> GetCCByHeaderIdQuery(
        GetCCByHeaderIdRequest request, long companyId, CT ct)
    {
        try
        {
            var response = await _cCRepository.GetCCByHeaderId(request.HeaderId, request.Type, companyId, ct);

            return response is null ?
                Result.Failure<GetCCByHeaderIdResponse>(ContractorContractErrors.ContractorContractNotFound) :
                new GetCCByHeaderIdResponse(response, response.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCCByHeaderIdResponse>(SharedErrors.UnknownError);
        }
    }
}
