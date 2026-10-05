using Engineering.Application.Abstractions.Data.WbsTemplates;
using Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateById;
using Engineering.Domain.Errors.WbsTemplates;

namespace Engineering.Application.Services.WbsTemplates.Queries.GetWbsTemplateById;

public class GetWbsTemplateByIdQueryHandler : IQueryHandler<GetWbsTemplateByIdQuery, GetWbsTemplateByIdResponse?>
{
    private readonly ILogger<GetWbsTemplateByIdQueryHandler> _logger;
    private readonly IWbsTemplateRepository _repository;

    public GetWbsTemplateByIdQueryHandler(ILogger<GetWbsTemplateByIdQueryHandler> logger,
        IWbsTemplateRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetWbsTemplateByIdResponse?>> Handle(GetWbsTemplateByIdQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetWbsTemplateById(request.Id, ct);
            if (entity is null)
                return Result.Failure<GetWbsTemplateByIdResponse?>(WbsTemplateErrors.WbsTemplateWithIdNotFound);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetWbsTemplateByIdResponse?>(SharedErrors.UnknownError);
        }
    }
}