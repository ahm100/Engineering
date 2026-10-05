using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractors.Commands.CreateRequestContractor;

public class CreateRequestContractorCommandHandler : ICommandHandler<CreateRequestContractorCommand, RequestContractor>
{
    private readonly ILogger<CreateRequestContractorCommandHandler> _logger;
    private readonly IRequestContractorRepository _repository;

    public CreateRequestContractorCommandHandler(ILogger<CreateRequestContractorCommandHandler> logger, IRequestContractorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestContractor?>> Handle(CreateRequestContractorCommand request, CT ct)
    {
        try
        {
            var entity = new RequestContractor(
                request.Volume,
                request.Description,
                request.CompanyId,
                request.ProjectOperationDetail,
                request.ServiceInfo);

            entity.AddHistory(null);

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestContractor>(SharedErrors.UnknownError);
        }
    }
}
