using Shared.Domain.Abstractions;

namespace Shared.Misc;

public static class Extensions {
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, IPageDesc pageDesc) {
        return query
            .Skip((pageDesc.PageNumber - 1) * pageDesc.PageSize)
            .Take(pageDesc.PageSize);
    }
}
