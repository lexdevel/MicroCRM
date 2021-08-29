using Microsoft.EntityFrameworkCore;
using MicroCRM.Entities;

namespace MicroCRM.Data
{
    public class DataContext : DbContext
    {
        #region Public properties

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderLine> OrderLines { get; set; }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContextOptions">The database context options.</param>
        public DataContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }
    }
}
