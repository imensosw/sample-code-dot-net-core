using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Memento.Models.DB
{
    public partial class MementoContext : DbContext
    {
        public virtual DbSet<LoginModel> UserRegistration { get; set; }
        
        public MementoContext(DbContextOptions<MementoContext> options) : base(options)
        {

        }

        
    }
}
 
