using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleanArchitecture.Infraestructure.Dto
{
    public class AuthorDto
    {
        public string Name { get; set; } = null!;
        public string Country { get; set; }
        public DateTime Date { get; set; }


    }
}
