namespace Engineering.Application.Services.ProcesVerbal.Contracts.RemoveProcesVerbal;

public record RemoveProcesVerbalRequest(
    long Id) : IHttpRequest;