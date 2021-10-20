using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        // get the connection string into constructor
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {
               
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // specify fluent api rules for your entities
            modelBuilder.Entity<Movie>(ConfigureMovie);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder) {
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            // we wanna EF to ignore Rating property and not to create the column
            builder.Ignore(m => m.Rating);
        }
        // make sure our entity classes are represented as DbSets
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies{ get; set; }
    }
}
