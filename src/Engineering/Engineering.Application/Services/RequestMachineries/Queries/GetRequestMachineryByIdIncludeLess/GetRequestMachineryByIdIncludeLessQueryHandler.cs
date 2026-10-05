using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryByIdIncludeLess;

public class GetRequestMachineryByIdIncludeLessQueryHandler : IQueryHandler<GetRequestMachineryByIdIncludeLessQuery, RequestMachinery>
{
    private readonly ILogger<GetRequestMachineryByIdIncludeLessQueryHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public GetRequestMachineryByIdIncludeLessQueryHandler(ILogger<GetRequestMachineryByIdIncludeLessQueryHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachinery?>> Handle(GetRequestMachineryByIdIncludeLessQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByIdIncludeLess(request.RequestMachineryId, ct);
            return result ?? Result.Failure<RequestMachinery>(RequestMachineryErrors.RequestMachineryNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestMachinery>(SharedErrors.UnknownError);
        }
    }
}
