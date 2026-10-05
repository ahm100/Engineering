using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryProjectOperation;

public class CreateRequestMachineryProjectOperationCommandHandler : ICommandHandler<CreateRequestMachineryProjectOperationCommand, RequestMachineryProjectOperation>
{
    private readonly ILogger<CreateRequestMachineryProjectOperationCommandHandler> _logger;
    private readonly IRequestMachineryProjectOperationRepository _repository;

    public CreateRequestMachineryProjectOperationCommandHandler(ILogger<CreateRequestMachineryProjectOperationCommandHandler> logger, IRequestMachineryProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryProjectOperation?>> Handle(CreateRequestMachineryProjectOperationCommand request, CT ct)
    {

        try
        {
            RequestMachineryProjectOperation? result = null;
            if (request.Id is not null)
            {
                result = await _repository.FindById(request.Id!.Value, ct);
                if (result is null)
                    return Result.Failure<RequestMachineryProjectOperation>(RequestMachineryProjectOperationErrors.RequestMachineryProjectOperationNotFound);

                if (request.IsDeleted)
                    result.SetIsDeleted();
                else
                {
                    result.SetData(request.ProjectOperation, request.RequestMachinery);
                }

                await _repository.Update(result);
            }
            else
            {
                var entity = new RequestMachineryProjectOperation(request.ProjectOperation, request.RequestMachinery);

                result = await _repository.Create(entity, ct);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryProjectOperation>(SharedErrors.UnknownError);
        }
    }
}
