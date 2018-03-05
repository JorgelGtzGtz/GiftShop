using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GiftShop.Web.Infrastructure
{
    public static class ExtentionsHelper
    {
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static PaginationSet<T> ToPagedList<T>(this IEnumerable<T> items, int currentPage, int totalRecords, int currentPageSize)
        {
            PaginationSet<T> pagedSet = new PaginationSet<T>()
            {
                Page = currentPage,
                TotalCount = totalRecords,
                TotalPages = (int)Math.Ceiling((decimal)totalRecords / currentPageSize),
                Items = items.Skip(currentPage * currentPageSize).Take(currentPageSize)
            };

            return pagedSet;
        }
        public static PaginationSet<DataRow> ToPagedList(this DataTable table, int currentPage, int totalRecords, int currentPageSize)
        {
            PaginationSet<DataRow> pagedSet = new PaginationSet<DataRow>()
            {
                Page = currentPage,
                TotalCount = totalRecords,
                TotalPages = (int)Math.Ceiling((decimal)totalRecords / currentPageSize),
                Items = table.AsEnumerable().Skip(currentPage * currentPageSize).Take(currentPageSize)
            };

            return pagedSet;
        }
        public static void SortList<T>(this List<T> list, string columnName, bool direction)
        {
            var property = typeof(T).GetProperty(columnName);
            var multiplier = direction ? -1 : 1;
            list.Sort((t1, t2) => {
                var col1 = property.GetValue(t1);
                var col2 = property.GetValue(t2);
                return multiplier * Comparer<object>.Default.Compare(col1, col2);
            });
        }
    }
}