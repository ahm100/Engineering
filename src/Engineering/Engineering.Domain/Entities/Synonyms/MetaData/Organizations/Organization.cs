namespace Engineering.Domain.Entities.Synonyms.MetaData.Organizations;

public class ViewOrganization : ActivateEntity<ViewOrganization>
{
    public string Code { get; private set; } = default!;
    public string NameFa { get; private set; } = default!;
    public string? NameEn { get; private set; }
    public string? Description { get; private set; }
    public string? Address { get; private set; }
    public bool IsDocumentAllowed { get; private set; } = default!;
    public string RootPath { get; private set; } = default!;
    public long OrganizationTypeId { get; private set; } = default!;
    public Guid PreferentialReferenceCode { get; private set; }
    public long? CompanyId { get; private set; } = default!;
    public long? SubSystemId { get; set; }
    public long? ManagerId { get; set; }

    private ViewOrganization()
    {
    }
}