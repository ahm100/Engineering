using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Domain.Entities.TelegramChats;

[Description(TelegramChatCmts.TelegramChat)]
public class TelegramChat : AuditableEntity<TelegramChat>
{
    [Description(GlobalCmts.Title)]
    public string? ChatName { get; private set; } = string.Empty;

    [Description(TelegramChatCmts.ChatUrl)]
    public string ChatUrl { get; private set; }

    [Description(TelegramChatCmts.ChatId)]
    public string ChatId { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; } = string.Empty;

    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; } = true;

    [Description(GlobalCmts.CostCenter)]
    public long CostCenterId { get; private set; }
    public CostCenter CostCenter { get; private set; }

    [Description(GlobalCmts.Project)]
    public long? ProjectId { get; private set; }
    public Project? Project { get; private set; }


    public TelegramChat(
        CostCenter costCenter,
        Project? project,
        string? chatName,
        string chatUrl,
        string chatId,
        string? description,
        bool isActive,
        List<TelegramMessageType>? types
    ) : this()
    {
        SetCostCenter(costCenter);
        SetProject(project);
        SetChatName(chatName);
        SetChatUrl(chatUrl);
        SetChatId(chatId);
        SetDescription(description);
        IsActive = isActive;
        AddTelegramChatTypes(types, chatId, chatUrl, chatName);
    }

    public static TelegramChat Create(
        CostCenter costCenter,
        Project? project,
        string? chatName,
        string chatUrl,
        string chatId,
        string? description,
        bool isActive,
        List<TelegramMessageType>? types)
    {
        return new TelegramChat(costCenter, project, chatName, chatUrl, chatId, description, isActive, types);
    }

    #region Set data

    public void SetCostCenter(CostCenter value)
    {
        CostCenter = Guard.Against.Null(value, nameof(value));
        CostCenterId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetProject(Project? value)
    {
        Project = value;
        ProjectId = value?.Id;
    }

    public void SetChatName(string? value)
    {
        ChatName = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetChatUrl(string value)
    {
        ChatUrl = value;
    }

    public void SetChatId(string value)
    {
        ChatId = value;
    }

    public void SetActive()
    {
        IsActive = true;
    }

    public void SetInActive()
    {
        IsActive = false;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }


    public void AddTelegramChatTypes(List<TelegramMessageType>? types, string chatId, string chatUrl, string? chatName)
    {
        _telegramChatTypes.ForEach(c => c.SetIsDeleted());

        if (types is { Count: > 0 })
        {
            foreach (var type in types)
            {
                _telegramChatTypes.Add(TelegramChatType.Create(this, type, chatId, chatUrl, chatName));
            }
        }
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<TelegramChatType> _telegramChatTypes;
    public IReadOnlyList<TelegramChatType> TelegramChatTypes => _telegramChatTypes;

    private List<TelegramMessageHistory> _TelegramMessageHistries;
    public IReadOnlyList<TelegramMessageHistory> TelegramMessageHistorys => _TelegramMessageHistries;

    private TelegramChat()
    {
        _telegramChatTypes = [];
        _TelegramMessageHistries = new List<TelegramMessageHistory>();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}