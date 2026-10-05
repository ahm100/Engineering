using Engineering.Application.Abstractions.Data.WbsTemplates;
using Engineering.Domain.Entities.WbsTemplates;
using Engineering.Domain.Errors.WbsTemplates;

namespace Engineering.Application.Services.WbsTemplates.Commands.UpdateWbsTemplate;

public class UpdateWbsTemplateCommandHandler : ICommandHandler<UpdateWbsTemplateCommand, WbsTemplate?>
{
    private readonly ILogger<UpdateWbsTemplateCommandHandler> _logger;
    private readonly IWbsTemplateRepository _repository;

    public UpdateWbsTemplateCommandHandler(ILogger<UpdateWbsTemplateCommandHandler> logger,
        IWbsTemplateRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<WbsTemplate?>> Handle(UpdateWbsTemplateCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<WbsTemplate>(WbsTemplateErrors.WbsTemplateWithIdNotFound);

            if (request.Title != null)
            {
                var isDuplicateTitle = await _repository.IsDuplicateTitle(request.Title, ct);
                if (isDuplicateTitle is not null && isDuplicateTitle.Id != entity.Id)
                    return Result.Failure<WbsTemplate?>(WbsTemplateErrors.WbsTemplateNameDuplicate);
            }
            if (request.Code != null)
            {
                var isDuplicateCode = await _repository.IsDuplicateCode(request.Code, ct);
                if (isDuplicateCode is not null && isDuplicateCode.Id != entity.Id)
                    return Result.Failure<WbsTemplate?>(WbsTemplateErrors.WbsTemplateCodeDuplicate);
            }

            entity.Update(request.Title,
                request.Code,
                request.Description,
                request.IsActive);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<WbsTemplate?>(SharedErrors.UnknownError);
        }
    }
}