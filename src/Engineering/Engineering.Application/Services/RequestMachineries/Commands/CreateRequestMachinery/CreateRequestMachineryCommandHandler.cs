using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachinery;

public class CreateRequestMachineryCommandHandler : ICommandHandler<CreateRequestMachineryCommand, RequestMachinery>
{
    private readonly ILogger<CreateRequestMachineryCommandHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public CreateRequestMachineryCommandHandler(ILogger<CreateRequestMachineryCommandHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachinery?>> Handle(CreateRequestMachineryCommand request, CT ct)
    {
        try
        {
            RequestMachinery? result = null;
            if (request.Id is not null)
            {
                result = await _repository.GetByIdAsync(request.Id!.Value, ct);
                if (result is null)
                    return Result.Failure<RequestMachinery>(RequestMachineryErrors.RequestMachineryNotFound);

                if (!(RequestMachineryStatusValidator.AllowStatusForUpdate.Any(x => x == result.Status)))
                    return Result.Failure<RequestMachinery>(RequestMachineryErrors.InValidStatus);

                if (request.IsDeleted)
                {
                    if (!(RequestMachineryStatusValidator.AllowStatusForDelete.Any(x => x == result.Status)))
                        return Result.Failure<RequestMachinery>(RequestMachineryErrors.InValidStatus);

                    result.SetIsDeleted();
                }
                else
                {
                    result.SetData(result.RequestNumber, request.TimeRequired, request.Unit, request.RequestCount, request.FromDate, request.ToDate, request.Description, request.Machinery, request.CompanyId);
                    result.SetProject(request.Project);

                    if (RequestMachineryStatusValidator.AllowStatusForResended.Any(x => x == result.Status))
                        result.ChangeStatus(RequestMachineryStatus.Resended);
                }
                result.AddHistory(null);
                await _repository.Update(result);
            }
            else
            {
                var requestNumber = await _repository.RequestNumberCreator(request.CompanyId, ct);

                var entity = new RequestMachinery(requestNumber, request.TimeRequired, request.Unit, request.RequestCount, request.FromDate, request.ToDate, request.Description, request.Machinery, request.CompanyId);
                entity.SetProject(request.Project);
                entity.AddHistory(null);
                result = await _repository.Create(entity, ct);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachinery>(SharedErrors.UnknownError);
        }
    }
}
