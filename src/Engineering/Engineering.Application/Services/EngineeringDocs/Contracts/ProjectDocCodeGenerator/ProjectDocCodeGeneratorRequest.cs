
namespace Engineering.Application.Services.EngineeringDocs.Contracts.ProjectDocCodeGenerator;

public record ProjectDocCodeGeneratorRequest(
    long ProjectId,
    long DisciplineId,
    long DisciplineDocId
) : IHttpRequest;