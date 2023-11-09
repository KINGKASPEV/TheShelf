using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheShelf.Model.Dtos
{
    public class SearchResultDto
    {
        public string SearchCriteria { get; set; }
        public List<BookReturnDto> Books { get; set; }
    }
}
