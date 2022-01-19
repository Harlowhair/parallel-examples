using LongRuningAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LongRuningAPI.Data
{
    public class LRADataContext : DbContext
    {
        public LRADataContext(DbContextOptions<LRADataContext> options) : base(options)
        {
        }

        public DbSet<LongRequest> LongRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
