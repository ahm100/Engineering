using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectSchedule;

namespace Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectSchedule;

public class RemoveProjectScheduleCommandHandler : ICommandHandler<RemoveProjectScheduleCommand, RemoveProjectScheduleResponse?>
{
    private readonly ILogger<RemoveProjectScheduleCommandHandler> _logger;
    private readonly IProjectScheduleImportRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveProjectScheduleCommandHandler(
        ILogger<RemoveProjectScheduleCommandHandler> logger,
        IProjectScheduleImportRepository repository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RemoveProjectScheduleResponse?>> Handle(
        RemoveProjectScheduleCommand request, CT ct)
    {
        try
        {
            var import = await _repository.GetByProjectId(request.ProjectId, ct);

            if (import is null)
                return Result.Failure<RemoveProjectScheduleResponse>(ProjectErrors.ProjectImportNotExist)!;

            import.SoftDelete();
            await _repository.Update(import);
            await _unitOfWork.CommitAsync(ct);

            return new RemoveProjectScheduleResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RemoveProjectScheduleResponse>(SharedErrors.UnknownError)!;
        }
    }
}
