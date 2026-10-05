using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetailDeduction = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailDeduction;

namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Commands.UpdateProjectOperationDetailDeduction;

public class UpdateProjectOperationDetailDeductionCommandHandler : ICommandHandler<UpdateProjectOperationDetailDeductionCommand, ProjectOperationDetailDeduction>
{
    private readonly ILogger<UpdateProjectOperationDetailDeductionCommand> _logger;
    private readonly IProjectOperationDetailDeductionRepository _repository;

    public UpdateProjectOperationDetailDeductionCommandHandler(ILogger<UpdateProjectOperationDetailDeductionCommand> logger, IProjectOperationDetailDeductionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailDeduction?>> Handle(UpdateProjectOperationDetailDeductionCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetailDeduction>(ProjectOperationDetailDeductionErrors.DeductionWithIdNotFound);

            var finalAmounts = request.Height * request.Weight * request.Width * request.Number * request.Length;
            var detailDeductions = entity.ProjectOperationDetail.ProjectOperationDetailDeductions.Sum(x => x.FinalAmount);
            if ((finalAmounts + detailDeductions) >= entity.ProjectOperationDetail.FinalAmount)
                return Result.Failure<ProjectOperationDetailDeduction>(ProjectOperationDetailErrors.DeductionsUnValid);

            entity.SetLength(request.Length);
            entity.SetWidth(request.Width);
            entity.SetHeight(request.Height);
            entity.SetWeight(request.Weight);
            entity.SetNumber(request.Number);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ProjectOperationDetailDeduction>(SharedErrors.UnknownError);
        }
    }
}