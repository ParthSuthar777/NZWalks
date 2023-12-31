using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.Data
{
    public class NZWalksAuthDBContext : IdentityDbContext
    {
        public NZWalksAuthDBContext(DbContextOptions<NZWalksAuthDBContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id="aecd99b5-0dcd-438f-8c73-0bf11a9510a0",
                    ConcurrencyStamp="aecd99b5-0dcd-438f-8c73-0bf11a9510a0",
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                 new IdentityRole
                {
                    Id="57408be9-a8ee-4ac9-a19a-2aef8383361e",
                    ConcurrencyStamp="57408be9-a8ee-4ac9-a19a-2aef8383361e",
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
