using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachinery;

public class UpdateRequestMachineryCommandHandler : ICommandHandler<UpdateRequestMachineryCommand, RequestMachinery>
{
    private readonly ILogger<UpdateRequestMachineryCommandHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public UpdateRequestMachineryCommandHandler(ILogger<UpdateRequestMachineryCommandHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachinery?>> Handle(UpdateRequestMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(request.RequestMachineryId, ct);
            if (entity is null)
                return Result.Failure<RequestMachinery>(RequestMachineryErrors.RequestMachineryNotFound);

            entity.SetProject(request.Project);
            entity.SetDescription(request.Description);
            entity.SetFromDate(request.FromDate);
            entity.SetToDate(request.ToDate);
            entity.SetMachinery(request.Machinery);
            entity.SetRequestCount(request.RequestCount);
            entity.SetTimeRequired(request.TimeRequired);
            entity.SetUnit(request.Unit);
            entity.SetCompanyId(request.CompanyId);

            entity.AddHistory(null);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachinery>(SharedErrors.UnknownError);
        }
    }
}
