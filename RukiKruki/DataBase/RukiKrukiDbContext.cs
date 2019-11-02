using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class RukiKrukiDbContext : DbContext
    {
        public RukiKrukiDbContext() : base("RukiKruki")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDllIsCopied = SqlProviderServices.Instance;
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Detail> Details { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<TO> TOs { get; set; }
        public virtual DbSet<TO_Detail> TO_Details { get; set; }
        public virtual DbSet<OrderTO> OrderTOs { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestDetail> RequestDetails { get; set; }
    }
}
