using Engineering.Application.Abstractions.Data.SessionRecords;
using Engineering.Application.Services.SessionRecords.Contracts.EditSessionRecord;
using Engineering.Domain.Entities.SessionRecords;

namespace Engineering.Application.Services.SessionRecords.Commands.EditSessionRecord;

public class EditSessionRecordCommandHandler : ICommandHandler<EditSessionRecordCommand, EditSessionRecordResponse?>
{
    private readonly ILogger<EditSessionRecordCommandHandler> _logger;
    private readonly ISessionRecordRepository _repository;

    public EditSessionRecordCommandHandler(
        ILogger<EditSessionRecordCommandHandler> logger,
        ISessionRecordRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<EditSessionRecordResponse?>> Handle(
        EditSessionRecordCommand request, CT ct)
    {
        var entity = await _repository.GetById(request.Id);

        if (entity is null)
            return Result.Failure<EditSessionRecordResponse>(SharedErrors.EntityNotFoundError)!;

        try
        {
            entity.SetTitleFa(request.TitleFa ?? entity.TitleFa);
            entity.SetTitleEn(request.TitleEn ?? entity.TitleEn);
            entity.SetSessionCategory(request.Category ?? entity.SessionCategory);
            entity.SetSessionType(request.Type ?? entity.SessionType);
            entity.SetSessionDate(request.SessionDate ?? entity.SessionDate);
            entity.SetStartTime(request.StartTime ?? entity.StartTime);
            entity.SetEndTime(request.EndTime ?? entity.EndTime);

            if (request.ProjectId is not null)
                entity.ProjctId = request.ProjectId;

            if (request.ContractId is not null)
                entity.ContractId = request.ContractId;

            if (request.Items is not null)
            {
                foreach (var item in entity.SessionItems)
                    item.SoftDelete();

                foreach (var titleFa in request.Items)
                    entity.AddItem(SessionItem.Create(entity, titleFa));
            }

            if (request.Docs is not null)
            {
                foreach (var doc in entity.SessionRecordDocs)
                    doc.SoftDelete();

                foreach (var docModel in request.Docs)
                    entity.AddDocument(new SessionRecordDoc(docModel.URL, docModel.Type, entity));
            }

            if (request.Invitees is not null)
            {
                foreach (var invitee in entity.SessionInvitees)
                    invitee.SoftDelete();

                foreach (var inviteeModel in request.Invitees)
                    entity.AddInvitee(SessionInvitee.Create(
                        entity, inviteeModel.Status, inviteeModel.CompanyId, inviteeModel.UserId));
            }

            if (request.Actions is not null)
            {
                foreach (var action in entity.SessionRecordActions)
                    action.SoftDelete();

                foreach (var actionModel in request.Actions)
                    entity.AddAction(SessionRecordAction.Create(
                        entity, actionModel.Description, actionModel.Status, actionModel.DeadLine, actionModel.UserId));
            }

            await _repository.Update(entity);

            return new EditSessionRecordResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error EditSessionRecord: {TitleFa}", request.TitleFa);
            return Result.Failure<EditSessionRecordResponse>(SharedErrors.UnknownError)!;
        }
    }
}