using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Commands.UpdateOperationInfoGroup;

public record UpdateOperationInfoGroupCommand(
    long Id,
    string OperationInfoGroupName,
    string OperationInfoGroupCode,
    bool IsActive,
    long? CompanyId
    ) : ICommand<OperationInfoGroup>;