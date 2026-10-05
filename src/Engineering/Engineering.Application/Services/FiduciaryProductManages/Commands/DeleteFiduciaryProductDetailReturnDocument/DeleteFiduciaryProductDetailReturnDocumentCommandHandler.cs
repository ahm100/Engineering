using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductManages.Commands.DeleteFiduciaryProductDetailReturnDocument;

public class DeleteFiduciaryProductDetailReturnDocumentCommandHandler : ICommandHandler<DeleteFiduciaryProductDetailReturnDocumentCommand, FiduciaryProductDetailReturnDocument>
{
    private readonly IFiduciaryProductDetailReturnDocumentRepository _repository;
    private readonly ILogger<DeleteFiduciaryProductDetailReturnDocumentCommandHandler> _logger;

    public DeleteFiduciaryProductDetailReturnDocumentCommandHandler(IFiduciaryProductDetailReturnDocumentRepository repository, ILogger<DeleteFiduciaryProductDetailReturnDocumentCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<FiduciaryProductDetailReturnDocument?>> Handle(DeleteFiduciaryProductDetailReturnDocumentCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.FiduciaryProductDetailReturnDocumentId, ct);

            if (entity is null)
                return Result.Failure<FiduciaryProductDetailReturnDocument>(FiduciaryProductDetailReturnDocumentErrors.FiduciaryProductDetailReturnDocumentWithIdNotFound);

            entity.SetIsDeleted();

            return entity;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FiduciaryProductDetailReturnDocument>(SharedErrors.UnknownError);
        }
    }
}
