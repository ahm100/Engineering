using Engineering.Application.Abstractions.Data.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Queries.GetRequestMachineryOperators;

public class GetRequestMachineryOperatorsQueryHandler : IQueryHandler<GetRequestMachineryOperatorsQuery, List<GetRequestMachineryOperatorsQueryModel>>
{
    private readonly ILogger<GetRequestMachineryOperatorsQueryHandler> _logger;
    private readonly IRequestMachineryInquiryOperatorRepository _repository;

    public GetRequestMachineryOperatorsQueryHandler(ILogger<GetRequestMachineryOperatorsQueryHandler> logger, IRequestMachineryInquiryOperatorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<GetRequestMachineryOperatorsQueryModel>?>> Handle(GetRequestMachineryOperatorsQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetFilteredOperatorAsync(request.RequestMachineryId, request.MachineryId, request.MachineryGroupId, ct);

            return entities ?? Result.Failure<List<GetRequestMachineryOperatorsQueryModel>>(RequestMachineryErrors.OperatorNotfound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<GetRequestMachineryOperatorsQueryModel>>(SharedErrors.UnknownError);
        }
    }
}
