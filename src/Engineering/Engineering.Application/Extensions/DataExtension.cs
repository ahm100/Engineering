using Gita.Backend.Shared.Domain.Models;
using Gita.Backend.Shared.Domain.Models.Schema;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Engineering.Application.Extensions
{

    public static class DataExtensions
    {
        public static Task<PagedResultSet<T>> ToPagedResultSet<T>(this IQueryable<T> query, IPagedQuery parameters,
            CT cancellationToken)
        {
            return query.ToPagedResultSet(
                parameters.Page,
                parameters.PageSize,
                cancellationToken
            );
        }

        public static Task<PagedResultSet<T>> ToPagedResultSet<T>(this IQueryable<T> query, INullablePagedQuery parameters,
            CT cancellationToken)
        {
            return query.ToPagedResultSet(
                parameters.Page + 1,
                parameters.PageSize,
                cancellationToken
            );
        }

        public static async Task<PagedResultSet<T>> ToPagedResultSet<T>(
            this IQueryable<T> query,
            int? pageNumber,
            int? pageSize,
            CT cancellationToken)
        {
            var items = await query
                .ApplyPagingOrAll(pageNumber, pageSize)
                .ToListAsync(cancellationToken);

            // We could omit an extra count query when we're querying with no page size (all rows)
            var totalCount = pageNumber is null && pageSize is null
                ? items.Count
                : await query.CountAsync(cancellationToken);

            return new PagedResultSet<T>(
                items,
                totalCount,
                pageNumber == 0 ? 0 : (pageNumber ?? 1) - 1,
                pageSize ?? items.Count
            );
        }

        public static IQueryable<T> ApplyPaging<T>(
            this IQueryable<T> queryable,
            int? pageNumber,
            int? pageSize
        )
        {
            var pageIndex = (pageNumber ?? 1) - 1;
            var pgSize = pageSize ?? 10;

            return queryable
                .Skip(pageIndex * pgSize)
                .Take(pgSize);
        }

        public static IQueryable<T> ApplyPagingOrAll<T>(
            this IQueryable<T> queryable,
            int? pageNumber,
            int? pageSize
        )
        {
            if (pageNumber is not null && pageNumber > 0 && pageSize is not null && pageSize > 0)
            {
                return queryable.ApplyPaging(pageNumber, pageSize);
            }

            return queryable;
        }

        public static bool ShouldUseEntireDay(this DateTime dateTime, out DateTime date)
        {
            if (dateTime.Date == dateTime)
            {
                date = dateTime.AddDays(1);
                return true;
            }

            date = dateTime;
            return false;
        }

        public static (bool Success, decimal Value, string Error) ValidateDecimal(string input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
                return (false, 0, $"{fieldName} نمی‌تواند خالی باشد");

            string cleanedInput = input
                .Replace(",", "")
                .Replace(" ", "")
                .Replace("٬", "")
                .Trim();

            if (!Regex.IsMatch(cleanedInput, @"^-?\d+(\.\d+)?$"))
                return (false, 0, $"{fieldName} باید عددی باشد");

            if (!decimal.TryParse(cleanedInput, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
                return (false, 0, $"{fieldName} معتبر نیست");

            if (result < 0)
                return (false, 0, $"{fieldName} نمی‌تواند منفی باشد");

            return (true, result, null);
        }

        public static (bool Success, int Value, string Error) ValidateInteger(string input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
                return (false, 0, $"{fieldName} نمی‌تواند خالی باشد");

            string cleanedInput = Regex.Replace(input, @"[^\d]", "");

            if (string.IsNullOrEmpty(cleanedInput))
                return (false, 0, $"{fieldName} باید عددی باشد");

            if (!int.TryParse(cleanedInput, out int result))
                return (false, 0, $"{fieldName} معتبر نیست");

            if (result <= 0)
                return (false, 0, $"{fieldName} باید بزرگتر از صفر باشد");

            return (true, result, null);
        }
    }
}
