using Engineering.Application.Services.TransportationContractorMachines.Contracts.ChangeTransportationContractorMachineState;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.CreateTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.DeleteTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsActiveTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsFilteredTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetTransportationContractorMachineById;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.UpdateTransportationContractorMachine;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Application.Services.TransportationContractorMachines;

public partial class TransportationContractorMachineLogic : ITransportationContractorMachineLogic
{
    public async Task<Result<bool?>> CreateTransportationContractorMachineExecute(
        CreateTransportationContractorMachineRequest request, CT ct)
    {
        try
        {
            var validateRequest = await ValidateRequest(request, ct);
            if (validateRequest.IsFailure) return Result.Failure<bool?>(validateRequest.Error!);

            if (request.ContractorMachines.Count > 0)
                foreach (var req in request.ContractorMachines)
                {
                    var isDuplicate = await _repository.IsDuplicateMachine(null, req.NumberPlate, req.Vin, ct);
                    if (isDuplicate) return Result.Failure<bool?>(MachineTypeErrors.MachineTypeWithNpOrVINDuplicate);

                    var machine = validateRequest.Value.machines!.FirstOrDefault(x => x.Id == req.MachineTypeId);
                    var contractorMachine = new TransportationContractorMachine(req.NumberPlate, req.Vin, req.Color, machine!, validateRequest.Value.contractor!);
                    var result = await _repository.Create(contractorMachine, ct);

                    if (req.ContractorPersonnels != null && req.ContractorPersonnels.Count > 0)
                    {
                        var contractorPersonnels = await _contractorPersonnelRepository.GetByIds(req.ContractorPersonnels, ct);
                        if (contractorPersonnels is null && contractorPersonnels?.Distinct().Count() != req.ContractorPersonnels.Distinct().Count())
                            return Result.Failure<bool?>(TransportationContractorErrors.TransportationContractorPersonnelNotFound);

                        contractorMachine.SetPersonnel(contractorPersonnels);
                    }
                }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<TransportationContractorMachine?>> DeleteTransportationContractorMachineExecute(
        DeleteTransportationContractorMachineRequest request, CT ct)
    {
        try
        {
            var entities = await _repository.GetByIds(request.Ids, ct);
            if (entities is null && entities?.Count != request.Ids.Count)
                return Result.Failure<TransportationContractorMachine>
                    (TransportationContractorErrors.TransportationContractorMachineNotFound);

            foreach (var entity in entities)
            {
                entity.SoftDelete();
                await _repository.Update(entity);
            }

            return entities.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationContractorMachine>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<TransportationContractorMachine?>> UpdateTransportationContractorMachineExecute(
        UpdateTransportationContractorMachineRequest request, CT ct)
    {
        try
        {
            var entity = await _repository.GetTransportationContractorMachine(request.Id, ct);
            if (entity is null) return Result.Failure<TransportationContractorMachine>
                    (TransportationContractorErrors.TransportationContractorMachineNotFound);

            var machine = await _machineTypeRepository.FindById(request.MachineTypeId, ct);
            if (machine is null)
                return Result.Failure<TransportationContractorMachine>(MachineTypeErrors.MachineTypeWithIdNotFound);

            var isDuplicate = await _repository.IsDuplicateMachine(request.Id, request.NumberPlate, request.Vin, ct);
            if (isDuplicate) return Result.Failure<TransportationContractorMachine>(MachineTypeErrors.MachineTypeWithNpOrVINDuplicate);

            var contractor = await _contractorRepository.FindById(request.ContractorId, ct);
            if (contractor is null)
                return Result.Failure<TransportationContractorMachine>(TransportationContractorErrors.TransportationContractorNotFound);

            entity.Update(request.NumberPlate, request.Vin, request.Color, request.IsActive, machine, contractor);

            List<TransportationContractorPersonnel>? contractorPersonnels = [];
            if (request.ContractorPersonnels != null && request.ContractorPersonnels.Count > 0)
            {
                contractorPersonnels = await _contractorPersonnelRepository.GetByIds(request.ContractorPersonnels, ct);
                if (contractorPersonnels is null && contractorPersonnels?.Distinct().Count() != request.ContractorPersonnels.Distinct().Count())
                    return Result.Failure<TransportationContractorMachine>(TransportationContractorErrors.TransportationContractorNotFound);
            }

            entity.SetPersonnel(contractorPersonnels);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<TransportationContractorMachine>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<TransportationContractorMachine?>> ChangeTransportationContractorMachineStateExecute(
        ChangeTransportationContractorMachineStateRequest request, CT ct)
    {
        try
        {
            var entities = await _repository.GetByIds(request.Ids, ct);
            if (entities.Count < request.Ids.Count)
                return Result.Failure<TransportationContractorMachine>(TransportationContractorErrors.TransportationContractorMachineNotFound);

            foreach (var entity in entities)
            {
                if (request.IsActive)
                {
                    if (entity.IsActive) return Result.Failure<TransportationContractorMachine>(GlobalErrors.IsActive);
                    entity.SetActive();
                }
                else
                {
                    if (!entity.IsActive) return Result.Failure<TransportationContractorMachine>(GlobalErrors.InActive);
                    entity.SetDeactivate();
                }
                await _repository.Update(entity);
            }

            return entities.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationContractorMachine>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetTransportationContractorMachineByIdResponse?>> GetTransportationContractorMachineByIdExecute(
        GetTransportationContractorMachineByIdRequest request, CT ct)
    {
        try
        {
            var item = await _repository.GetTransportationContractorMachineById(request.Id, ct);
            return item ?? Result.Failure<GetTransportationContractorMachineByIdResponse?>
                (TransportationContractorErrors.TransportationContractorMachineNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetTransportationContractorMachineByIdResponse?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetsFilteredTransportationContractorMachineResponseModel>>?>> GetsFilteredTransportationContractorMachineExecute(
        GetsFilteredTransportationContractorMachineRequest request, CT ct)
    {
        try
        {
            var items = await _repository.GetFilteredTransportationContractorMachines(
                request.Ids,
                request.ContractorIds,
                request.MachineTypeIds,
                request.FilterData,
                request.IsActive,
                request.PageIndex,
                request.PageSize,
                ct);

            var response = items.Data.Any() ?
                new DataResult<List<GetsFilteredTransportationContractorMachineResponseModel>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<GetsFilteredTransportationContractorMachineResponseModel>>>
                (TransportationContractorErrors.FilteredTransportationContractorMachineNotFound);

            return response!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsFilteredTransportationContractorMachineResponseModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetsActiveTransportationContractorMachineResponseModel>>?>> GetsActiveTransportationContractorMachineExecute(
        GetsActiveTransportationContractorMachineRequest request, CT ct)
    {
        try
        {
            var items = await _repository.GetAllActiveTransportationContractorMachines(
                request.Ids,
                request.ContractorIds,
                request.MachineTypeIds,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            var response = items.Data.Any() ?
                new DataResult<List<GetsActiveTransportationContractorMachineResponseModel>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<GetsActiveTransportationContractorMachineResponseModel>>>
                (TransportationContractorErrors.FilteredTransportationContractorMachineNotFound);

            return response!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsActiveTransportationContractorMachineResponseModel>>>(SharedErrors.UnknownError);
        }
    }


    #region PrivateMethod

    private async Task<Result<(TransportationContractor? contractor, List<Domain.Entities.MachineTypes.MachineType>? machines)>> ValidateRequest(
        CreateTransportationContractorMachineRequest request, CT ct)
    {
        var duplicateNPs = request.ContractorMachines
            .GroupBy(m => m.NumberPlate)
            .Where(g => g.Count() > 1)
            .Select(g => g.ToList())
            .ToList();
        if (duplicateNPs.Any())
            return Result.Failure<(TransportationContractor?,
                List<Domain.Entities.MachineTypes.MachineType>?)>(MachineTypeErrors.MachineTypeWithNpOrVINDuplicate);

        var duplicateVINs = request.ContractorMachines
            .GroupBy(m => m.Vin)
            .Where(g => g.Count() > 1)
            .Select(g => g.ToList())
            .ToList();
        if (duplicateVINs.Any())
            return Result.Failure<(TransportationContractor?,
                List<Domain.Entities.MachineTypes.MachineType>?)>(MachineTypeErrors.MachineTypeWithNpOrVINDuplicate);

        var contractor = await _contractorRepository.FindById(request.TransportationContractorId, ct);
        if (contractor is null)
            return Result.Failure<(TransportationContractor?,
                List<Domain.Entities.MachineTypes.MachineType>?)>(TransportationContractorErrors.TransportationContractorNotFound);

        var machineIds = request.ContractorMachines.Listed(x => x.MachineTypeId);
        var machines = await _machineTypeRepository.GetsMachineTypeByIds(machineIds, ct);
        if (machines is null || machines.Count() != machineIds.Count())
            return Result.Failure<(TransportationContractor?,
                List<Domain.Entities.MachineTypes.MachineType>?)>(MachineTypeErrors.MachineTypeWithIdNotFound);

        return (contractor, machines);
    }

    #endregion
}
