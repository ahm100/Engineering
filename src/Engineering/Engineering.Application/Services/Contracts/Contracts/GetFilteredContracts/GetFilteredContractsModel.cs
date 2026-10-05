using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.GetFilteredContracts;

public class GetFilteredContractsModel : IUserAuditable
{
    public long Id { get; set; }
    public long? ContractNumber { get; set; }

    public string FaTitle { get; set; } = string.Empty;
    public string? EnTitle { get; set; }
    public string? Description { get; set; }

    public long ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; }

    public long ContractPartyId { get; set; }
    public string? ContractPartyName { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int Duration { get; set; }
    public ContractDurationUnit DurationUnit { get; set; }
    public string DurationUnitTitle => DurationUnit.GetEnumDescription();

    public ContractStatus Status { get; set; }
    public string StatusTitle => Status.GetEnumDescription();

    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
    public long CreatorId { get; set; }
    public string? Creator { get; set; }

    public DateTime? Updated { get; set; }
    public string? UpdatedShamsi => Updated?.ToShamsi();
    public long? UpdaterId { get; set; }
    public string? Updater { get; set; }
}
