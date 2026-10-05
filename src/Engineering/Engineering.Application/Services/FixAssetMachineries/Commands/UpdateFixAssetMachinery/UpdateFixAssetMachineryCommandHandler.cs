using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.UpdateFixAssetMachinery;

public class UpdateFixAssetMachineryCommandHandler : ICommandHandler<UpdateFixAssetMachineryCommand, FixAssetMachinery>
{
    private readonly ILogger<UpdateFixAssetMachineryCommand> _logger;
    private readonly IFixAssetMachineryRepository _repository;

    public UpdateFixAssetMachineryCommandHandler(ILogger<UpdateFixAssetMachineryCommand> logger, IFixAssetMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetMachinery?>> Handle(UpdateFixAssetMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, null, ct);
            if (entity is null)
                return Result.Failure<FixAssetMachinery>(FixAssetMachineryErrors.FixAssetMachineryNotFoundWithId);

            entity.SetMachinerySpecification(request.MachinerySpecification);
            entity.SetNumberPlates(request.NumberPlates);
            entity.SetStartDate(request.StartDate);
            entity.SetEndDate(request.EndDate);
            entity.SetDescription(request.Description);
            entity.SetDriverId(request.DriverId);
            entity.SetDriverName(request.DriverName);
            entity.SetFixAssetMachineryType(request.FixAssetMachineryType);
            entity.SetCompanyId(request.CompanyId);
            entity.SetMachinery(request.Machinery);
            entity.SetMachineryPrice(request.MachineryPrice);
            entity.SetContractorId(request.ContractorId);
            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive == true)
                    entity.SetActive();
                else
                    entity.SetInActive();
            }

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<FixAssetMachinery>(SharedErrors.UnknownError);
        }
    }
}