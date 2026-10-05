namespace Engineering.Domain.Entities.EngineeringConfig;

public class EngineeringConfigHistory : ActivateEntity<EngineeringConfigHistory, long>
{
    [Description(EngineeringConfigCmts.SendTelegramMessage)]
    public bool SendTelegramMessage { get; private set; }

    [Description(EngineeringConfigCmts.ProjectThirdParties)]
    public bool ProjectThirdParties { get; private set; } = false;

    public long EngineeringConfigId { get; private set; }
    public EngineeringConfig EngineeringConfig { get; private set; }

    public EngineeringConfigHistory(EngineeringConfig engineerConfig) : this()
    {
        SetEngineeringConfig(engineerConfig);
        SetSendTelegramMessage(engineerConfig.SendTelegramMessage);
        SetProjectThirdParties(engineerConfig.ProjectThirdParties);
        if (engineerConfig.IsActive)
            SetActive();
        else
            SetDeactivate();
    }

    public void SetSendTelegramMessage(bool value)
    {
        SendTelegramMessage = Guard.Against.Null(value);
    }

    public void SetProjectThirdParties(bool value)
    {
        ProjectThirdParties = Guard.Against.Null(value);
    }

    public void SetEngineeringConfig(EngineeringConfig value)
    {
        EngineeringConfig = Guard.Against.Null(value, nameof(value));
        EngineeringConfigId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EngineeringConfigHistory()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}