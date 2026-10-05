using Engineering.Domain.Entities.ProcesVerbal.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractForProcesVerbal;

public class GetContractForProcesVerbalResponse
{
    public long Id { get; set; }
    public long? CNumber { get; set; }
    public string TitleFa { get; set; } = string.Empty;
    public bool IsTempDelivered { get; set; }
    public List<ProcesVerbalType>? ExcludedProcesVerbalTypes => !IsTempDelivered ? [ProcesVerbalType.FinalDelivery] : null;
}