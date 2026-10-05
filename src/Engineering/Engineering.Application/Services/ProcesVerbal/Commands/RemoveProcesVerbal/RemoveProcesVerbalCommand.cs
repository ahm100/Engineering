using Engineering.Application.Services.ProcesVerbal.Contracts.RemoveProcesVerbal;

namespace Engineering.Application.Services.ProcesVerbal.Commands.RemoveProcesVerbal;

public record RemoveProcesVerbalCommand(
    long Id) : ICommand<RemoveProcesVerbalResponse?>;