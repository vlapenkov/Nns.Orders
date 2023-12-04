using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Common.PagedList
{
    public class PagedList<T>
    {

        public static readonly int MaxNumber = 50;
        /// <summary>
        /// Номер страницы
        /// </summary>
        public int PageNumber { get; set; } = default!;

        /// <summary>
        /// Количество элементов на странице
        /// </summary>
        public int? PageSize { get; set; }

        public long Count { get; set; }

        public List<T> Items { get; init; } = new List<T>();
    }
}
