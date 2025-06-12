﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleanArchitecture.Domain
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int ReleaseYear { get; set; }
        public string ImageUrl { get; set; }
        public int Stock { get; set; }
        public String ISBN { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }

        public Genre Genre { get; set; }
        public int GenreId { get; set; }



    }
}
