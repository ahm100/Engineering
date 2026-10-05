using Engineering.Application.Abstractions.Data.WbsTemplates;
using Engineering.Application.Services.WbsTemplates.Contracts.GetFltrWbsTemplate;
using Engineering.Domain.Errors.WbsTemplates;

namespace Engineering.Application.Services.WbsTemplates.Queries.GetFltrWbsTemplate;

public class GetFltrWbsTemplateQueryHandler : IQueryHandler<GetFltrWbsTemplateQuery, GetFltrWbsTemplateResponse?>
{
    private readonly ILogger<GetFltrWbsTemplateQueryHandler> _logger;
    private readonly IWbsTemplateRepository _repository;

    public GetFltrWbsTemplateQueryHandler(ILogger<GetFltrWbsTemplateQueryHandler> logger,
        IWbsTemplateRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetFltrWbsTemplateResponse?>> Handle(GetFltrWbsTemplateQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFltrWbsTemplate(request.FilterData,
                request.PageIndex,
                request.PageSize, ct);
            if (result.Data is null || result.RowCount < 1)
                return Result.Failure<GetFltrWbsTemplateResponse?>(WbsTemplateErrors.WbsTemplateWithFilterNotFound);

            return new GetFltrWbsTemplateResponse(result.Data, result.RowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFltrWbsTemplateResponse?>(SharedErrors.UnknownError);
        }
    }
}