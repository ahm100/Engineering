using Engineering.Application.Abstractions.Data.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperations.Command.SetPOActualDate;

public class SetPOActualDateCommandHandler : ICommandHandler<SetPOActualDateCommand, bool?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectOperationRepository _repository;
    private readonly ILogger<SetPOActualDateCommandHandler> _logger;

    public SetPOActualDateCommandHandler(ILogger<SetPOActualDateCommandHandler> logger,
        IProjectOperationRepository repository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool?>> Handle(SetPOActualDateCommand request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationById(request.ProjectOperationId, ct);

            if (result is null)
                return Result.Failure<bool?>(ProjectOperationErrors.NotFound);

            if (result.ActualStartDate == null && result.ActualFinishDate == null)
            {
                result.SetActualStartDate(request.DailyProjectOperationDate);
                result.SetActualFinishDate(request.DailyProjectOperationDate);
            }

            else if (result.ActualStartDate == null || request.DailyProjectOperationDate < result.ActualStartDate)
                result.SetActualStartDate(request.DailyProjectOperationDate);

            else if (result.ActualFinishDate == null || request.DailyProjectOperationDate > result.ActualStartDate)
                result.SetActualFinishDate(request.DailyProjectOperationDate);

            await _repository.Update(result);
            await _unitOfWork.CommitAsync(ct);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}