using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleanArchitecture.Domain
{
    public class JWT
    {
        public int id { get; set; }
        public string key { get; set; }
        public string issuer { get; set; }
        public string audience { get; set; }



    }
}
