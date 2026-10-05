using Engineering.Domain.Entities.EngineeringConfig.Enum;

namespace Engineering.Domain.Entities.EngineeringConfig;

public class EngineeringCodingConfig : ActivateEntity<EngineeringCodingConfig, long>
{
    [Description(EngineeringConfigCmts.EngineeringConfig)]
    public EngineeringConfig EngineeringConfig { get; private set; }
    public long EngineeringConfigId { get; private set; }
    [Description(EngineeringConfigCmts.CodingAlgorithmType)]
    public CodingAlgorithmType Type { get; private set; }
    [Description(EngineeringConfigCmts.SendTelegramMessage)]
    public string Prefix { get; private set; } = string.Empty;

    public EngineeringCodingConfig(
        EngineeringConfig config,
        CodingAlgorithmType type,
        string prefix,
        bool isActive) : this()
    {
        SetEngineeringConfig(config);
        SetCodingAlgorithmType(type);
        SetPrefix(prefix);
        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    public void Update(
        EngineeringConfig config,
        CodingAlgorithmType type,
        string prefix,
        bool? isActive)
    {
        SetEngineeringConfig(config);
        SetCodingAlgorithmType(type);
        SetPrefix(prefix);
        if (isActive is not null && isActive.Value)
            SetActive();
        else if (isActive is not null && !isActive.Value)
            SetDeactivate();
    }

    public void SetEngineeringConfig(EngineeringConfig value)
    {
        EngineeringConfig = Guard.Against.Null(value, nameof(value));
        EngineeringConfigId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetCodingAlgorithmType(CodingAlgorithmType value)
    {
        Type = Guard.Against.Null(value);
    }

    public void SetPrefix(string value)
    {
        Prefix = Guard.Against.Null(value);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EngineeringCodingConfig() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}