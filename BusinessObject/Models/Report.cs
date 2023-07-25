using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Report<TValueType>
        where TValueType : struct
    {
        public int Month { get; set; }
        public TValueType MonthValue { get; set; }
    }
    public class ReportResponse<TValueType>
        where TValueType : struct
    {
        public ReportResponse()
        {
            Data = Enumerable.Empty<Report<TValueType>>();
        }
        public IEnumerable<Report<TValueType>> Data { get; set; }
    }
}
