using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Domain.Entities.WbsTemplates;

[Description(WbsCmts.WbsTemplate)]
public class WbsTemplate : ActivateEntity<WbsTemplate, long>
{
    [Description(GlobalCmts.Title)]
    public string Title { get; private set; } = string.Empty;

    [Description(GlobalCmts.Code)]
    public string Code { get; private set; } = string.Empty;

    [Description(GlobalCmts.CompanyId)]
    public long CompanyId { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    public WbsTemplate(
        string title,
        string code,
        string? description,
        bool isActive,
        long companyId) : this()
    {
        SetTitle(title);
        SetCode(code);
        SetDescription(description);
        SetCompanyId(companyId);
        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    public void Update(
        string? title,
        string? code,
        string? description,
        bool? isActive)
    {
        SetTitle(title ?? Title);
        SetCode(code ?? Code);
        SetDescription(description ?? Description);
        if (isActive is not null && isActive.Value)
            SetActive();
        else if (isActive is not null && !isActive.Value)
            SetDeactivate();
    }

    public void SetTitle(string value)
    {
        Title = Guard.Against.NullOrEmpty(value, nameof(value));
    }

    public void SetCode(string value)
    {
        Code = Guard.Against.NullOrEmpty(value, nameof(value));
    }

    public void SetCompanyId(long value)
    {
        CompanyId = Guard.Against.Null(value, nameof(value));
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<ProjectWbs> _projectWbses;
    public IReadOnlyList<ProjectWbs> ProjectWbses => _projectWbses;
    private WbsTemplate()
    {
        _projectWbses = [];
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}