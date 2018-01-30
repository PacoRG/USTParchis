using System.Collections.Generic;

namespace Domain.Model.Infraestructure
{
    public class SearchModel
    {
        public SearchModel()
        {
            this.Filters = new List<FilterModel>();
        }

        public int? PageIndex { get; set; }

        public int? RecordsPerPage { get; set; }

        public string SortColumn { get; set; }

        public bool? IsAscendingSort { get; set; }

        public List<FilterModel> Filters {get;}
    }
}
