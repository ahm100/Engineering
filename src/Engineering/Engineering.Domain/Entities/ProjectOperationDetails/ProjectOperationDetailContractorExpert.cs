using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Domain.Entities.ProjectOperationDetails;

public class ProjectOperationDetailContractorExpert : ActivateEntity<ProjectOperationDetailContractorExpert, long>
{
    [Description(GlobalCmts.Volume)]
    public decimal Volume { get; private set; }

    [Description(ProjectDetailCmts.HaveContract)]
    public bool HaveContract { get; private set; } = false;

    [Description(ProjectDetailCmts.ConsumableVolumeExpert)]
    public long ConsumableVolumeExpertId { get; set; }
    public ConsumableVolumeExpert ConsumableVolumeExpert { get; set; }

    [Description(ProjectDetailCmts.ProjectOperationDetailContractorService)]
    public long ProjectOperationDetailContractorServiceId { get; private set; }
    public ProjectOperationDetailContractorService ProjectOperationDetailContractorService { get; private set; }

    public ProjectOperationDetailContractorExpert(
        ProjectOperationDetailContractorService projectOperationDetailContractorService,
        ConsumableVolumeExpert consumableVolumeExpert,
        decimal volume,
        bool isActive) : this()
    {
        SetProjectOperationDetailContractorService(projectOperationDetailContractorService);
        SetConsumableVolumeExpert(consumableVolumeExpert);
        SetVolume(volume);
        SetHaveContract(false);
        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    public void Update(
        ProjectOperationDetailContractorService? projectOperationDetailContractorService,
        ConsumableVolumeExpert? consumableVolumeExpert,
        decimal? volume,
        bool? isActive)
    {
        SetProjectOperationDetailContractorService(projectOperationDetailContractorService ?? ProjectOperationDetailContractorService);
        SetConsumableVolumeExpert(consumableVolumeExpert ?? ConsumableVolumeExpert);
        SetVolume(volume ?? Volume);
        if (isActive == true)
            SetActive();
        else if (isActive == false)
            SetDeactivate();
    }

    public void SetProjectOperationDetailContractorService(ProjectOperationDetailContractorService value)
    {
        ProjectOperationDetailContractorService = Guard.Against.Null(value, nameof(value));
        ProjectOperationDetailContractorServiceId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetConsumableVolumeExpert(ConsumableVolumeExpert value)
    {
        ConsumableVolumeExpert = Guard.Against.Null(value, nameof(value));
        ConsumableVolumeExpertId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetVolume(decimal value)
    {
        Volume = Guard.Against.Null(value, nameof(value));
    }

    public void SetHaveContract(bool value)
    {
        HaveContract = Guard.Against.Null(value, nameof(value));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectOperationDetailContractorExpert() { }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}