using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataTables.AspNet.Core;
using Excalibur.Avalon.Extensions;

namespace Excalibur.AspNetCore.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> GenericSort<T>(this IQueryable<T> query, IDataTablesRequest request)
        {
            if (request.Columns.Any(x => x.Sort != null))
            {
                var column = request.Columns.First(x => x.Sort != null);
                var direction = column.Sort.Direction.ToString("G") == "Descending" ? "OrderByDescending" : "OrderBy";

                query = query.OrderBy(direction, column.Name);
            }
            return query;
        }
    }
}
