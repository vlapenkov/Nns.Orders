using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBS.Employees.Dto
{
    public abstract class BaseFilter
    {

        /// <summary>
        /// Номер страницы
        /// </summary>
        public int? PageNumber { get; set; } =1;

        /// <summary>
        /// Количество элементов на странице
        /// </summary>
        public int? PageSize { get; set; } = 30;
    }
}
