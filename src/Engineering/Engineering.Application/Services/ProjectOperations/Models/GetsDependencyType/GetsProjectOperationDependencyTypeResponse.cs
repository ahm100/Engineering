using Engineering.Application.Services.ProjectOperations.Models.Models;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsDependencyType;

public record GetsProjectOperationDependencyTypeResponse(
    List<DependencyTypeModel> ConsiderationTypes
    );
