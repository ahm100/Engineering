using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductManages.Commands.DeleteFiduciaryProductDetailReturn;

public class DeleteFiduciaryProductDetailReturnCommandHandler : ICommandHandler<DeleteFiduciaryProductDetailReturnCommand, FiduciaryProductDetailReturn>
{
    private readonly ILogger<DeleteFiduciaryProductDetailReturnCommandHandler> _logger;
    private readonly IFiduciaryProductDetailReturnRepository _repository;

    public DeleteFiduciaryProductDetailReturnCommandHandler(ILogger<DeleteFiduciaryProductDetailReturnCommandHandler> logger, IFiduciaryProductDetailReturnRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FiduciaryProductDetailReturn?>> Handle(DeleteFiduciaryProductDetailReturnCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(request.FiduciaryProductDetailReturnId, ct);

            if (entity is null)
                return Result.Failure<FiduciaryProductDetailReturn>(FiduciaryProductDetailReturnErrors.FiduciaryProductDetailReturnWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<FiduciaryProductDetailReturn>(FiduciaryProductDetailReturnErrors.IsDeleted);

            entity.SetIsDeleted();

            foreach (var document in entity.Documents)
            {
                document.SetIsDeleted();
            }

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FiduciaryProductDetailReturn>(SharedErrors.UnknownError);
        }
    }
}
