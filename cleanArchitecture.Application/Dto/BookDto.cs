using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleanArchitecture.Domain;

namespace cleanArchitecture.Application.Dto
{
    public class BookDto
    {

        public string Title { get; set; }
        public string Summary { get; set; }
        public int ReleaseYear { get; set; }
        public string ImageUrl { get; set; }
        public int Stock { get; set; }
        public String ISBN { get; set; }
        public int AuthorId { get; set; }

        public int GenreId { get; set; }

    }
}
