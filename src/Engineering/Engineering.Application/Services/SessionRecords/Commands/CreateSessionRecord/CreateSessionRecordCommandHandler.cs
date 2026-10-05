using Engineering.Application.Abstractions.Data.Contracts;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.SessionRecords;
using Engineering.Application.Services.SessionRecords.Contracts.CreateSessionRecord;
using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.SessionRecords;

namespace Engineering.Application.Services.SessionRecords.Commands.CreateSessionRecord;

public class CreateSessionRecordCommandHandler : ICommandHandler<CreateSessionRecordCommand, CreateSessionRecordResponse?>
{
    private readonly ILogger<CreateSessionRecordCommandHandler> _logger;
    private readonly ISessionRecordRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IContractRepository _contractRepository;
    private readonly IProjectRepository _projectRepository;

    public CreateSessionRecordCommandHandler(
        ILogger<CreateSessionRecordCommandHandler> logger,
         ISessionRecordRepository repository,
         IUnitOfWork unitOfWork,
         IContractRepository contractRepository,
         IProjectRepository projectRepository)
    {
        _repository = repository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _contractRepository = contractRepository;
        _projectRepository = projectRepository;
    }

    public async Task<Result<CreateSessionRecordResponse?>> Handle(
        CreateSessionRecordCommand request, CT ct)
    {
        Contract? contract;
        if (request.ContractId > 0)
        {
            contract = await _contractRepository.GetContractById(request.ContractId ?? 0, ct);
            if (contract is null)
                return Result.Failure<CreateSessionRecordResponse>(SharedErrors.ItemNotFound)!;
        }

        string? ProjectName = request.ProjectName;

        var project = await _projectRepository.GetById(request.ProjectId, ct);
        if (project is null)
            return Result.Failure<CreateSessionRecordResponse>(SharedErrors.ItemNotFound)!;
        ProjectName = project.ProjectName;

        await _unitOfWork.BeginTransactionAsync(ct);
        try
        {
            var sessionRecord = SessionRecord.Create(
                request.TitleFa, request.TitleEn, ProjectName, request.ContractNumber,
                request.SessionDate, request.StartTime, request.EndTime, request.Location,
                request.Category, request.Type);


            foreach (var item in request.Items)
            {
                sessionRecord.AddItem(SessionItem.Create(sessionRecord, item));
            }

            foreach (var doc in request.Docs)
            {
                sessionRecord.AddDocument(new SessionRecordDoc(doc.URL, doc.Type, sessionRecord));
            }

            foreach (var action in request.Actions)
            {
                sessionRecord.AddAction(SessionRecordAction.Create(
                    sessionRecord, action.Description, action.Status, action.DeadLine, action.UserId));
            }

            foreach (var invite in request.Invitees)
            {
                sessionRecord.AddInvitee(SessionInvitee.Create(
                    sessionRecord, invite.Status, invite.CompanyId, invite.UserId));
            }

            await _repository.Create(sessionRecord, ct);
            await _unitOfWork.CommitAsync(ct);
            await _unitOfWork.CommitTransactionAsync(ct);

            return Result.Success(new CreateSessionRecordResponse(sessionRecord.Id, true))!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error CreateSessionRecord: {TitleFa}", request.TitleFa);
            await _unitOfWork.RollbackTransactionAsync(ct);
            return Result.Failure<CreateSessionRecordResponse>(SharedErrors.UnknownError)!;
        }
    }
}
