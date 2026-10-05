using Engineering.Application.Abstractions.Data.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryAssignments;

public class DeleteRequestMachineryAssignmentsCommandHandler : ICommandHandler<DeleteRequestMachineryAssignmentsCommand, bool?>
{
    private readonly ILogger<DeleteRequestMachineryAssignmentsCommandHandler> _logger;
    private readonly IRequestMachineryAssignmentRepository _repository;

    public DeleteRequestMachineryAssignmentsCommandHandler(ILogger<DeleteRequestMachineryAssignmentsCommandHandler> logger, IRequestMachineryAssignmentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(DeleteRequestMachineryAssignmentsCommand request, CT ct)
    {
        try
        {
            var entities = request.RequestMachinery.RequestMachineryAssignments.ToList();

            if (entities is not null)
                if (entities.Count > 0)
                    foreach (var item in entities)
                    {
                        item.SetIsDeleted();
                        await _repository.Update(item);
                    }

            return true;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}
