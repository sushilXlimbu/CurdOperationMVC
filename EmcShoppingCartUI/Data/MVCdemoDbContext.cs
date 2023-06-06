using EmcShoppingCartUI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmcShoppingCartUI.Data
{
    public class MVCdemoDbContext : DbContext
    {
        public MVCdemoDbContext(DbContextOptions options): base(options)
        {

        }
        //this property is used to access the tables form DB
        public DbSet<Employee> Employees { get; set; }
    }


}
