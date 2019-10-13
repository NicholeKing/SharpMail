using Microsoft.EntityFrameworkCore;

namespace csharp_email.Models
{
	public class MyContext : DbContext
	{
		public MyContext (DbContextOptions options) : base(options) {}

		public DbSet<User> Users {get;set;}
		public DbSet<Mail> Emails {get;set;}
	}
}