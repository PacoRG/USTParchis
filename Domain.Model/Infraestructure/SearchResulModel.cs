using System.Collections.Generic;

namespace Domain.Model.Infraestructure
{
    public class SearchResultModel<T> where T: class
    {
        public int TotalRecords { get; set; }

        public ICollection<T> Records { get; set; }
    }
}
