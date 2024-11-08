using System.Data;
using SourceBaseBE.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using iSoft.DBLibrary.DBConnections.Interfaces;
using iSoft.Database.DBContexts;

namespace SourceBaseBE.Database.DBContexts
{
    public class CommonDBContext : BaseDBContext
    {
        // DB Set
        public virtual DbSet<Example001Entity> I_Example001s { get; set; }
        public virtual DbSet<Example002Entity> I_Example002s { get; set; }
        public virtual DbSet<Example003Entity> I_Example003s { get; set; }
        public virtual DbSet<Example001TransEntity> I_Example001Trans { get; set; }
        /*[GEN-43]*/

        public CommonDBContext()
        {
        }
        public CommonDBContext(IConfiguration configuration)
            : base(configuration)
        {
        }
        public CommonDBContext(IDBConnectionCustom dbConnectionCustom)
            : base(dbConnectionCustom)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Example001Entity>()
                        .HasMany(e => e.ListExample003)
                        .WithMany(e => e.ListExample001)
                        .UsingEntity<Dictionary<string, object>>(
                            "REF_Example001_Example003",
                            j => j
                                .HasOne<Example003Entity>()
                                .WithMany()
                                .HasForeignKey("Example003Id")
                                .OnDelete(DeleteBehavior.ClientSetNull),
                            j => j
                                .HasOne<Example001Entity>()
                                .WithMany()
                                .HasForeignKey("Example001Id")
                                .OnDelete(DeleteBehavior.ClientSetNull)
                        );
            /*[GEN-15]*/

            modelBuilder.Entity<Example001Entity>()
                        .HasOne(e => e.ItemExample002)
                        .WithMany(e => e.ListExample001)
                        .HasForeignKey(e => e.Example002Id)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Example001TransEntity>()
                        .HasOne(e => e.ItemBase)
                        .WithMany(e => e.ListExample001Trans)
                        .HasForeignKey(e => e.BaseId);
            /*[GEN-16]*/

            // CreatedAt
            modelBuilder.Entity<Example001Entity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<Example002Entity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<Example003Entity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<Example001TransEntity>().HasIndex(x => x.CreatedAt);
            /*[GEN-22]*/

            // UpdateAt
            modelBuilder.Entity<Example001Entity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<Example002Entity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<Example003Entity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<Example001TransEntity>().HasIndex(x => x.UpdatedAt);
            /*[GEN-23]*/

            // Order
            modelBuilder.Entity<Example001Entity>().HasIndex(x => x.Order);
            modelBuilder.Entity<Example002Entity>().HasIndex(x => x.Order);
            modelBuilder.Entity<Example003Entity>().HasIndex(x => x.Order);
            modelBuilder.Entity<Example001TransEntity>().HasIndex(x => x.Order);
            /*[GEN-24]*/

            // DeletedFlag
            modelBuilder.Entity<Example001Entity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<Example002Entity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<Example003Entity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<Example001TransEntity>().HasIndex(x => x.DeletedFlag);
            /*[GEN-25]*/


            base.OnModelCreating(modelBuilder);
        }

        public static async Task<bool> CreateDatabase(IDBConnectionCustom dbConnectionCustom)
        {
            using (var dbcontext = new CommonDBContext(dbConnectionCustom))
            {
                bool result = await dbcontext.Database.EnsureCreatedAsync();
                return result;
            }
        }

        public static async Task<bool> DeleteDatabase(IDBConnectionCustom dbConnectionCustom)
        {
            using (var dbcontext = new CommonDBContext(dbConnectionCustom))
            {
                bool result = await dbcontext.Database.EnsureDeletedAsync();
                return result;
            }
        }
    }
}
