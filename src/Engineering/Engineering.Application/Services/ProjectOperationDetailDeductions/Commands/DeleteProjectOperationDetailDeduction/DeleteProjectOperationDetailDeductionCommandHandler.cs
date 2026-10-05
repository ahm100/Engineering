using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetailDeduction = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailDeduction;

namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Commands.DeleteProjectOperationDetailDeduction;

public class DeleteProjectOperationDetailDeductionCommandHandler : ICommandHandler<DeleteProjectOperationDetailDeductionCommand, ProjectOperationDetailDeduction>
{
    private readonly ILogger<DeleteProjectOperationDetailDeductionCommand> _logger;
    private readonly IProjectOperationDetailDeductionRepository _repository;

    public DeleteProjectOperationDetailDeductionCommandHandler(ILogger<DeleteProjectOperationDetailDeductionCommand> logger, IProjectOperationDetailDeductionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailDeduction?>> Handle(DeleteProjectOperationDetailDeductionCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetailDeduction>(ProjectOperationDetailDeductionErrors.DeductionWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<ProjectOperationDetailDeduction>(ProjectOperationDetailDeductionErrors.CanNotDelete);

            entity.SetIsDeleted();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetailDeduction>(SharedErrors.UnknownError);
        }
    }
}