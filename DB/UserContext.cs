using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DB
{
    public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");

            User user = new
            ()
            {
                Id = 1,
                UserName = "Administrator",
                PasswordHash = Encoding.UTF8.GetBytes("myERz/2cBeCVpiUrdyftFRRbMOFasrabKtXgzvpN7W0qYhnJHVvmeXh3WrOMrz+p17AJUfyIWCsHitQmi+o+yw=="),
                PasswordSalt = Encoding.UTF8.GetBytes("85PRl+y6QQa83/XkXOuUMiqO5gLNBQhlzQSISk3MJ2QlmsmhR4ezHjC7ztbv1rHmewCm3fcCUkkPB5yVUmj98DlIt2driV89CwjwPqQ8h4HWyPxqEWAVUUAGJlAWFHPXv9kGIMSUGkhmWbNk05FcPI9JE7G7oSMrvPXpU/lTByo="),
                Role = Role.Admin
            };

            modelBuilder.Entity<User>().HasData(user);
        }
    }
}
