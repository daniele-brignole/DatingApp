using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        //DbcontextOptions contiene opzioni sulla connessione al Db, come la stringa di connessione
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> Users{get;set;}
    }
}