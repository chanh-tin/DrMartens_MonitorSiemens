using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using iSoft.DBLibrary.DBConnections.Interfaces;
using SourceBaseBE.Database.Entities;
using SourceBaseBE.Database.Enums;

namespace SourceBaseBE.Database.DBContexts
{
    public class CommonDBContext : DbContext
    {
        public IDBConnectionCustom dbConnectionCustom;
        protected IConfiguration Configuration { get; set; }
        public virtual DbSet<MasterDataOptionFilterEntity> MasterDataOptionFilters { get; set; }
        public virtual DbSet<FactoryEntity> Factorys { get; set; }
        public virtual DbSet<WorkshopEntity> Workshops { get; set; }
        public virtual DbSet<ShiftEntity> Shifts { get; set; }
        public virtual DbSet<BreakTimeEntity> BreakTimes { get; set; }
        public virtual DbSet<MachineLossDescribeEntity> MachineLossDescribes { get; set; }
        public virtual DbSet<MachineLossPositionEntity> MachineLossPositions { get; set; }
        public virtual DbSet<MachineEntity> Machines { get; set; }
        public virtual DbSet<LineEntity> Lines { get; set; }
        public virtual DbSet<OeePointConfigEntity> OeePointConfigs { get; set; }
        public virtual DbSet<OeePointEntity> OeePoints { get; set; }
        public virtual DbSet<MachineLossGroupEntity> MachineLossGroups { get; set; }
        public virtual DbSet<MachineBlockDataEntity> MachineBlockDatas { get; set; }
        public virtual DbSet<MachineLossEntity> MachineLosses { get; set; }
        public virtual DbSet<TimezoneEntity> Timezones { get; set; }
        public virtual DbSet<LanguageEntity> Languages { get; set; }
        public virtual DbSet<CountryEntity> Countries { get; set; }
        public virtual DbSet<TranslateLanguageEntity> TranslateLanguages { get; set; }
        public virtual DbSet<Example003Entity> Example003s { get; set; }
        public virtual DbSet<Example002Entity> Example002s { get; set; }
        public virtual DbSet<PermissionDetailEntity> PermissionDetails { get; set; }
        public virtual DbSet<UserGroupEntity> UserGroups { get; set; }
        public virtual DbSet<PermissionDetailEntity> PermissionCRUDTypes { get; set; }
        public virtual DbSet<UserGroupEntity> PermissionGroups { get; set; }
        public virtual DbSet<PermissionEntity> Permissions { get; set; }
        public virtual DbSet<OrganizationEntity> Organizations { get; set; }
        public virtual DbSet<ISoftProjectEntity> ISoftProjects { get; set; }
        public virtual DbSet<Example001Entity> Example001s { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<GenTemplateEntity> GenTemplates { get; set; }
        public virtual DbSet<iSoft.Database.Entities.FCMEntity> FCMs { get; set; }

        public CommonDBContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public CommonDBContext(IDBConnectionCustom dbConnectionCustom)
        {

            this.dbConnectionCustom = dbConnectionCustom;
        }
        public CommonDBContext()
        {

        }
        public IDbConnection GetConnection()
        {
            return this.dbConnectionCustom.GetConnection();
        }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
          {
              builder.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Warning)
         .AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information)
         .AddConsole()
         .AddDebug();
          }
        );
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            dbConnectionCustom?.SetOptionBuilder(ref optionsBuilder);
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
#endif
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (this.dbConnectionCustom.GetDBConfig().DatabaseType == iSoft.Common.Enums.DBProvider.EnumDBProvider.Postgres)
            {
                modelBuilder.HasPostgresExtension("unaccent");
                modelBuilder.ConfigureDateTimeProperties("timestamp without time zone");
            }
            modelBuilder.Entity<Example001Entity>()
                        .HasMany(e => e.ListExample003)
                        .WithMany(e => e.ListExample001)
                        .UsingEntity<Dictionary<string, object>>(
                            "refExample001Example003",
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
            modelBuilder.Entity<UserEntity>()
                        .HasMany(e => e.ListISoftProject)
                        .WithMany(e => e.ListUser)
                        .UsingEntity<Dictionary<string, object>>(
                            "refUserISoftProject",
                            j => j
                                .HasOne<ISoftProjectEntity>()
                                .WithMany()
                                .HasForeignKey("ISoftProjectId")
                                .OnDelete(DeleteBehavior.ClientSetNull),
                            j => j
                                .HasOne<UserEntity>()
                                .WithMany()
                                .HasForeignKey("UserId")
                                .OnDelete(DeleteBehavior.ClientSetNull)
                        );

            modelBuilder.Entity<UserEntity>()
                        .HasMany(e => e.ListPermission)
                        .WithMany(e => e.ListUser)
                        .UsingEntity<Dictionary<string, object>>(
                            "refUserPermission",
                            j => j
                                .HasOne<PermissionEntity>()
                                .WithMany()
                                .HasForeignKey("PermissionId")
                                .OnDelete(DeleteBehavior.ClientSetNull),
                            j => j
                                .HasOne<UserEntity>()
                                .WithMany()
                                .HasForeignKey("UserId")
                                .OnDelete(DeleteBehavior.ClientSetNull)
                        );

            modelBuilder.Entity<UserEntity>()
                        .HasMany(e => e.ListUserGroup)
                        .WithMany(e => e.ListUser)
                        .UsingEntity<Dictionary<string, object>>(
                            "refUserUserGroup",
                            j => j
                                .HasOne<UserGroupEntity>()
                                .WithMany()
                                .HasForeignKey("UserGroupId")
                                .OnDelete(DeleteBehavior.ClientSetNull),
                            j => j
                                .HasOne<UserEntity>()
                                .WithMany()
                                .HasForeignKey("UserId")
                                .OnDelete(DeleteBehavior.ClientSetNull)
                        );
            modelBuilder.Entity<PermissionEntity>()
                        .HasMany(e => e.ListUserGroup)
                        .WithMany(e => e.ListPermission)
                        .UsingEntity<Dictionary<string, object>>(
                            "refPermissionUserGroup",
                            j => j
                                .HasOne<UserGroupEntity>()
                                .WithMany()
                                .HasForeignKey("UserGroupId")
                                .OnDelete(DeleteBehavior.ClientSetNull),
                            j => j
                                .HasOne<PermissionEntity>()
                                .WithMany()
                                .HasForeignKey("PermissionId")
                                .OnDelete(DeleteBehavior.ClientSetNull)
                        );
            modelBuilder.Entity<LanguageEntity>()
                        .HasMany(e => e.ListCountry)
                        .WithMany(e => e.ListLanguage)
                        .UsingEntity<Dictionary<string, object>>(
                            "refLanguageCountry",
                            j => j
                                .HasOne<CountryEntity>()
                                .WithMany()
                                .HasForeignKey("CountryId")
                                .OnDelete(DeleteBehavior.ClientSetNull),
                            j => j
                                .HasOne<LanguageEntity>()
                                .WithMany()
                                .HasForeignKey("LanguageId")
                                .OnDelete(DeleteBehavior.ClientSetNull)
                        );
            modelBuilder.Entity<TimezoneEntity>()
                        .HasMany(e => e.ListCountry)
                        .WithMany(e => e.ListTimezone)
                        .UsingEntity<Dictionary<string, object>>(
                            "refTimezoneCountry",
                            j => j
                                .HasOne<CountryEntity>()
                                .WithMany()
                                .HasForeignKey("CountryId")
                                .OnDelete(DeleteBehavior.ClientSetNull),
                            j => j
                                .HasOne<TimezoneEntity>()
                                .WithMany()
                                .HasForeignKey("TimezoneId")
                                .OnDelete(DeleteBehavior.ClientSetNull)
                        );
            modelBuilder.Entity<MachineEntity>()
                        .HasMany(e => e.ListOeePoint)
                        .WithMany(e => e.ListMachine)
                        .UsingEntity<Dictionary<string, object>>(
                            "refMachineOeePoint",
                            j => j
                                .HasOne<OeePointEntity>()
                                .WithMany()
                                .HasForeignKey("OeePointId")
                                .OnDelete(DeleteBehavior.ClientSetNull),
                            j => j
                                .HasOne<MachineEntity>()
                                .WithMany()
                                .HasForeignKey("MachineId")
                                .OnDelete(DeleteBehavior.ClientSetNull)
                        );
            /*[GEN-15]*/
            modelBuilder.Entity<Example001Entity>()
                        .HasOne(e => e.ItemExample002)
                        .WithMany(e => e.ListExample001)
                        .HasForeignKey(e => e.Example002Id)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<PermissionDetailEntity>()
                        .HasOne(e => e.ItemPermission)
                        .WithMany(e => e.ListPermissionDetail)
                        .HasForeignKey(e => e.PermissionId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<TranslateLanguageEntity>()
                        .HasOne(e => e.ItemLanguage)
                        .WithMany(e => e.ListTranslateLanguage)
                        .HasForeignKey(e => e.LanguageId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<UserEntity>()
                        .HasOne(e => e.ItemLanguage)
                        .WithMany(e => e.ListUser)
                        .HasForeignKey(e => e.LanguageId)
                        .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<UserEntity>()
                        .HasOne(e => e.ItemCountry)
                        .WithMany(e => e.ListUser)
                        .HasForeignKey(e => e.CountryId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<MachineBlockDataEntity>()
                        .HasOne(e => e.ItemOeePoint)
                        .WithMany(e => e.ListMachineBlockData)
                        .HasForeignKey(e => e.OeePointId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<OeePointEntity>()
                        .HasOne(e => e.ItemOeePointConfig)
                        .WithMany(e => e.ListOeePoint)
                        .HasForeignKey(e => e.OeePointConfigId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<MachineBlockDataEntity>()
                        .HasOne(e => e.ItemMachineLoss)
                        .WithMany(e => e.ListMachineBlockData)
                        .HasForeignKey(e => e.MachineLossId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<OeePointEntity>()
                        .HasOne(e => e.ItemLine)
                        .WithMany(e => e.ListOeePoint)
                        .HasForeignKey(e => e.LineId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<MachineEntity>()
                        .HasOne(e => e.ItemLine)
                        .WithMany(e => e.ListMachine)
                        .HasForeignKey(e => e.LineId)
                        .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<MachineBlockDataEntity>()
                        .HasOne(e => e.ItemMachineLossPosition)
                        .WithMany(e => e.ListMachineBlockData)
                        .HasForeignKey(e => e.MachineLossPositionId)
                        .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<MachineBlockDataEntity>()
                        .HasOne(e => e.ItemMachineLossDescribe)
                        .WithMany(e => e.ListMachineBlockData)
                        .HasForeignKey(e => e.MachineLossDescribeId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<MachineLossEntity>()
                        .HasOne(e => e.ItemMachineLossGroup)
                        .WithMany(e => e.ListMachineLoss)
                        .HasForeignKey(e => e.MachineLossGroupId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<BreakTimeEntity>()
                        .HasOne(e => e.ItemLine)
                        .WithMany(e => e.ListBreakTime)
                        .HasForeignKey(e => e.LineId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<ShiftEntity>()
                        .HasOne(e => e.ItemLine)
                        .WithMany(e => e.ListShift)
                        .HasForeignKey(e => e.LineId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<WorkshopEntity>()
                        .HasOne(e => e.ItemFactory)
                        .WithMany(e => e.ListWorkshop)
                        .HasForeignKey(e => e.FactoryId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<LineEntity>()
                        .HasOne(e => e.ItemWorkshop)
                        .WithMany(e => e.ListLine)
                        .HasForeignKey(e => e.WorkshopId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
            /*[GEN-16]*/


            // CreatedAt
            modelBuilder.Entity<TranslateLanguageEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<OrganizationEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<ISoftProjectEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<iSoft.Database.Entities.FCMEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<Example001Entity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<GenTemplateEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<PermissionEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<UserGroupEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<PermissionDetailEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<Example002Entity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<Example003Entity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<LanguageEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<CountryEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<TimezoneEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<MachineLossEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<MachineBlockDataEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<MachineLossGroupEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<OeePointEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<OeePointConfigEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<LineEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<MachineEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<MachineLossPositionEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<MachineLossDescribeEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<BreakTimeEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<ShiftEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<WorkshopEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<FactoryEntity>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<MasterDataOptionFilterEntity>().HasIndex(x => x.CreatedAt);
            /*[GEN-22]*/

            // UpdateAt
            modelBuilder.Entity<TranslateLanguageEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<OrganizationEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<ISoftProjectEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<iSoft.Database.Entities.FCMEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<Example001Entity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<GenTemplateEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<PermissionEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<UserGroupEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<PermissionDetailEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<Example002Entity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<Example003Entity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<LanguageEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<CountryEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<TimezoneEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<MachineLossEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<MachineBlockDataEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<MachineLossGroupEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<OeePointEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<OeePointConfigEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<LineEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<MachineEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<MachineLossPositionEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<MachineLossDescribeEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<BreakTimeEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<ShiftEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<WorkshopEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<FactoryEntity>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<MasterDataOptionFilterEntity>().HasIndex(x => x.UpdatedAt);
            /*[GEN-23]*/

            // Order
            modelBuilder.Entity<TranslateLanguageEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<OrganizationEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<ISoftProjectEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<iSoft.Database.Entities.FCMEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<Example001Entity>().HasIndex(x => x.Order);
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<GenTemplateEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<PermissionEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<UserGroupEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<PermissionDetailEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<Example002Entity>().HasIndex(x => x.Order);
            modelBuilder.Entity<Example003Entity>().HasIndex(x => x.Order);
            modelBuilder.Entity<LanguageEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<CountryEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<TimezoneEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<MachineLossEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<MachineBlockDataEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<MachineLossGroupEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<OeePointEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<OeePointConfigEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<LineEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<MachineEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<MachineLossPositionEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<MachineLossDescribeEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<BreakTimeEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<ShiftEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<WorkshopEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<FactoryEntity>().HasIndex(x => x.Order);
            modelBuilder.Entity<MasterDataOptionFilterEntity>().HasIndex(x => x.Order);
            /*[GEN-24]*/

            // DeletedFlag
            modelBuilder.Entity<TranslateLanguageEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<OrganizationEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<ISoftProjectEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<iSoft.Database.Entities.FCMEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<Example001Entity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<GenTemplateEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<PermissionEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<UserGroupEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<PermissionDetailEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<Example002Entity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<Example003Entity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<LanguageEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<CountryEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<TimezoneEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<MachineLossEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<MachineBlockDataEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<MachineLossGroupEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<OeePointEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<OeePointConfigEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<LineEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<MachineEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<MachineLossPositionEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<MachineLossDescribeEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<BreakTimeEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<ShiftEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<WorkshopEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<FactoryEntity>().HasIndex(x => x.DeletedFlag);
            modelBuilder.Entity<MasterDataOptionFilterEntity>().HasIndex(x => x.DeletedFlag);
            /*[GEN-25]*/

            modelBuilder.Entity<MachineBlockDataEntity>().HasIndex(x => x.MachineStatus);
            modelBuilder.Entity<MachineBlockDataEntity>().HasIndex(x => x.StartDateTime);
            modelBuilder.Entity<MachineBlockDataEntity>().HasIndex(x => x.EndDateTime);
            modelBuilder.Entity<MachineBlockDataEntity>().HasIndex(x => x.LastMessageId);
            modelBuilder.Entity<MachineBlockDataEntity>().HasIndex(x => x.LastReceivedTime);
            modelBuilder.Entity<MachineBlockDataEntity>().HasIndex(x => x.LastCountIn);
            modelBuilder.Entity<MachineBlockDataEntity>().HasIndex(x => x.LastNGCount);


            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<DriverRegistrationEntity>().HasKey(e => new { e.DriverId, e.EntryRequestId });
            //modelBuilder.Entity<WorkingDayEntity>().Ignore(x => x.InOutState);

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
