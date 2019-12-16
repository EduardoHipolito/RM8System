using AutoMapper;
using Core.Domain;
using Core.Web.Models;
using Framework.Web;
using Framework.Web.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;

namespace Core.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JValue, object>().ConvertUsing(source => source.Value);

            // Add as many of these lines as you need to map your objects
            CreateMap<Address, AddressModel>();
            CreateMap<AddressModel, Address>();

            CreateMap<Aplication, AplicationModel>();
            CreateMap<AplicationModel, Aplication>();

            CreateMap<City, CityModel>();
            CreateMap<CityModel, City>();

            CreateMap<Cnae, CnaeModel>();
            CreateMap<CnaeModel, Cnae>();

            CreateMap<Company, CompanyModel>().ForMember(f => f.Children, opt => opt.Ignore());
            CreateMap<CompanyModel, Company>().ForMember(f => f.Children, opt => opt.Ignore()); ;

            CreateMap<Country, CountryModel>();
            CreateMap<CountryModel, Country>();

            CreateMap<Document, DocumentModel>();
            CreateMap<DocumentModel, Document>();

            CreateMap<DocumentType, DocumentTypeModel>();
            CreateMap<DocumentTypeModel, DocumentType>();

            CreateMap<LegalPerson, LegalPersonModel>();
            CreateMap<LegalPersonModel, LegalPerson>();

            CreateMap<Person, LegalPersonModel>();
            CreateMap<LegalPersonModel, Person>();

            CreateMap<Module, ModuleModel>();
            CreateMap<ModuleModel, Module>();

            CreateMap<Person, PersonModel>();
            CreateMap<PersonModel, Person>();

            CreateMap<Phone, PhoneModel>();
            CreateMap<PhoneModel, Phone>();

            CreateMap<PhysicalPerson, PhysicalPersonModel>();
            CreateMap<PhysicalPersonModel, PhysicalPerson>();

            CreateMap<Person, PhysicalPersonModel>();
            CreateMap<PhysicalPersonModel, Person>();

            CreateMap<State, StateModel>();
            CreateMap<StateModel, State>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();

            CreateMap<UserAplicationCompany, UserAplicationCompanyModel>();
            CreateMap<UserAplicationCompanyModel, UserAplicationCompany>();
            
        }
    }
    public class Startup : BaseStartup
    {
        public Startup(IHostingEnvironment configuration) : base (configuration)
        {
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            base.Configure(app, env, loggerFactory);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication().AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Audience = "api1";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = TokenAuthOption.Key,
                    RequireSignedTokens = true,
                    ValidAudience = TokenAuthOption.Audience,
                    ValidIssuer = TokenAuthOption.Issuer,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            base.ConfigureServices(services);
        }
    }
}
