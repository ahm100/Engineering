
namespace Engineering.Application.Services.OperationInfoDependencies.Models.GetOperationInfoDependencyById;

public record GetOperationInfoDependencyByIdResponse(
    long Id,
    long RelationId,
    string RelationCode,
    string RelationName,
    int WorkingDays,
    string DependencyType
    );
