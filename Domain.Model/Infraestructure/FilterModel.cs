using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Infraestructure
{
    public class FilterModel
    {
        public string Column { get; set; }

        public FilterType Type { get; set; }

        public string FilterValue { get; set; }
    }
}
