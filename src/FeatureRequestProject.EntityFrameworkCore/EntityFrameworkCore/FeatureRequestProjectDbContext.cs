using FeatureRequestProject.FeatureRequestComments;
using FeatureRequestProject.FeatureRequests;
using FeatureRequestProject.FeatureRequestVotes;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace FeatureRequestProject.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class FeatureRequestProjectDbContext :
    AbpDbContext<FeatureRequestProjectDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    public DbSet<FeatureRequest> FeatureRequests { get; set; }
    public DbSet<FeatureRequestComment> FeatureRequestComments { get; set; }
    public DbSet<FeatureRequestVote> FeatureRequestVotes { get; set; }

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public FeatureRequestProjectDbContext(DbContextOptions<FeatureRequestProjectDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        builder.Entity<FeatureRequest>(f =>
        {
            f.ToTable(FeatureRequestProjectConsts.DbTablePrefix + "FeatureRequests",
                FeatureRequestProjectConsts.DbSchema);
            f.ConfigureByConvention();
            f.Property(fr => fr.Title).IsRequired().HasMaxLength(200);
            f.Property(fr => fr.Description).IsRequired().HasMaxLength(2000);
        });

        builder.Entity<FeatureRequestComment>(fc =>
        {
            fc.ToTable(FeatureRequestProjectConsts.DbTablePrefix + "FeatureRequestComments",
                FeatureRequestProjectConsts.DbSchema);
            fc.ConfigureByConvention();
            fc.Property(c => c.Content).IsRequired();
            fc.HasOne<FeatureRequest>().WithMany(fr => fr.Comments).HasForeignKey(c => c.FeatureRequestId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            fc.HasOne<IdentityUser>().WithMany().HasForeignKey(c => c.UserId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<FeatureRequestVote>(fv =>
        {
            fv.ToTable(FeatureRequestProjectConsts.DbTablePrefix + "FeatureRequestVotes",
                FeatureRequestProjectConsts.DbSchema);
            fv.ConfigureByConvention();
            fv.HasIndex(v => new { v.FeatureRequestId, v.UserId }).IsUnique();
        });

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(FeatureRequestProjectConsts.DbTablePrefix + "YourEntities", FeatureRequestProjectConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}
