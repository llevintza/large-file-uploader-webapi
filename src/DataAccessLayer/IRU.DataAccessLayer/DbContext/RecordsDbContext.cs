using System;
using System.Collections.Generic;
using System.Text;

using IRU.DataAccessLayer.Entities;

using Microsoft.EntityFrameworkCore;

namespace IRU.DataAccessLayer.DbContext
{
    public class RecordsDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public RecordsDbContext(DbContextOptions<RecordsDbContext> options) : base(options)
        {
            
        }

        public DbSet<RecordEntity> Records { get; set; }
    }
}
