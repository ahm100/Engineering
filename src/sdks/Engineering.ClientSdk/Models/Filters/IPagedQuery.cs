using Gita.Backend.Shared.Domain.Models.Schema;

namespace Engineering.ClientSdk.Models.Filters;

public interface ILegacyPagedQuery : IPagedQuery
{
    int PageNumber { get; }
    int PageSize { get; }
}
