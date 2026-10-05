namespace Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbalDetailById;

public record GetProcesVerbalDetailByIdRequest(
    long Id) : IHttpRequest;