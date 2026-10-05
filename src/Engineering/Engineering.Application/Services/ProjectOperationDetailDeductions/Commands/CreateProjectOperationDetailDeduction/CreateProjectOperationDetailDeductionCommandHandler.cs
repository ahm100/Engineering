using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Commands.CreateProjectOperationDetailDeduction;

public class CreateProjectOperationDetailDeductionCommandHandler : ICommandHandler<CreateProjectOperationDetailDeductionCommand, ProjectOperationDetailDeduction>
{
    private readonly ILogger<CreateProjectOperationDetailDeductionCommand> _logger;
    private readonly IProjectOperationDetailDeductionRepository _repository;

    public CreateProjectOperationDetailDeductionCommandHandler(ILogger<CreateProjectOperationDetailDeductionCommand> logger, IProjectOperationDetailDeductionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailDeduction?>> Handle(CreateProjectOperationDetailDeductionCommand request, CT ct)
    {
        try
        {
            var entity = ProjectOperationDetailDeduction.Create(
                    request.ProjectOperationDetail,
                    request.Length,
                    request.Width,
                    request.Height,
                    request.Weight,
                    request.Number);
            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetailDeduction>(SharedErrors.UnknownError);
        }
    }
}