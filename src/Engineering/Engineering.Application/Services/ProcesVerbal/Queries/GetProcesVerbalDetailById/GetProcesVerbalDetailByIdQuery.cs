using Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbalDetailById;

namespace Engineering.Application.Services.ProcesVerbal.Queries.GetProcesVerbalDetailById;

public record GetProcesVerbalDetailByIdQuery(
    long Id) : IQuery<GetProcesVerbalDetailByIdResponse?>;