using Core.Domain;
using Core.Domain.Configurations;
using Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;

//server=manny.db.elephantsql.com;user id=feokoxxo;password=t6BBAodYKvr0rSgJ0yvE9wagEhiuCqHb;database=feokoxxo
//Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\DBCore.mdf;Integrated Security = True;Connect Timeout = 30;User Instance = True
//Server = .\;Initial Catalog=DBCore; AttachDbFilename=|DataDirectory|\DBCore.mdf; Trusted_Connection = True; MultipleActiveResultSets = true
//Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\DBCore.mdf;Initial Catalog=DBCore;Integrated Security=True
//Server=localhost\SQLEXPRESS;Database=DBCore;User Id=sa;Password=93298440
//Server=localhost\SQLEXPRESS;Database=DBCore_Test;User Id=sa;Password=93298440
//Server=LAPTOP-VC2FBTM3\SQLEXPRESS;Database=RM8_Core;User Id=RM8_USER;Password=RM8_USER
//Data Source=SQL5045.site4now.net;Initial Catalog=DB_A4DF0B_EduardoHipolito;User Id=DB_A4DF0B_EduardoHipolito_admin;Password=2007Dudu;
//@"Data Source=sql7005.site4now.net;Initial Catalog=DB_A3D636_RM8;User Id=DB_A3D636_RM8_admin;Password=rm8bluto;Connect Timeout = 180";


namespace Core.DataAccess
{

    public class CoreContext : ContextBase
    {
        private const string connection = @"Data Source=198.38.83.200;Initial Catalog=dstudioc_rm8core_preprod;User Id=dstudioc_rm8;Password=!s$@JFjm$3f!E2#n;Connect Timeout = 180";

        public CoreContext() : base(connection, DataBaseType.SqlServer)
        { }

        public DbSet<Aplication> Aplication { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<Cnae> Cnae { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<UserAplicationCompany> Permitions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new AplicationConfiguration(modelBuilder.Entity<Aplication>());
            new ModuleConfiguration(modelBuilder.Entity<Module>());
            new CnaeConfiguration(modelBuilder.Entity<Cnae>());
            new DocumentConfiguration(modelBuilder.Entity<Document>());
            new DocumentTypeConfiguration(modelBuilder.Entity<DocumentType>());
            new AddressConfiguration(modelBuilder.Entity<Address>());
            new CompanyConfiguration(modelBuilder.Entity<Company>());
            new PersonConfiguration(modelBuilder.Entity<Person>());
            new PhoneConfiguration(modelBuilder.Entity<Phone>());
            new UserConfiguration(modelBuilder.Entity<User>());
            new CountryConfiguration(modelBuilder.Entity<Country>());
            new StateConfiguration(modelBuilder.Entity<State>());
            new CityConfiguration(modelBuilder.Entity<City>());
            new UserAplicationCompanyConfiguration(modelBuilder.Entity<UserAplicationCompany>());
            new PhysicalPersonConfiguration(modelBuilder.Entity<PhysicalPerson>());
            new LegalPersonConfiguration(modelBuilder.Entity<LegalPerson>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
