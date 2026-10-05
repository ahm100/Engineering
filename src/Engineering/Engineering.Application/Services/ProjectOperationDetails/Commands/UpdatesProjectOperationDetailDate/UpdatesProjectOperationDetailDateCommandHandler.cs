using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.UpdatesProjectOperationDetailDate;

public class UpdatesProjectOperationDetailDateCommandHandler : ICommandHandler<UpdatesProjectOperationDetailDateCommand, ProjectOperationDetail>
{
    private readonly ILogger<UpdatesProjectOperationDetailDateCommand> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public UpdatesProjectOperationDetailDateCommandHandler(ILogger<UpdatesProjectOperationDetailDateCommand> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(UpdatesProjectOperationDetailDateCommand request, CT ct)
    {
        try
        {
            var entity = request.ProjectOperationDetail;
            entity.SetStartDate(request.StartDate);
            entity.SetEndDate(request.EndDate);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}