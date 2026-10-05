namespace Engineering.Application.Extensions.Pagination;

public static class Pagination
{
    public static List<TSource> SetPaging<TSource>(this List<TSource> source, int pageIndex, int pageSize)
    {
        if (pageIndex < 0)
            pageIndex = 0;
        pageSize = pageSize == 0 ? int.MaxValue : pageSize;
        var result = source.Skip(pageIndex * pageSize).Take(pageSize);
        return result.ToList();
    }
}
