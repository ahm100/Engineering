using Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateById;

namespace Engineering.Application.Services.WbsTemplates.Queries.GetWbsTemplateById;

public record GetWbsTemplateByIdQuery(
    long Id) : IQuery<GetWbsTemplateByIdResponse?>;