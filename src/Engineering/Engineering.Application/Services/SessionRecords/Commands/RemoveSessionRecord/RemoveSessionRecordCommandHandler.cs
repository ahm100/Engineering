using Engineering.Application.Abstractions.Data.SessionRecords;
using Engineering.Application.Services.ProcesVerbal.Contracts.RemoveProcesVerbal;
using Engineering.Application.Services.SessionRecords.Contracts.RemoveSessionRecord;

namespace Engineering.Application.Services.SessionRecords.Commands.RemoveSessionRecord;

public class RemoveSessionRecordCommandHandler : ICommandHandler<RemoveSessionRecordCommand, RemoveSessionRecordResponse?>
{
    private readonly ILogger<RemoveSessionRecordCommandHandler> _logger;
    private readonly ISessionRecordRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSessionRecordCommandHandler(
        ILogger<RemoveSessionRecordCommandHandler> logger,
        ISessionRecordRepository repository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RemoveSessionRecordResponse?>> Handle(
        RemoveSessionRecordCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id);
            if (entity is null)
                return Result.Failure<RemoveSessionRecordResponse>(SharedErrors.ItemNotFound)!;

            entity.SoftDelete();
            await _repository.Update(entity);
            await _unitOfWork.CommitAsync(ct);

            return new RemoveSessionRecordResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error RemoveSessionRecord: {Id}", request.Id);
            return Result.Failure<RemoveSessionRecordResponse>(SharedErrors.UnknownError)!;
        }
    }
}
