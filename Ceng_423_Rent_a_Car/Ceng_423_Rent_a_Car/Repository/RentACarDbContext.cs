using Ceng_423_Rent_a_Car.Models;
using Microsoft.EntityFrameworkCore;

namespace Ceng_423_Rent_a_Car.Repository
{
	public class RentACarDbContext : DbContext
	{
		public DbSet<Driver>? Drivers { get; set; }
		public DbSet<Rent>? Rents { get; set; }
		public DbSet<Car>? Cars { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;");
		}
	}
}