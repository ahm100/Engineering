using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryDocument;

public class UpdateRequestMachineryDocumentCommandHandler : ICommandHandler<UpdateRequestMachineryDocumentCommand, RequestMachineryDocument>
{
    private readonly ILogger<UpdateRequestMachineryDocumentCommandHandler> _logger;
    private readonly IRequestMachineryDocumentRepository _repository;

    public UpdateRequestMachineryDocumentCommandHandler(ILogger<UpdateRequestMachineryDocumentCommandHandler> logger, IRequestMachineryDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryDocument?>> Handle(UpdateRequestMachineryDocumentCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<RequestMachineryDocument>(RequestMachineryErrors.RequestMachineryDocumentWithIdNotFound);

            entity.SetUrl(request.Url);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryDocument>(SharedErrors.UnknownError);
        }
    }
}
