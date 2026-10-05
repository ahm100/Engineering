using Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.ImplementationAssistantsModels;

namespace Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.ImplementationAssistantsGetsByProjectId;

public record ImplementationAssistansGetsByProjectIdResponse(
    List<ImplementationAssistansGetsByProjectIdModel> Data,
    int RowCount);
