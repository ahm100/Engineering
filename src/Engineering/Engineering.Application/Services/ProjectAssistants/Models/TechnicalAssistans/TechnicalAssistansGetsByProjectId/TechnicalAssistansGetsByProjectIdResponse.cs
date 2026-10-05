using Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.TechnicalAssistansModels;

namespace Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.TechnicalAssistansGetsByProjectId;

public record TechnicalAssistansGetsByProjectIdResponse(
    List<TechnicalAssistansGetsByProjectIdModel> Data,
    int RowCount);
