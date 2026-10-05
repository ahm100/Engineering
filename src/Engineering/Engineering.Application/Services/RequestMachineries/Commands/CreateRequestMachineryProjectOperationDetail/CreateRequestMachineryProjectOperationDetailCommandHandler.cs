using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryProjectOperationDetail;

public class CreateRequestMachineryProjectOperationDetailCommandHandler : ICommandHandler<CreateRequestMachineryProjectOperationDetailCommand, RequestMachineryProjectOperationDetail>
{
    private readonly ILogger<CreateRequestMachineryProjectOperationDetailCommandHandler> _logger;
    private readonly IRequestMachineryProjectOperationDetailRepository _repository;

    public CreateRequestMachineryProjectOperationDetailCommandHandler(ILogger<CreateRequestMachineryProjectOperationDetailCommandHandler> logger, IRequestMachineryProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryProjectOperationDetail?>> Handle(CreateRequestMachineryProjectOperationDetailCommand request, CT ct)
    {
        try
        {
            RequestMachineryProjectOperationDetail? result = null;
            if (request.Id is not null)
            {
                result = await _repository.FindById(request.Id!.Value, ct);
                if (result is null)
                    return Result.Failure<RequestMachineryProjectOperationDetail>(RequestMachineryProjectOperationDetailErrors.RequestMachineryProjectOperationDetailNotFound);

                if (request.IsDeleted)
                    result.SetIsDeleted();
                else
                {
                    result.SetData(request.ProjectOperationDetail, request.RequestMachinery);
                }

                await _repository.Update(result);
            }
            else
            {

                var entity = new RequestMachineryProjectOperationDetail(request.ProjectOperationDetail, request.RequestMachinery);

                result = await _repository.Create(entity, ct);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
