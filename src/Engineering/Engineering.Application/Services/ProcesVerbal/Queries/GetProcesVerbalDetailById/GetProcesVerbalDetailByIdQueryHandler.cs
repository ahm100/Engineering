using Engineering.Application.Abstractions.Data.ProcesVerbals;
using Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbalDetailById;
using Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbals;
using Microsoft.Identity.Client;
using System.Runtime.CompilerServices;

namespace Engineering.Application.Services.ProcesVerbal.Queries.GetProcesVerbalDetailById;

public class GetProcesVerbalDetailByIdQueryHandler : IQueryHandler<GetProcesVerbalDetailByIdQuery, GetProcesVerbalDetailByIdResponse?>
{
    private readonly ILogger<GetProcesVerbalDetailByIdQueryHandler> _logger;
    private readonly IProcesVerbalsRepository _repository;

    public GetProcesVerbalDetailByIdQueryHandler(
        ILogger<GetProcesVerbalDetailByIdQueryHandler> logger,
        IProcesVerbalsRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetProcesVerbalDetailByIdResponse?>> Handle(
        GetProcesVerbalDetailByIdQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetProcesVerbalDetailById(request.Id, ct);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error GetProcesVerbalDetailById Id: {Id}", request.Id);
            return Result.Failure<GetProcesVerbalDetailByIdResponse>(SharedErrors.UnknownError)!;
        }
    }
}
