using Framework.DataAccess.EntityFrameworkExtentions;
using Framework.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Framework.DataAccess
{

    public class ContextBase : DbContext
    {
        public string ConnectionString { get; set; }
        public DataBaseType DataBaseType { get; set; }

        public ContextBase(string connectionString, DataBaseType dataBaseType) : base()
        {
            this.ConnectionString = connectionString;
            this.DataBaseType = dataBaseType;
        }
        public ContextBase(DbContextOptions options) : base(options)
        {
        }
        public static string MDF_Directory
        {
            get
            {
                //return @"D:\eduardohipolito2007\RM8\Backend";
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
            string environmentConnectionString = Environment.GetEnvironmentVariable("ConnectionString");
            Enum.TryParse(Environment.GetEnvironmentVariable("DataBaseType"), out DataBaseType environmentDataBaseType);

            if (environmentConnectionString != null)
            {
                ConnectionString = environmentConnectionString;
                DataBaseType = environmentDataBaseType;

                //this.Database.Migrate();
            }

            //Log.Instance.Function(new StringBuilder(environmentConnectionString));

            ConnectionString = ConnectionString.Replace("|DataDirectory|", MDF_Directory);

            switch (DataBaseType)
            {
                case DataBaseType.SqlServer:
                    optionsBuilder.UseSqlServer(ConnectionString);
                    break;
                case DataBaseType.Postgre:
                    optionsBuilder.UseNpgsql(ConnectionString);
                    break;
                default:
                    optionsBuilder.UseSqlServer(ConnectionString);
                    break;
            }




            LoggerFactory MyLoggerFactory = new LoggerFactory(new[] { new LoggerProvider((x) => Debug.WriteLine(x)) });
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            //optionsBuilder.UseNpgsql(@"server=localhost;user id=postgres;password=93298440;database=dbRM8Core"); // POSTGRESS
            //optionsBuilder.UseNpgsql(@"server=stampy-01.db.elephantsql.com;user id=fsmlbooq;password=vmbB2wPhUdAlhEHeX3O1uZT2KjVdWbzw;database=fsmlbooq"); //ProdTeste
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplySing();

            var cascadeFKs = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()).Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }
    }
}
