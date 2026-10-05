using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Commands.CreateOperationLocation;

public class CreateOperationLocationCommandHandler : ICommandHandler<CreateOperationLocationCommand, OperationLocation?>
{
    private readonly ILogger<CreateOperationLocationCommand> _logger;
    private readonly IOperationLocationRepository _repository;

    public CreateOperationLocationCommandHandler(
        ILogger<CreateOperationLocationCommand> logger,
        IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationLocation?>> Handle(CreateOperationLocationCommand request, CT ct)
    {
        try
        {
            var entity = new OperationLocation(
                request.CostCenter,
                request.Project,
                request.Parent,
                request.PrivateName,
                request.PrivateCode,
                request.PublicName,
                request.PublicCode,
                "",
                "",
                request.Priority,
                request.IsActive,
                request.CompanyId);

            if (entity.Parent is null)
                entity.SetCoding($"{request.CostCenter.CostCenterCode},{request.PrivateCode}");
            if (entity.Parent is not null)
                entity.SetCoding($"{request.Parent!.Coding},{request.PrivateCode}");

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationLocation?>(SharedErrors.UnknownError);
        }
    }
}