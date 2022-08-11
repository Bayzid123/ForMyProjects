using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PartnerManagement.Helper
{
    public class PagingList<T> : List<T>
    {
        public long CurrentPage { get; set; }
        public long TotalPage { get; set; }
        public long PageSize { get; set; }
        public long TotalCount { get; set; }

        public PagingList(List<T> items, long totalcount, long pageNumber, long pageSize)
        {
            TotalCount = totalcount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPage = (int)Math.Ceiling(totalcount / (double)pageSize);
            AddRange(items);
        }

        // &PageNo=1&PageSize=3&sortColumn=itemAttributeId&sort=2&searchColumn=itemAttributeName&search=size


        // With Sorted Column and Search
        public static PagingList<T> CreateAsync(IQueryable<T> source, long pageNumber, long pageSize, string sortColumn, string sort, string searchColumn, string search)
        {
            // Search Data Dynamically
            if (Is.AllowToSearchByColumn(searchColumn, search))
            {
                if (searchColumn.IsSeparatorExist())
                {
                    source = SearchForMulitpleColumn(source, searchColumn, search);
                }
                else
                {
                    source = SearchByDynamically(source, searchColumn, search);
                }
            }

            // Sort Data Dynamically
            if (Is.StringNotNullOrEmpty(sortColumn))
                source = OrderByDynamically(source, sortColumn, sort);

            return Offset(source, pageNumber, pageSize);
        }

        private static IQueryable<T> SearchForMulitpleColumn(IQueryable<T> source, string searchColumn, string search)
        {
            var columns = searchColumn.SplitParamWithSymble();
            var values = search.SplitParamWithSymble();

            // Assuming columns and values size are same. for empty value take 'null' as value.
            for (int i = 0; i < columns.Length; i++)
            {
                if (values[i] == "null")
                    continue;

                source = SearchByDynamically(source, columns[i], values[i]);
            }

            return source;
        }


        // With Sorted Column
        public static PagingList<T> CreateAsync(IQueryable<T> source, long pageNumber, long pageSize, string sortColumn, string sort)
        {
            // Sort Data Dynamically
            if (!string.IsNullOrWhiteSpace(sortColumn))
                source = OrderByDynamically(source, sortColumn, sort);

            return Offset(source, pageNumber, pageSize);
        }

        private static PagingList<T> Offset(IQueryable<T> source, long pageNumber, long pageSize)
        {
            var totalCount = source.Count();
            var items = source.Skip(((int)pageNumber - 1) * (int)pageSize).Take((int)pageSize).ToList();

            return new PagingList<T>(items, totalCount, pageNumber, pageSize);
        }

        public static PagingList<T> CreateAsync(IQueryable<T> source, long pageNumber, long pageSize)
        {
            return Offset(source, pageNumber, pageSize);
        }

        private static IQueryable<T> OrderByDynamically(IQueryable<T> source, string sortColumn, string sort)
        {
            return source.SortByDynamic(sortColumn, sort);
        }

        private static IQueryable<T> SearchByDynamically(IQueryable<T> source, string searchColumn, string search)
        {
            return source.WhereByColumn(searchColumn, search);
        }
    }
}
