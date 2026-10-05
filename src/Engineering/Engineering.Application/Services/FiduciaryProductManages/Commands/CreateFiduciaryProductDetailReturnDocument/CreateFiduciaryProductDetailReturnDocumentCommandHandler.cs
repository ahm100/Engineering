using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProductManages.Commands.CreateFiduciaryProductDetailReturnDocument;

public class CreateFiduciaryProductDetailReturnDocumentCommandHandler : ICommandHandler<CreateFiduciaryProductDetailReturnDocumentCommand, FiduciaryProductDetailReturnDocument>
{
    private readonly ILogger<CreateFiduciaryProductDetailReturnDocumentCommandHandler> _logger;
    private readonly IFiduciaryProductDetailReturnDocumentRepository _repository;

    public CreateFiduciaryProductDetailReturnDocumentCommandHandler(ILogger<CreateFiduciaryProductDetailReturnDocumentCommandHandler> logger, IFiduciaryProductDetailReturnDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FiduciaryProductDetailReturnDocument?>> Handle(CreateFiduciaryProductDetailReturnDocumentCommand request, CT ct)
    {
        try
        {
            var entity = new FiduciaryProductDetailReturnDocument(request.FiduciaryProductReturnDetail, request.Url);

            await _repository.Create(entity, ct);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<FiduciaryProductDetailReturnDocument>(SharedErrors.UnknownError);
        }
    }
}
