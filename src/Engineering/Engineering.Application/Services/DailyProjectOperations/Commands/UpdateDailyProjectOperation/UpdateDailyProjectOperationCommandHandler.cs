using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.UpdateDailyProjectOperation;

public class UpdateDailyProjectOperationCommandHandler : ICommandHandler<UpdateDailyProjectOperationCommand, DailyProjectOperation>
{
    private readonly ILogger<UpdateDailyProjectOperationCommandHandler> _logger;
    private readonly IDailyProjectOperationRepository _repository;

    public UpdateDailyProjectOperationCommandHandler(ILogger<UpdateDailyProjectOperationCommandHandler> logger,
                                                     IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperation?>> Handle(UpdateDailyProjectOperationCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.DailyProjectOperationId, ct);
            if (entity is null)
                return Result.Failure<DailyProjectOperation?>(DailyProjectOperationErrors.DailyProjectOperationWithIdNotFound);

            entity.SetStartDate(request.StartDate);
            entity.SetEndDate(request.EndDate);
            entity.SetHeight(request.Height);
            entity.SetLength(request.Length);
            entity.SetNumber(request.Number);
            entity.SetStatus(request.Status);
            entity.SetWidth(request.Width);
            entity.SetWeight(request.Weight);
            entity.SetCompanyId(request.CompanyId);
            entity.SetDescription(request.Description);

            entity.AddHistory();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperation?>(SharedErrors.UnknownError);
        }
    }
}
