namespace Engineering.Application.Services.ContractorContracts.Contracts.GetCCThirdParties;

public record GetCCThirdPartiesResponse(
    List<GetCCThirdPartiesModel> Data,
    int RowCount);

public class GetCCThirdPartiesModel
{
    public long Id { get; set; }
    public long? ThirdPartyId { get; set; }
    public string? ThirdParty { get; set; }
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; }
    public long? ContractRequestNumber { get; set; }
    public string? Skill { get; set; }
    public string? Machineries { get; set; }
    public DateTime? StartDate { get; set; }
    public string? StartDateShamsi => StartDate?.ToShamsi();
    public DateTime? EndDate { get; set; }
    public string? EndDateShamsi => StartDate?.ToShamsi();
}

public class CCThirdPartiesPdfModel
{
    public string Number { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public string? Contractor { get; set; }
    public string? ContractNumber { get; set; }
    public string? Skill { get; set; }
    public string? Machineries { get; set; }
    public string? WorkStartDate { get; set; }
    public string? WorkEndDate { get; set; }
}