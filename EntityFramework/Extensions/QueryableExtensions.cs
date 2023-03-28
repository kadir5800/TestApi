using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereFullText<T>(this IQueryable<T> query, string property, string text)
        {
            return query.Where(q => EF.Functions.FreeText(property, text));
        }
    }
}
