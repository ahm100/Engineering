using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationDocument;

public class CreateDailyProjectOperationDocumentCommandHandler : ICommandHandler<CreateDailyProjectOperationDocumentCommand, DailyProjectOperationDocument>
{
    private readonly ILogger<CreateDailyProjectOperationDocumentCommandHandler> _logger;
    private readonly IDailyProjectOperationDocumentRepository _repository;

    public CreateDailyProjectOperationDocumentCommandHandler(ILogger<CreateDailyProjectOperationDocumentCommandHandler> logger,
                                                             IDailyProjectOperationDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperationDocument?>> Handle(CreateDailyProjectOperationDocumentCommand request, CT ct)
    {
        try
        {
            var entity = new DailyProjectOperationDocument(request.Url,
                                                           request.DailyProjectOperation);

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperationDocument?>(SharedErrors.UnknownError);
        }
    }
}
