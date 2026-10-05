using Gita.Backend.Shared.Domain.Extensions;
using Gita.Backend.Shared.Domain.Shared.Models;

namespace Engineering.Api.Extensions.Enums;

public static class EnumExtensions
{
    public static GetEnumsResponse GetEnums<TEnum>(
        GetEnumsRequest request,
        IReadOnlyCollection<int>? excludedCodes = null)
        where TEnum : Enum
    {
        IQueryable<EnumObject> query =
            EnumExt.GetEnumObjectList<TEnum>().AsQueryable();

        if (excludedCodes?.Count > 0)
            query = query.Where(oo => !excludedCodes.Contains(oo.Code));

        // فیلتر بر اساس کدها
        if (request.Codes?.Any() == true)
        {
            query = request.Anti
                ? query.Where(x => !request.Codes.Contains(x.Code))
                : query.Where(x => request.Codes.Contains(x.Code));
        }

        // فیلتر بر اساس متن
        if (!string.IsNullOrWhiteSpace(request.FilterData))
        {
            var filter = request.FilterData.Trim();

            query = query.Where(x =>
                x.Description.Contains(filter, StringComparison.OrdinalIgnoreCase));
        }

        var rowCount = query.Count();

        // صفحه بندی
        if (request.PageIndex > 0 && request.PageSize > 0)
        {
            query = query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        return new GetEnumsResponse(
            query.ToList(),
            rowCount);
    }
}
