using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractors.Commands.DeleteRequestContractor;

public class DeleteRequestContractorCommandHandler : ICommandHandler<DeleteRequestContractorCommand, RequestContractor>
{
    private readonly ILogger<DeleteRequestContractorCommandHandler> _logger;
    private readonly IRequestContractorRepository _repository;

    public DeleteRequestContractorCommandHandler(ILogger<DeleteRequestContractorCommandHandler> logger, IRequestContractorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestContractor?>> Handle(DeleteRequestContractorCommand request, CT ct)
    {
        try
        {
            var entity = request.RequestContractor;
            if (entity is null)
                return Result.Failure<RequestContractor>(RequestContractorErrors.RequestContractorNotFound);
            if (entity.IsDeleted)
                return Result.Failure<RequestContractor>(RequestContractorErrors.IsDeleted);

            entity.SetIsDeleted();

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
