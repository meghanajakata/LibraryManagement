using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;

namespace LibraryManagement.Data
{
    public class LibraryManagementContext : DbContext
    {
        public LibraryManagementContext (DbContextOptions<LibraryManagementContext> options)
            : base(options)
        {
        }

        public DbSet<LibraryManagement.Models.Book> Books { get; set; } = default!;
        public DbSet<LibraryManagement.Models.UserInfo> UserInfos { get; set; } = default!;
    }
}
