using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.EntityTypeConfigurations
{
    public class PasswordCollectionConfiguration : IEntityTypeConfiguration<PasswordCollection>
    {
        public void Configure(EntityTypeBuilder<PasswordCollection> builder)
        {
            builder.ToTable("PasswordCollections", "dbo");
            builder.HasKey(x => x.Id).HasName("PK_PasswordCollections").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.AccountId).HasColumnName(@"AccountId").HasColumnType("bigint").IsRequired();
            builder.Property(x => x.Url).HasColumnName(@"Url").HasColumnType("nvarchar(500)").IsRequired().HasMaxLength(500);
            builder.Property(x => x.CategoryId).HasColumnName(@"CategoryId").HasColumnType("bigint").IsRequired();
            builder.Property(x => x.Username).HasColumnName(@"Username").HasColumnType("nvarchar(250)").IsRequired().HasMaxLength(250);
            builder.Property(x => x.Password).HasColumnName(@"Password").HasColumnType("nvarchar(250)").IsRequired().HasMaxLength(250);
            builder.Property(x => x.CreateDate).HasColumnName(@"CreateDate").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Account).WithMany(b => b.PasswordCollections).HasForeignKey(c => c.AccountId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PasswordCollections_Accounts");
            builder.HasOne(a => a.Category).WithMany(b => b.PasswordCollections).HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PasswordCollections_Categories");
        }
    }

}
