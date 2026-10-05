using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.RequestMachineryAssignments;

public class CreateRequestMachineryAssignmentCommandHandler : ICommandHandler<CreateRequestMachineryAssignmentCommand, RequestMachineryAssignment>
{
    private readonly ILogger<CreateRequestMachineryAssignmentCommandHandler> _logger;
    private readonly IRequestMachineryAssignmentRepository _repository;

    public CreateRequestMachineryAssignmentCommandHandler(ILogger<CreateRequestMachineryAssignmentCommandHandler> logger,
                                                IRequestMachineryAssignmentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryAssignment?>> Handle(CreateRequestMachineryAssignmentCommand request, CT ct)
    {
        try
        {
            RequestMachineryAssignment? result = null;
            if (request.Id is not null)
            {
                result = await _repository.FindById(request.Id!.Value, ct);
                if (result is null)
                    return Result.Failure<RequestMachineryAssignment>(RequestMachineryErrors.RequestMachineryNotFound);

                if (request.IsDeleted)
                    result.SetIsDeleted();
                else
                {
                    result.SetData(request.MachineryIdentifier, request.RequestMachinery);
                }

                await _repository.Update(result);
            }
            else
            {
                var entity = new RequestMachineryAssignment(request.MachineryIdentifier, request.RequestMachinery);

                result = await _repository.Create(entity, ct);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryAssignment>(SharedErrors.UnknownError);
        }
    }
}
