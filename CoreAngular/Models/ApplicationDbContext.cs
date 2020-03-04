using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAngular.Models
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _conf;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration conf)
            : base(options)
        {
            _conf = conf;
        }

        public ApplicationDbContext(IConfiguration conf)
        {
            _conf = conf;
        }


        public DbSet<Persona> Persona { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(_conf.GetValue<string>("schema"));
        }

        }
    }
