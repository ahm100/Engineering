using Engineering.Application.Abstractions.Data.ProcesVerbals;
using Engineering.Application.Services.ProcesVerbal.Contracts.RemoveProcesVerbal;

namespace Engineering.Application.Services.ProcesVerbal.Commands.RemoveProcesVerbal;

public class RemoveProcesVerbalCommandHandler : ICommandHandler<RemoveProcesVerbalCommand, RemoveProcesVerbalResponse?>
{
    private readonly ILogger<RemoveProcesVerbalCommandHandler> _logger;
    private readonly IProcesVerbalsRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveProcesVerbalCommandHandler(
        ILogger<RemoveProcesVerbalCommandHandler> logger,
        IProcesVerbalsRepository repository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RemoveProcesVerbalResponse?>> Handle(
        RemoveProcesVerbalCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id);
            if (entity is null)
                return Result.Failure<RemoveProcesVerbalResponse>(SharedErrors.ItemNotFound)!;

            entity.SoftDelete();
            await _repository.Update(entity);
            await _unitOfWork.CommitAsync(ct);

            return new RemoveProcesVerbalResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error RemoveProcesVerbal: {Id}", request.Id);
            return Result.Failure<RemoveProcesVerbalResponse>(SharedErrors.UnknownError)!;
        }
    }
}
