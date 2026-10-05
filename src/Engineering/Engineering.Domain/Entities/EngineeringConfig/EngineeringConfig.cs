namespace Engineering.Domain.Entities.EngineeringConfig;

public class EngineeringConfig : ActivateEntity<EngineeringConfig, long>
{
    [Description(GlobalCmts.CompanyId)]
    public long CompanyId { get; private set; }
    [Description(EngineeringConfigCmts.SendTelegramMessage)]
    public bool SendTelegramMessage { get; private set; }

    [Description(EngineeringConfigCmts.ProjectThirdParties)]
    public bool ProjectThirdParties { get; private set; } = false;

    [Description(EngineeringConfigCmts.HaveCodingAlgorithm)]
    public bool HaveCodingAlgorithm { get; private set; } = false;

    public EngineeringConfig(long companyId,
        bool sendTelegramMessage,
        bool projectThirdParties,
        bool? isActive) : this()
    {
        SetCompanyId(companyId);
        SetSendTelegramMessage(sendTelegramMessage);
        SetProjectThirdParties(projectThirdParties);
        IsActive = isActive ?? false;
        AddHistory();
    }

    public void Update(bool sendTelegramMessage,
        bool projectThirdParties,
        bool? isActive)
    {
        SetSendTelegramMessage(sendTelegramMessage);
        SetProjectThirdParties(projectThirdParties);
        IsActive = isActive ?? IsActive;
        AddHistory();
    }

    public void SetCompanyId(long value)
    {
        CompanyId = Guard.Against.Null(value);
    }

    public void SetSendTelegramMessage(bool value)
    {
        SendTelegramMessage = Guard.Against.Null(value);
    }

    public void SetProjectThirdParties(bool value)
    {
        ProjectThirdParties = Guard.Against.Null(value);
    }

    public void AddHistory()
    {
        _histories.Add(new EngineeringConfigHistory(this));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<EngineeringConfigHistory> _histories;
    public IReadOnlyList<EngineeringConfigHistory> EngineeringConfigHistories => _histories;
    private List<EngineeringCodingConfig> _engineeringCodingConfigs;
    public IReadOnlyList<EngineeringCodingConfig> EngineeringCodingConfigs => _engineeringCodingConfigs;
    private EngineeringConfig()
    {
        _histories = [];
        _engineeringCodingConfigs = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}