namespace Engineering.Domain.Entities.ServiceInfos;

[Description(GlobalCmts.Document)]
public class ServiceInfoDocument : AuditableEntity<ServiceInfoDocument>
{
    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(GlobalCmts.ServiceInfo)]
    public long ServiceInfoId { get; private set; }
    public ServiceInfo ServiceInfo { get; private set; }

    public ServiceInfoDocument(string url,
        ServiceInfo serviceInfo) : this()
    {
        SetUrl(url);
        SetServiceInfo(serviceInfo);
    }

    public static ServiceInfoDocument Create(string url, ServiceInfo serviceInfo)
    {
        return new ServiceInfoDocument(url, serviceInfo);
    }

    public void SetIsDeleted()
    {
        this.IsDeleted = true;
    }

    public void SetUrl(string value)
    {
        Url = Guard.Against.NullOrEmpty(value, nameof(value));
    }

    public void SetServiceInfo(ServiceInfo value)
    {
        ServiceInfo = Guard.Against.Null(value, nameof(value));
        ServiceInfoId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ServiceInfoDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}