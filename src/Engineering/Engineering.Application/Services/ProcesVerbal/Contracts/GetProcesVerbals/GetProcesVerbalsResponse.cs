using Engineering.Domain.Entities.ProcesVerbal.Enums;

namespace Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbals;

public record GetProcesVerbalsResponse(
    List<GetProcesVerbalsResponseModel> Data,
    int RowCount);

public class GetProcesVerbalsResponseModel
{
    public GetProcesVerbalsResponseModel(
        long id,
        string titleFa,
        string? titleEn,
        string? code,
        ProcesVerbalType type,
        string typeDescription,
        long projectId,
        string? projectName,
        long contractId,
        long? contractNum,
        DateTime recordDateTime)
    {
        Id = id;
        TitleFa = titleFa;
        TitleEn = titleEn;
        Code = code;
        Type = type;
        TypeDescription = typeDescription;
        ProjectId = projectId;
        ProjectName = projectName;
        ContractId = contractId;
        ContractNum = contractNum;
        RecordDateTime = recordDateTime;
    }

    public long Id { get; set; }
    public string TitleFa { get; set; }
    public string? TitleEn { get; set; }
    public string? Code { get; set; }
    public ProcesVerbalType Type { get; set; }
    public string TypeDescription { get; set; }
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public long ContractId { get; set; }
    public long? ContractNum { get; set; }
    public DateTime RecordDateTime { get; set; }
}