using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace cleanArchitecture.Infraestructure
{
    public interface IApplicationContext
    {


        DbSet<Author> Authors { get; set; }
       DbSet<Genre> Genres { get; set; }
       DbSet<User> Users { get; set; }
        DbSet<Book> Books { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));


    }
}
