using Library.Common.Enums;
using Library.Data.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Data
{
    public class LibraryContext : IdentityDbContext
    {

        public DbSet<ActiveHolderEntityModel> ActiveHolders { get; set; }
        public DbSet<BookEntityModel> Books { get; set; }
        public DbSet<KeyWordEntityModel> KeyWords  { get; set; }
        public DbSet<NotificationEntityModel> Notifications { get; set; }
        public DbSet<StatusLogEntityModel> StatusLogs { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            :base(options)
        {
            Database.EnsureCreated();



            if(Roles.Count() == 0)
            {

                Roles.AddRange(
                    new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Name = "User", NormalizedName = "USER" }
                    );

            }

        }

    }
}
