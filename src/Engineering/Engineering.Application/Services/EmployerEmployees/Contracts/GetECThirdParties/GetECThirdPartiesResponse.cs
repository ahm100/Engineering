namespace Engineering.Application.Services.EmployerEmployees.Contracts.GetECThirdParties;

public record GetECThirdPartiesResponse(
    List<GetECThirdPartiesModel> Data,
    int RowCount);

public class GetECThirdPartiesModel
{
    public long Id { get; set; }
    public long? ThirdPartyId { get; set; }
    public string? ThirdParty { get; set; }
    public long? EmployerId { get; set; }
    public string? Employer { get; set; }
    public string? EContractHeadCode { get; set; }
    public string? Skill { get; set; }
    public DateTime StartDate { get; set; }
    public string StartDateShamsi => StartDate.ToShamsi();
    public DateTime EndDate { get; set; }
    public string EndDateShamsi => StartDate.ToShamsi();
}

public class ECThirdPartiesPdfModel
{
    public string Number { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public string? Employer { get; set; }
    public string? ContractNumber { get; set; }
    public string? Skill { get; set; }
    public string? WorkStartDate { get; set; }
    public string? WorkEndDate { get; set; }
}