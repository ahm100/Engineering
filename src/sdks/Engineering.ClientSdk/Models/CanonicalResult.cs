using Engineering.ClientSdk.Models.Schema;
using Gita.Backend.Shared.Application.Middlewares.UnhandledExceptions;
using Gita.Backend.Shared.Domain.Models.Schema;

namespace Engineering.ClientSdk.Models;

public class EngineeringCanonicalResult<T> : IEngineeringCanonicalResult<T>
{
    public T? Value { get; set; }

    public bool? IsSuccess { get; set; }

    public bool? IsFailure { get; set; }

    public ErrorModel? Error { get; set; }

    public int? PageNumber { get; set; }

    public int? PageIndex
    {
        get => PageNumber;
        set => PageNumber = value;
    }

    public int? PageSize { get; set; }

    public static EngineeringCanonicalResult<T> FromValue(T? value, object? request = null)
    {
        int? pageNumber = null;
        int? pageSize = null;

        if (request is Gita.Backend.Shared.Domain.Models.Schema.ILegacyPagedQuery pagedQuery)
        {
            pageNumber = pagedQuery.PageNumber;
            pageSize = pagedQuery.PageSize;
        }

        if (request is IPagedQuery pagedQuery1)
        {
            pageNumber = pagedQuery1.Page + 1;
            pageSize = pagedQuery1.PageSize;
        }

        if (request is INullablePagedQuery pagedQuery2)
        {
            pageNumber = pagedQuery2.Page + 1;
            pageSize = pagedQuery2.PageSize;
        }

        return new EngineeringCanonicalResult<T>
        {
            IsSuccess = true,
            IsFailure = false,
            Error = null,
            Value = value,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
    }
}
