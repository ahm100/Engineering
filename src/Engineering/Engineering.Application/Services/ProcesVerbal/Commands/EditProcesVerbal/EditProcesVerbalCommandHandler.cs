using Engineering.Application.Abstractions.Data.ProcesVerbals;
using Engineering.Application.Services.ProcesVerbal.Contracts.EditProcesVerbal;
using Engineering.Domain.Entities.ProcesVerbal;

namespace Engineering.Application.Services.ProcesVerbal.Commands.EditProcesVerbal;

public class EditProcesVerbalCommandHandler : ICommandHandler<EditProcesVerbalCommand, EditProcesVerbalResponse?>
{
    private readonly ILogger<EditProcesVerbalCommandHandler> _logger;
    private readonly IProcesVerbalsRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public EditProcesVerbalCommandHandler(
        ILogger<EditProcesVerbalCommandHandler> logger,
        IProcesVerbalsRepository repository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<EditProcesVerbalResponse?>> Handle(
        EditProcesVerbalCommand request, CT ct)
    {
        var entity = await _repository.GetById(request.Id);

        if (entity is null)
            return Result.Failure<EditProcesVerbalResponse>(SharedErrors.EntityNotFoundError)!;

        await _unitOfWork.BeginTransactionAsync(ct);
        try
        {
            entity.SetTitleFa(request.TitleFa ?? entity.TitleFa);
            entity.SetTitleEn(request.TitleEn);
            entity.SetType(request.Type ?? entity.Type);
            entity.SetRecordDateTime(request.RecordDateTime ?? entity.RecordDateTime);
            entity.SetLocation(request.Location ?? entity.Location);

            if (request.Items is not null)
            {
                foreach (var item in entity.ProcesVerbalItems)
                    item.SoftDelete();

                foreach (var titleFa in request.Items)
                    entity.AddItem(ProcesVerbalItem.Create(entity, titleFa));
            }

            if (request.Docs is not null)
            {
                foreach (var doc in entity.ProcesVerbalDocuments)
                    doc.SoftDelete();

                foreach (var url in request.Docs)
                    entity.AddDocument(new ProcesVerbalDoc(url, entity));
            }

            await _repository.Update(entity);
            await _unitOfWork.CommitTransactionAsync(ct);

            return new EditProcesVerbalResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error EditProcesVerbal: {TitleFa}", request.TitleFa);
            await _unitOfWork.RollbackTransactionAsync(ct);
            return Result.Failure<EditProcesVerbalResponse>(SharedErrors.UnknownError)!;
        }
    }
}