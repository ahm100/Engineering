using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractors.Commands.UpdateRequestContractor;

public class UpdateRequestContractorCommandHandler : ICommandHandler<UpdateRequestContractorCommand, RequestContractor>
{
    private readonly ILogger<UpdateRequestContractorCommandHandler> _logger;
    private readonly IRequestContractorRepository _repository;

    public UpdateRequestContractorCommandHandler(ILogger<UpdateRequestContractorCommandHandler> logger, IRequestContractorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestContractor?>> Handle(UpdateRequestContractorCommand request, CT ct)
    {
        try
        {
            var entity = request.RequestContractor;

            entity.SetVolume(request.Volume);
            entity.SetDescription(request.Description);

            entity.AddHistory(null);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestContractor>(SharedErrors.UnknownError);
        }
    }
}
