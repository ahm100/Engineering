using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperation;

public class CreateDailyProjectOperationCommandHandler : ICommandHandler<CreateDailyProjectOperationCommand, DailyProjectOperation>
{
    private readonly ILogger<CreateDailyProjectOperationCommandHandler> _logger;
    private readonly IDailyProjectOperationRepository _repository;

    public CreateDailyProjectOperationCommandHandler(ILogger<CreateDailyProjectOperationCommandHandler> logger,
                                                     IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperation?>> Handle(CreateDailyProjectOperationCommand request, CT ct)
    {
        try
        {
            var entity = new DailyProjectOperation(request.Status, request.StartDate, request.EndDate, request.Length, request.Width, request.Height,
                request.Weight, request.Number, request.ProjectOperationDetail, request.Description, request.CompanyId, request.LegacyId);

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperation?>(SharedErrors.UnknownError);
        }
    }
}
