using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Contexts.EntityTypeConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "dbo");
            builder.HasKey(x => x.Id).HasName("PK_Categories").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("bigint").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.AccountId).HasColumnName(@"AccountId").HasColumnType("bigint").IsRequired();
            builder.Property(x => x.CreateDate).HasColumnName(@"CreateDate").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();

            builder.HasOne(a => a.Account).WithMany(b => b.Categories).HasForeignKey(c => c.AccountId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Categories_Accounts");
        }
    }

}
