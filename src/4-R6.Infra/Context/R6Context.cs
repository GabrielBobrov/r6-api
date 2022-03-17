using System;
using R6.Domain.Entities;
using R6.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using R6.Infra.Configuration;
using Npgsql;

namespace R6.Infra.Context
{
    public class R6Context : DbContext{

        static R6Context()
        {
            NpgsqlConnection.GlobalTypeMapper.AddGlobalTypeMappers();
        }
        public R6Context()
        { }

        public R6Context(DbContextOptions<R6Context> options) : base(options)
        { }

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuider)
         {
             optionsBuider.UseNpgsql("Server=127.0.0.1;Port=5432;Database=r6db;User Id=postgres;Password=gabriel123;Timeout=15;");
         }

        public virtual DbSet<Operator> Operators{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.AddPostgresEnums();
            builder.AddMappings();

            base.OnModelCreating(builder);
        }
    }
}