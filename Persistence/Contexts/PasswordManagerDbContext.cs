using Domain.Entities;
using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts.EntityTypeConfigurations;

namespace Persistence.Contexts
{
    public partial class PasswordManagerDbContext : DbContext
    {
        private readonly IEncryptionProvider _provider;
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<PasswordCollection> PasswordCollections { get; set; }
        public PasswordManagerDbContext(DbContextOptions<PasswordManagerDbContext> options)
         : base(options)
        {
            this._provider = new GenerateEncryptionProvider("LTnGRsfktH40XVdL1FVP3c6DhMFbox44");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            OnModelCreatingPartial(modelBuilder);
            modelBuilder.UseEncryption(this._provider);
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PasswordCollectionConfiguration());

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
