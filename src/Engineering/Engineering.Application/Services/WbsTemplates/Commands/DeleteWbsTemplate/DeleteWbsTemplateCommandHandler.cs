using Engineering.Application.Abstractions.Data.WbsTemplates;
using Engineering.Domain.Entities.WbsTemplates;
using Engineering.Domain.Errors.WbsTemplates;

namespace Engineering.Application.Services.WbsTemplates.Commands.DeleteWbsTemplate;

public class DeleteWbsTemplateCommandHandler : ICommandHandler<DeleteWbsTemplateCommand, WbsTemplate?>
{
    private readonly ILogger<DeleteWbsTemplateCommandHandler> _logger;
    private readonly IWbsTemplateRepository _repository;

    public DeleteWbsTemplateCommandHandler(ILogger<DeleteWbsTemplateCommandHandler> logger,
        IWbsTemplateRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<WbsTemplate?>> Handle(DeleteWbsTemplateCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<WbsTemplate>(WbsTemplateErrors.WbsTemplateWithIdNotFound);
            if (entity.ProjectWbses is not null)
                return Result.Failure<WbsTemplate>(WbsTemplateErrors.WbsTemplateHasProjectWbs);
            entity.SoftDelete();
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<WbsTemplate?>(SharedErrors.UnknownError);
        }
    }
}