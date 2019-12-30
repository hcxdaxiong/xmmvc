using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using XNManage.Common;

namespace XNManage.Models
{
    public class XNDb:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category>  Categories { get; set; }

        public XNDb()
            : base("DefaultEntities")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<XNDb, Configuration<XNDb>>());
        }
    }

    internal sealed class Configuration<TContext> : DbMigrationsConfiguration<TContext> where TContext : DbContext
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}