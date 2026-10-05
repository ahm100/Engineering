using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Commands.CreateOperationInfoGroup;

public record CreateOperationInfoGroupCommand(
    string OperationInfoGroupName,
    string OperationInfoGroupCode,
    bool IsActive,
    long? CompanyId
    ) : ICommand<OperationInfoGroup>;