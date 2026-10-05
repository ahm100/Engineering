using Engineering.Application.Services.ProcesVerbal.Contracts.CreateProcesVerbal;
using Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbalDetailById;
using Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbals;
using Engineering.Application.Services.ProcesVerbal.Contracts.RemoveProcesVerbal;

namespace Engineering.Application.Services.ProcesVerbal;

public interface IProcesVerbalLogic
{
    Task<Result<CreateProcesVerbalResponse>> CreateProcesVerbal(
        CreateProcesVerbalRequest request, CT ct);

    Task<Result<RemoveProcesVerbalResponse>> RemoveProcesVerbal(
        RemoveProcesVerbalRequest request, CT ct);

    Task<Result<GetProcesVerbalsResponse>> GetProcesVerbals(
        GetProcesVerbalsRequest request, CT ct);

    Task<Result<GetProcesVerbalDetailByIdResponse>> GetProcesVerbalDetailById(
        GetProcesVerbalDetailByIdRequest request, CT ct);
}
