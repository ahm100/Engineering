using Engineering.Application.Services.SessionRecords.Contracts.RemoveSessionRecord;

namespace Engineering.Application.Services.SessionRecords.Commands.RemoveSessionRecord;

public record RemoveSessionRecordCommand(
    long Id) : ICommand<RemoveSessionRecordResponse?>;