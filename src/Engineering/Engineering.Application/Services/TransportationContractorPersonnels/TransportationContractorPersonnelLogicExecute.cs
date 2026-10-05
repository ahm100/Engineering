using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.ChangeTransportationContractorPersonnelState;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.CreateTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.DeleteTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsActiveTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsFilteredTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetTransportationContractorPersonnelById;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.UpdateTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractors.Contracts.CreateTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.UpdateTransportationContractor;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.CreateThirdPartyPersonnel;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.RemoveThirdParty;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Commands.UpdateThirdParty;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Application.Services.TransportationContractorPersonnels;

public partial class TransportationContractorPersonnelLogic : ITransportationContractorPersonnelLogic
{
    public async Task<Result<TransportationContractorPersonnel?>> CreateTransportationContractorPersonnelExecute(
        CreateTransportationContractorPersonnelRequest request, CT ct)
    {
        try
        {
            var companyId = CompanyValidator.GetCompanyId(_userInfoService);
            if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
                return Result.Failure<TransportationContractorPersonnel>(GlobalErrors.InvalidCompany);

            var contractor = await _contractorRepository.FindById(request.TransportationContractorId, ct);
            if (contractor is null)
                return Result.Failure<TransportationContractorPersonnel>(TransportationContractorErrors.TransportationContractorNotFound);

            var command = new CreateContractorPersonnelModel(request.FirstName, request.LastName,
                request.PhoneNumber, request.IdentityNo, request.Description, request.CertificateNumber, request.LagacyId,
                request.PersonnelAddress != null ?
                new CreateContractorPersonnelAddressModel(request.PersonnelAddress.CityId, request.PersonnelAddress.Address, request.PersonnelAddress.Title) : null);

            var createPersonnel = await _mediator.Send(new CreateThirdPartyPersonnelCommand(command, companyId!.Value), ct);
            if (createPersonnel.IsFailure || createPersonnel.Value is null)
                return Result.Failure<TransportationContractorPersonnel>(createPersonnel.Error!);
            var personnelId = createPersonnel.Value.Id;

            var personnel = new TransportationContractorPersonnel(personnelId!.Value, request.CertificateNumber, request.LagacyId, contractor);
            var result = await _repository.Create(personnel, ct);

            if (request.PersonnelMachines != null && request.PersonnelMachines.Count > 0)
            {
                var personnelMachines = await _contractorMachineRepository.GetByIds(request.PersonnelMachines, ct);
                if (personnelMachines is null && personnelMachines?.Distinct().Count() != request.PersonnelMachines.Distinct().Count())
                    return Result.Failure<TransportationContractorPersonnel>(TransportationContractorErrors.TransportationContractorMachineNotFound);

                personnel.SetPersonnel(personnelMachines);
            }

            return personnel;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationContractorPersonnel?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<TransportationContractorPersonnel?>> DeleteTransportationContractorPersonnelExecute(
        DeleteTransportationContractorPersonnelRequest request, CT ct)
    {
        try
        {
            var entities = await _repository.GetByIds(request.Ids, ct);
            if (entities is null && entities?.Count != request.Ids.Count)
                return Result.Failure<TransportationContractorPersonnel>
                    (TransportationContractorErrors.TransportationContractorPersonnelNotFound);

            foreach (var entity in entities)
            {
                entity.SoftDelete();
                await _repository.Update(entity);

                var deletePersonnel = await _mediator.Send(new RemoveThirdPartyCommand(entity.ThirdPartyId), ct);
                if (deletePersonnel.IsFailure || deletePersonnel.Value is null)
                    return Result.Failure<TransportationContractorPersonnel>(deletePersonnel.Error!);
            }

            return entities.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationContractorPersonnel>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<TransportationContractorPersonnel?>> UpdateTransportationContractorPersonnelExecute(
        UpdateTransportationContractorPersonnelRequest request, CT ct)
    {
        try
        {
            var entity = await _repository.GetTransportationContractorPersonnel(request.Id, ct);
            if (entity is null) return Result.Failure<TransportationContractorPersonnel>
                    (TransportationContractorErrors.TransportationContractorPersonnelNotFound);

            var contractor = await _contractorRepository.FindById(request.TransportationContractorId, ct);
            if (contractor is null)
                return Result.Failure<TransportationContractorPersonnel>(TransportationContractorErrors.TransportationContractorNotFound);

            var command = new UpdateContractorPersonnelModel(request.Id, request.ThirdPartyId, request.FirstName, request.LastName,
                request.PhoneNumber, request.IdentityNo, request.Description, request.CertificateNumber, request.IsActive, false, request.LagacyId,
                request.PersonnelAddress != null ? new UpdateContractorPersonnelAddressModel(request.PersonnelAddress.Id, request.PersonnelAddress.CityId,
                request.PersonnelAddress.Address, request.PersonnelAddress.Title, request.PersonnelAddress.IsDeleted) : null);

            var updatePersonnel = await _mediator.Send(new UpdateThirdPartyPersonnelCommand(command), ct);
            if (updatePersonnel.IsFailure || updatePersonnel.Value is null)
                return Result.Failure<TransportationContractorPersonnel>(updatePersonnel.Error!);
            var personnelId = updatePersonnel.Value.Id;

            entity.Update(personnelId!.Value, request.CertificateNumber, request.LagacyId, request.IsActive, contractor);

            List<TransportationContractorMachine>? personnelMachines = [];
            if (request.PersonnelMachines != null && request.PersonnelMachines.Count > 0)
            {
                personnelMachines = await _contractorMachineRepository.GetByIds(request.PersonnelMachines, ct);
                if (personnelMachines is null && personnelMachines?.Distinct().Count() != request.PersonnelMachines.Distinct().Count())
                    return Result.Failure<TransportationContractorPersonnel>(TransportationContractorErrors.TransportationContractorMachineNotFound);
            }

            entity.SetPersonnel(personnelMachines);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<TransportationContractorPersonnel>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<TransportationContractorPersonnel?>> ChangeTransportationContractorPersonnelStateExecute(
        ChangeTransportationContractorPersonnelStateRequest request, CT ct)
    {
        try
        {
            var entities = await _repository.GetByIds(request.Ids, ct);
            if (entities.Count < request.Ids.Count)
                return Result.Failure<TransportationContractorPersonnel>(TransportationContractorErrors.TransportationContractorPersonnelNotFound);

            foreach (var entity in entities)
            {
                if (request.IsActive)
                {
                    if (entity.IsActive) return Result.Failure<TransportationContractorPersonnel>(GlobalErrors.IsActive);
                    entity.SetActive();
                }
                else
                {
                    if (!entity.IsActive) return Result.Failure<TransportationContractorPersonnel>(GlobalErrors.InActive);
                    entity.SetDeactivate();
                }
                await _repository.Update(entity);
            }

            return entities.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TransportationContractorPersonnel>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetTransportationContractorPersonnelByIdResponse?>> GetTransportationContractorPersonnelByIdExecute(
        GetTransportationContractorPersonnelByIdRequest request, CT ct)
    {
        try
        {
            var item = await _repository.GetTransportationContractorPersonnelById(request.Id, ct);
            return item ?? Result.Failure<GetTransportationContractorPersonnelByIdResponse?>
                (TransportationContractorErrors.TransportationContractorPersonnelNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetTransportationContractorPersonnelByIdResponse?>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetsFilteredTransportationContractorPersonnelResponseModel>>?>> GetsFilteredTransportationContractorPersonnelExecute(
        GetsFilteredTransportationContractorPersonnelRequest request, CT ct)
    {
        try
        {
            var items = await _repository.GetFilteredTransportationContractorPersonnels(
                request.Ids,
                request.ContractorIds,
                request.PersonnelIds,
                request.MachineTypeIds,
                request.FilterData,
                request.IsActive,
                request.PageIndex,
                request.PageSize,
                ct);

            var response = items.Data.Any() ?
                new DataResult<List<GetsFilteredTransportationContractorPersonnelResponseModel>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<GetsFilteredTransportationContractorPersonnelResponseModel>>>
                (TransportationContractorErrors.FilteredTransportationContractorPersonnelNotFound);

            return response!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsFilteredTransportationContractorPersonnelResponseModel>>>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DataResult<List<GetsActiveTransportationContractorPersonnelResponseModel>>?>> GetsActiveTransportationContractorPersonnelExecute(
        GetsActiveTransportationContractorPersonnelRequest request, CT ct)
    {
        try
        {
            var items = await _repository.GetAllActiveTransportationContractorPersonnels(
                request.Ids,
                request.ContractorIds,
                request.PersonnelIds,
                request.MachineIds,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            var response = items.Data.Any() ?
                new DataResult<List<GetsActiveTransportationContractorPersonnelResponseModel>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<GetsActiveTransportationContractorPersonnelResponseModel>>>
                (TransportationContractorErrors.FilteredTransportationContractorPersonnelNotFound);

            return response!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsActiveTransportationContractorPersonnelResponseModel>>>(SharedErrors.UnknownError);
        }
    }
}
