using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Domain;
using Framework.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Stock.Business.Entities;
using Stock.Domain;
using Stock.Web.Models;

namespace Stock.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JValue, object>().ConvertUsing(source => source.Value);

            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();

            CreateMap<Supplier, SupplierModel>();
            CreateMap<SupplierModel, Supplier>();

            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();

            CreateMap<Entry, EntryModel>();
            CreateMap<EntryModel, Entry>();

            CreateMap<ProductEntry, ProductEntryModel>();
            CreateMap<ProductEntryModel, ProductEntry>();

            CreateMap<Customer, CustomerModel>();
            CreateMap<CustomerModel, Customer>();

            CreateMap<FormOfPayment, FormOfPaymentModel>();
            CreateMap<FormOfPaymentModel, FormOfPayment>();

            CreateMap<Payment, PaymentModel>();
            CreateMap<PaymentModel, Payment>();

            CreateMap<PhysicalPerson, PhysicalPersonModel>();
            CreateMap<PhysicalPersonModel, PhysicalPerson>();

            CreateMap<ProductSale, ProductSaleModel>();
            CreateMap<ProductSaleModel, ProductSale>();

            CreateMap<Sale, SaleModel>();
            CreateMap<SaleModel, Sale>();

            CreateMap<Stock.Domain.Stock, StockModel>();
            CreateMap<StockModel, Stock.Domain.Stock>();

            CreateMap<StockHistory, StockHistoryModel>();
            CreateMap<StockHistoryModel, StockHistory>();

            CreateMap<StockSum, StockSumModel>();
            CreateMap<StockSumModel, StockSum>();
        }
    }

    public class Startup : BaseStartup
    {
        public Startup(IHostingEnvironment configuration) : base(configuration)
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
                options.Authority = "http://localhost:58154";
                options.RequireHttpsMetadata = false;
                options.Audience = "api1";
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
