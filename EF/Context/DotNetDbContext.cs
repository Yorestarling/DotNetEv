using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Context;

public class DotNetDbContext(DbContextOptions<DotNetDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
