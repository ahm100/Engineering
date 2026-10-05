using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.RemoveRequestContractorMachinery;

public class RemoveRequestContractorMachineryCommandHandler : ICommandHandler<RemoveRequestContractorMachineryCommand, RequestMachinery>
{
    private readonly ILogger<RemoveRequestContractorMachineryCommandHandler> _logger;
    private readonly IRequestMachineryRepository _repository;

    public RemoveRequestContractorMachineryCommandHandler(ILogger<RemoveRequestContractorMachineryCommandHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachinery?>> Handle(RemoveRequestContractorMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.RequestMachinery.Id, ct);
            if (entity is null)
                return Result.Failure<RequestMachinery>(RequestMachineryInquiryErrors.RequestMachineryInquiryNotFOund);

            entity.SetContractorMachinery(null);
            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachinery>(SharedErrors.UnknownError);
        }
    }
}
