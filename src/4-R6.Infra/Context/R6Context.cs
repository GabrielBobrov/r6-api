using System;
using R6.Domain.Entities;
using R6.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace R6.Infra.Context
{
    public class R6Context : DbContext{
        public R6Context()
        { }

        public R6Context(DbContextOptions<R6Context> options) : base(options)
        {

        }

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuider)
         {
             optionsBuider.UseMySql("Server=localhost;Database=r6db;Uid=root;Pwd=", new MySqlServerVersion(new Version(8, 0, 11)));
         }

        public virtual DbSet<Operator> Operators{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new OperatorMap());
        }
    }
}