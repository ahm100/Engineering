using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductManages.Commands.CreateFiduciaryProductDetailReturn;

public class CreateFiduciaryProductDetailReturnCommandHandler : ICommandHandler<CreateFiduciaryProductDetailReturnCommand, FiduciaryProductDetailReturn>
{
    private readonly ILogger<CreateFiduciaryProductDetailReturnCommandHandler> _logger;
    private readonly IFiduciaryProductDetailReturnRepository _repository;

    public CreateFiduciaryProductDetailReturnCommandHandler(ILogger<CreateFiduciaryProductDetailReturnCommandHandler> logger, IFiduciaryProductDetailReturnRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FiduciaryProductDetailReturn?>> Handle(CreateFiduciaryProductDetailReturnCommand request, CT ct)
    {
        try
        {
            var entity = new FiduciaryProductDetailReturn(request.InvoiceId, request.Description, request.ReturnCount, request.ReturnDate, request.LateDay, request.LateFine, request.CurrencyId, request.Type, request.FiduciaryProductDetailManagement);

            await _repository.Create(entity, ct);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FiduciaryProductDetailReturn>(SharedErrors.UnknownError);
        }
    }
}
