using Core.DataAccess;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Framework.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Migrations.Seeds
{

    public class CoreSeed
    {
        public void Execute(CoreContext context)
        {
            //context.Database.Migrate();


            try
            {

                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    Country _brazil = new Country()
                    {
                        Code = "BR",
                        Name = "Brasil",
                        PhoneCode = 55
                    };
                    context.Country.Add(_brazil);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed Country OK"));
                    ////////////////////////////////////////////////////////////////

                    State _saoPaulo = new State()
                    {
                        Code = "SP",
                        Name = "São Paulo",
                        IdCountry = _brazil.Id
                    };
                    context.State.Add(_saoPaulo);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed State OK"));
                    ////////////////////////////////////////////////////////////////

                    City _ribeiraoPreto = new City()
                    {
                        Name = "Ribeirão Preto",
                        IdCountry = _brazil.Id,
                        IdState = _saoPaulo.Id
                    };
                    context.City.Add(_ribeiraoPreto);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed City OK"));
                    ////////////////////////////////////////////////////////////////

                    var documentCPF = new DocumentType()
                    {
                        Name = "CPF",
                        PersonType = Core.Domain.Enum.PersonType.Physical,
                        IdCountry = _brazil.Id
                    };
                    var documentRG = new DocumentType()
                    {
                        Name = "RG",
                        PersonType = Core.Domain.Enum.PersonType.Physical,
                        IdCountry = _brazil.Id
                    };
                    var documentCNPJ = new DocumentType()
                    {
                        Name = "CNPJ",
                        PersonType = Core.Domain.Enum.PersonType.Legal,
                        IdCountry = _brazil.Id
                    };
                    context.DocumentType.Add(documentCPF);
                    context.DocumentType.Add(documentRG);
                    context.DocumentType.Add(documentCNPJ);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed DocumentTypes OK"));
                    ////////////////////////////////////////////////////////////////

                    Address endereco = new Address()
                    {
                        IdCity = _ribeiraoPreto.Id,
                        Type = Core.Domain.Enum.AddressType.Residential,
                        Complement = "T 5 B D Ap 11",
                        PublicArea = "José de Alcantara",
                        PublicAreaType = Core.Domain.Enum.PublicAreaType.Street,
                        Number = 1115,
                        IdState = _saoPaulo.Id,
                        IdCountry = _brazil.Id,
                        Neighborhood = "Heitor Rigon",
                        PostalCode = 14062100
                    };
                    var cpf = TextoHelper.LongRandom(10000000000, 99999999999, new Random()).ToString();
                    var cpfObj = new Document()
                    {
                        IdDocumentType = documentCPF.Id,
                        Value = cpf
                    };
                    var rg = TextoHelper.LongRandom(10000000000, 99999999999, new Random()).ToString();
                    var rgObj = new Document()
                    {
                        IdDocumentType = documentRG.Id,
                        Value = rg
                    };
                    var tel = new Phone()
                    {
                        Type = Core.Domain.Enum.PhoneType.Residential,
                        IdCountry = _brazil.Id,
                        AreaCode = 16,
                        Number = 37049953
                    };
                    var cel = new Phone()
                    {
                        Type = Core.Domain.Enum.PhoneType.Cell,
                        IdCountry = _brazil.Id,
                        AreaCode = 16,
                        Number = 991880904
                    };
                    PhysicalPerson pessoaF = new PhysicalPerson()
                    {
                        Name = "Eduardo Hipolito Pimenta de Pádua",
                        HomePage = "www.rm8.com.br",
                        Email = "grande.eduardo@gmail.com"
                    };
                    pessoaF.Addresses = new List<Address>
                {
                    endereco
                };
                    pessoaF.Documents = new List<Document>
                {
                    cpfObj,
                    rgObj
                };
                    pessoaF.Phones = new List<Phone>
                {
                    tel,
                    cel
                };
                    context.Person.Add(pessoaF);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed pessoaF OK"));
                    ////////////////////////////////////////////////////////////////

                    var endereco2 = new Address()
                    {
                        IdCity = _ribeiraoPreto.Id,
                        Type = Core.Domain.Enum.AddressType.Residential,
                        Complement = "Casa",
                        PublicArea = "Salvador Minardi",
                        PublicAreaType = Core.Domain.Enum.PublicAreaType.Street,
                        Number = 464,
                        IdState = _saoPaulo.Id,
                        IdCountry = _brazil.Id,
                        Neighborhood = "Alto das Acácias",
                        PostalCode = 14140000
                    };
                    var cnpj = TextoHelper.LongRandom(10000000000, 99999999999, new Random()).ToString();
                    var cnpjObj = new Document()
                    {
                        IdDocumentType = documentCNPJ.Id,
                        Value = cnpj
                    };
                    var tel2 = new Phone()
                    {
                        Type = Core.Domain.Enum.PhoneType.Commercial,
                        IdCountry = _brazil.Id,
                        AreaCode = 16,
                        Number = 37049953
                    };
                    var cel2 = new Phone()
                    {
                        Type = Core.Domain.Enum.PhoneType.Commercial,
                        IdCountry = _brazil.Id,
                        AreaCode = 16,
                        Number = 991880904
                    };
                    LegalPerson pessoaJ = new LegalPerson()
                    {
                        Name = "Eduardo Hipolito Pimenta de Pádua",
                        HomePage = "www.rm8.com.br",
                        Email = "grande.eduardo@gmail.com",
                        FantasyName = "RM8 Teste 1",
                        CorporateName = "RM8 Sistemas LTDA"
                    };
                    pessoaJ.Addresses = new List<Address>
                {
                    endereco2
                };
                    pessoaJ.Documents = new List<Document>
                {
                    cnpjObj
                };
                    pessoaJ.Phones = new List<Phone>
                {
                    tel2,
                    cel2
                };
                    context.Person.Add(pessoaJ);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed pessoaJ OK"));
                    ////////////////////////////////////////////////////////////////

                    endereco = new Address()
                    {
                        IdCity = _ribeiraoPreto.Id,
                        Type = Core.Domain.Enum.AddressType.Residential,
                        Complement = "Casa",
                        PublicArea = "Salvador Minardi",
                        PublicAreaType = Core.Domain.Enum.PublicAreaType.Street,
                        Number = 464,
                        IdState = _saoPaulo.Id,
                        IdCountry = _brazil.Id,
                        Neighborhood = "Alto das Acácias",
                        PostalCode = 14140000
                    };
                    LegalPerson pessoaJ2 = new LegalPerson()
                    {
                        Name = "Eduardo Hipolito Pimenta de Pádua",
                        HomePage = "www.rm8.com.br",
                        Email = "grande.eduardo@gmail.com",
                        FantasyName = "RM8 Teste 2",
                        CorporateName = "RM82 Sistemas LTDA",
                        Addresses = new List<Address>()
                    };
                    pessoaJ2.Addresses.Add(endereco);
                    var cnpj2 = TextoHelper.LongRandom(10000000000, 99999999999, new Random()).ToString();
                    var cnpjObj2 = new Document()
                    {
                        IdDocumentType = documentCNPJ.Id,
                        Value = cnpj2,
                        IdPerson = pessoaJ2.Id
                    };
                    pessoaJ2.Documents = new List<Document>
                {
                    cnpjObj2
                };
                    tel = new Phone()
                    {
                        Type = Core.Domain.Enum.PhoneType.Commercial,
                        IdCountry = _brazil.Id,
                        AreaCode = 16,
                        Number = 37049953
                    };
                    cel = new Phone()
                    {
                        Type = Core.Domain.Enum.PhoneType.Cell,
                        IdCountry = _brazil.Id,
                        AreaCode = 16,
                        Number = 981841833
                    };
                    pessoaJ2.Phones = new List<Phone>
                {
                    tel,
                    cel
                };
                    context.Person.Add(pessoaJ2);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed pessoaJ2 OK"));
                    ////////////////////////////////////////////////////////////////

                    Company loja = new Company()
                    {
                        IdPerson = pessoaJ.Id,
                        PaymentDay = 20,
                        ReducedName = "RM8 Teste MATRIZ",
                        Type = Core.Domain.Enum.CompanyType.Master,
                    };
                    context.Company.Add(loja);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed loja OK"));
                    ////////////////////////////////////////////////////////////////

                    Company loja2 = new Company()
                    {
                        IdPerson = pessoaJ2.Id,
                        PaymentDay = 20,
                        ReducedName = "RM8 Teste filial",
                        Type = Core.Domain.Enum.CompanyType.Child,
                        IdMaster = loja.Id
                    };
                    context.Company.Add(loja2);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed loja2 OK"));
                    ////////////////////////////////////////////////////////////////

                    User usuario = new User()
                    {
                        IdPerson = pessoaF.Id,
                        Login = "EduardoH",
                        ProfileType = Core.Domain.Enum.ProfileType.Developer,
                        Password = Crypt.Encrypt("93298440")
                    };
                    context.Users.Add(usuario);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuario OK"));
                    ////////////////////////////////////////////////////////////////

                    Module _base = new Module()
                    {
                        Name = "Administração",
                        Description = "Administração do sistema",
                        ModuleCode = Core.Business.AplicationsCodes.ModuleCode,
                        Index = 1,
                        Icon = "fa fa-cogs"
                    };
                    context.Module.Add(_base);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _base OK"));
                    ////////////////////////////////////////////////////////////////

                    Module _stock = new Module()
                    {
                        Name = "Comercial",
                        Description = "Cadastros comerciais",
                        ModuleCode = Stock.Business.AplicationsCodes.ModuleCode,
                        Index = 2,
                        Icon = "fa fa-table"
                    };
                    context.Module.Add(_stock);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _comercial OK"));
                    ////////////////////////////////////////////////////////////////

                    Aplication _physicalPessoa = new Aplication()
                    {
                        Name = "Pessoa Fisica",
                        Description = "Cadastro de Pessoa Fisica",
                        AplicationCode = Core.Business.AplicationsCodes.PhysicalPerson,
                        Link = "/system/base/physicalperson",
                        Index = 1,
                        IdModule = _base.Id
                    };
                    context.Aplication.Add(_physicalPessoa);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _physicalPessoa OK"));
                    ////////////////////////////////////////////////////////////////

                    Aplication _legalPessoa = new Aplication()
                    {
                        Name = "Pessoa Juridica",
                        Description = "Cadastro de Pessoa Juridica",
                        AplicationCode = Core.Business.AplicationsCodes.LegalPerson,
                        Link = "/system/base/legalperson",
                        Index = 2,
                        IdModule = _base.Id
                    };
                    context.Aplication.Add(_legalPessoa);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _legalPessoa OK"));
                    ////////////////////////////////////////////////////////////////

                    Aplication _usuario = new Aplication()
                    {
                        Name = "Usuarios",
                        Description = "Cadastro de Usuarios",
                        AplicationCode = Core.Business.AplicationsCodes.User,
                        Link = "/system/base/user",
                        Index = 3,
                        IdModule = _base.Id
                    };
                    context.Aplication.Add(_usuario);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _usuario OK"));
                    ////////////////////////////////////////////////////////////////

                    Aplication _loja = new Aplication()
                    {
                        Name = "Empresas",
                        Description = "Cadastro de Empresas",
                        AplicationCode = Core.Business.AplicationsCodes.Company,
                        Link = "/system/base/company",
                        Index = 4,
                        IdModule = _base.Id
                    };
                    context.Aplication.Add(_loja);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _loja OK"));
                    ////////////////////////////////////////////////////////////////

                    Aplication _modulo = new Aplication()
                    {
                        Name = "Modulos",
                        Description = "Cadastro de Modulos",
                        AplicationCode = Core.Business.AplicationsCodes.Module,
                        Link = "/system/base/module",
                        Index = 5,
                        IdModule = _base.Id
                    };
                    context.Aplication.Add(_modulo);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _modulo OK"));
                    ////////////////////////////////////////////////////////////////

                    Aplication _aplication = new Aplication()
                    {
                        Name = "Aplicações",
                        Description = "Cadastro de Aplicações",
                        AplicationCode = Core.Business.AplicationsCodes.Aplication,
                        Link = "/system/base/aplication",
                        Index = 6,
                        IdModule = _base.Id
                    };
                    context.Aplication.Add(_aplication);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _aplication OK"));
                    ////////////////////////////////////////////////////////////////

                    Aplication _cnae = new Aplication()
                    {
                        Name = "Cnae",
                        Description = "Cadastro de Cnae",
                        AplicationCode = Core.Business.AplicationsCodes.Cnae,
                        Link = "/system/base/cnae",
                        Index = 7,
                        IdModule = _base.Id
                    };
                    context.Aplication.Add(_cnae);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _cnae OK"));
                    ////////////////////////////////////////////////////////////////

                    Aplication _country = new Aplication()
                    {
                        Name = "País",
                        Description = "Cadastro de País",
                        AplicationCode = Core.Business.AplicationsCodes.Country,
                        Link = "/system/base/country",
                        Index = 8,
                        IdModule = _base.Id
                    };
                    context.Aplication.Add(_country);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _country OK"));
                    ////////////////////////////////////////////////////////////////

                    Aplication _state = new Aplication()
                    {
                        Name = "Estado",
                        Description = "Cadastro de Estado",
                        AplicationCode = Core.Business.AplicationsCodes.State,
                        Link = "/system/base/state",
                        Index = 9,
                        IdModule = _base.Id
                    };
                    context.Aplication.Add(_state);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _state OK"));
                    ////////////////////////////////////////////////////////////////

                    Aplication _city = new Aplication()
                    {
                        Name = "Cidade",
                        Description = "Cadastro de Cidade",
                        AplicationCode = Core.Business.AplicationsCodes.City,
                        Link = "/system/base/city",
                        Index = 10,
                        IdModule = _base.Id
                    };
                    context.Aplication.Add(_city);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _city OK"));
                    ////////////////////////////////////////////////////////////////

                    Aplication _userAplicationCompany = new Aplication()
                    {
                        Name = "Permissões",
                        Description = "Vinculo de permissão do usuario para empresa e aplicações",
                        AplicationCode = Core.Business.AplicationsCodes.UserAplicationCompany,
                        Link = "/system/base/useraplicationcompany",
                        Index = 11,
                        IdModule = _base.Id
                    };
                    context.Aplication.Add(_userAplicationCompany);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _userAplicationCompany OK"));
                    ////////////////////////////////////////////////////////////////

                    Aplication _Category = new Aplication()
                    {
                        Name = "Categoria",
                        Description = "Categoria de Produtos",
                        AplicationCode = Stock.Business.AplicationsCodes.Category,
                        Link = "/system/stock/category",
                        Index = 1,
                        IdModule = _stock.Id
                    };
                    context.Aplication.Add(_Category);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _Category OK"));

                    ////////////////////////////////////////////////////////////////
                    Aplication _Product = new Aplication()
                    {
                        Name = "Produto",
                        Description = "Cadastro de Produtos",
                        AplicationCode = Stock.Business.AplicationsCodes.Product,
                        Link = "/system/stock/product",
                        Index = 2,
                        IdModule = _stock.Id
                    };
                    context.Aplication.Add(_Product);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _Product OK"));
                    ////////////////////////////////////////////////////////////////
                    Aplication _Entry = new Aplication()
                    {
                        Name = "Entrada",
                        Description = "Entrada de produtos",
                        AplicationCode = Stock.Business.AplicationsCodes.Entry,
                        Link = "/system/stock/entry",
                        Index = 4,
                        IdModule = _stock.Id
                    };
                    context.Aplication.Add(_Entry);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _Entry OK"));
                    ////////////////////////////////////////////////////////////////
                    Aplication _Supplier = new Aplication()
                    {
                        Name = "Fornecedor",
                        Description = "Cadastro de fornecedores",
                        AplicationCode = Stock.Business.AplicationsCodes.Supplier,
                        Link = "/system/stock/supplier",
                        Index = 34,
                        IdModule = _stock.Id
                    };
                    context.Aplication.Add(_Supplier);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _Supplier OK"));
                    ////////////////////////////////////////////////////////////////
                    Aplication _customer = new Aplication()
                    {
                        Name = "Cliente",
                        Description = "Cadastro de clientes",
                        AplicationCode = Stock.Business.AplicationsCodes.Customer,
                        Link = "/system/stock/customer",
                        Index = 5,
                        IdModule = _stock.Id
                    };
                    context.Aplication.Add(_customer);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _customer OK"));
                    ////////////////////////////////////////////////////////////////
                    Aplication _formOfPayment = new Aplication()
                    {
                        Name = "Formar de pagamento",
                        Description = "Cadastro de formas de pagamento",
                        AplicationCode = Stock.Business.AplicationsCodes.FormOfPayment,
                        Link = "/system/stock/formofpayment",
                        Index = 6,
                        IdModule = _stock.Id
                    };
                    context.Aplication.Add(_formOfPayment);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _formOfPayment OK"));
                    ////////////////////////////////////////////////////////////////
                    Aplication _sale = new Aplication()
                    {
                        Name = "Venda",
                        Description = "Vendas de produtos",
                        AplicationCode = Stock.Business.AplicationsCodes.Sale,
                        Link = "/system/stock/sale",
                        Index = 7,
                        IdModule = _stock.Id
                    };
                    context.Aplication.Add(_sale);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _sale OK"));
                    ////////////////////////////////////////////////////////////////
                    Aplication _stockApp = new Aplication()
                    {
                        Name = "Estoque",
                        Description = "Estoque de produtos",
                        AplicationCode = Stock.Business.AplicationsCodes.Stock,
                        Link = "/system/stock/stocksummary",
                        Index = 8,
                        IdModule = _stock.Id
                    };
                    context.Aplication.Add(_stockApp);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _sale OK"));
                    ////////////////////////////////////////////////////////////////
                    Aplication _dailySales = new Aplication()
                    {
                        Name = "Vendas do dia",
                        Description = "Visualizar as vendas do dia",
                        AplicationCode = Stock.Business.AplicationsCodes.DailySales,
                        Link = "",
                        Index = 100,
                        IdModule = _stock.Id,
                        ShowMenu = false,
                    };
                    context.Aplication.Add(_dailySales);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _dailySales OK"));
                    ////////////////////////////////////////////////////////////////
                    Aplication _dailyEntries = new Aplication()
                    {
                        Name = "Entradas do dia",
                        Description = "Vizualisar as entradas do dia",
                        AplicationCode = Stock.Business.AplicationsCodes.DailyEntries,
                        Link = "",
                        Index = 101,
                        IdModule = _stock.Id,
                        ShowMenu = false,
                    };
                    context.Aplication.Add(_dailyEntries);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _dailyEntries OK"));
                    ////////////////////////////////////////////////////////////////
                    Aplication _dailyProfit = new Aplication()
                    {
                        Name = "Lucro do dia",
                        Description = "Vizualisar o lucro do dia",
                        AplicationCode = Stock.Business.AplicationsCodes.DailyProfit,
                        Link = "",
                        Index = 102,
                        IdModule = _stock.Id,
                        ShowMenu = false,
                    };
                    context.Aplication.Add(_dailyProfit);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _dailyProfit OK"));
                    ////////////////////////////////////////////////////////////////
                    Aplication _monthSales = new Aplication()
                    {
                        Name = "Vendas do mês",
                        Description = "Vizualizar as vendas do mês",
                        AplicationCode = Stock.Business.AplicationsCodes.MonthSales,
                        Link = "",
                        Index = 103,
                        IdModule = _stock.Id,
                        ShowMenu = false,
                    };
                    context.Aplication.Add(_monthSales);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _monthSales OK"));
                    ////////////////////////////////////////////////////////////////
                    Aplication _monthEntries = new Aplication()
                    {
                        Name = "Entradas do mês",
                        Description = "Vizualizar as entradas do mês",
                        AplicationCode = Stock.Business.AplicationsCodes.MonthEntries,
                        Link = "",
                        Index = 104,
                        IdModule = _stock.Id,
                        ShowMenu = false,
                    };
                    context.Aplication.Add(_monthEntries);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _monthEntries OK"));
                    ////////////////////////////////////////////////////////////////
                    Aplication _monthProfit = new Aplication()
                    {
                        Name = "Lucro do mês",
                        Description = "Vizualizar o lucro do mês",
                        AplicationCode = Stock.Business.AplicationsCodes.MonthProfit,
                        Link = "",
                        Index = 105,
                        IdModule = _stock.Id,
                        ShowMenu = false,
                    };
                    context.Aplication.Add(_monthProfit);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _monthProfit OK"));
                    ////////////////////////////////////////////////////////////////
                    Aplication _documentType = new Aplication()
                    {
                        Name = "Tipos de documentos",
                        Description = "Tipos de documentos",
                        AplicationCode = Core.Business.AplicationsCodes.DocumentType,
                        Link = "",
                        Index = 100,
                        IdModule = _base.Id,
                        ShowMenu = false,
                    };
                    context.Aplication.Add(_documentType);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed _documentType OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_physicalpessoa = new UserAplicationCompany()
                    {
                        IdAplication = _physicalPessoa.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_physicalpessoa);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_physicalpessoa OK"));

                    ////////////////////////////////////////////////////////////////
                    UserAplicationCompany usuarioAplicationLoja_legalpessoa = new UserAplicationCompany()
                    {
                        IdAplication = _legalPessoa.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_legalpessoa);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_legalpessoa OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_usuario = new UserAplicationCompany()
                    {
                        IdAplication = _usuario.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_usuario);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_usuario OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_loja = new UserAplicationCompany()
                    {
                        IdAplication = _loja.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_loja);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_loja OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_aplication = new UserAplicationCompany()
                    {
                        IdAplication = _aplication.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_aplication);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_aplication OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_modulo = new UserAplicationCompany()
                    {
                        IdAplication = _modulo.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_modulo);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_modulo OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_userAplicationCompany = new UserAplicationCompany()
                    {
                        IdAplication = _userAplicationCompany.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_userAplicationCompany);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_userAplicationCompany OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_Cnae = new UserAplicationCompany()
                    {
                        IdAplication = _cnae.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_Cnae);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_Cnae OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_Country = new UserAplicationCompany()
                    {
                        IdAplication = _country.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_Country);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_Country OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_state = new UserAplicationCompany()
                    {
                        IdAplication = _state.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_state);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_state OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_city = new UserAplicationCompany()
                    {
                        IdAplication = _city.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_city);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_city OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_usuario2 = new UserAplicationCompany()
                    {
                        IdAplication = _usuario.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja2.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_usuario2);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_usuario2 OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_loja2 = new UserAplicationCompany()
                    {
                        IdAplication = _loja.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja2.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_loja2);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_loja2 OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_aplication2 = new UserAplicationCompany()
                    {
                        IdAplication = _aplication.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja2.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_aplication2);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_aplication2 OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_modulo2 = new UserAplicationCompany()
                    {
                        IdAplication = _modulo.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja2.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_modulo2);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_modulo2 OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_usrAppsLoja2 = new UserAplicationCompany()
                    {
                        IdAplication = _userAplicationCompany.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja2.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_usrAppsLoja2);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_usrAppsLoja2 OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_Cnae2 = new UserAplicationCompany()
                    {
                        IdAplication = _cnae.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja2.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_Cnae2);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_Cnae2 OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_Country2 = new UserAplicationCompany()
                    {
                        IdAplication = _country.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja2.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_Country2);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_Country OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_state2 = new UserAplicationCompany()
                    {
                        IdAplication = _state.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja2.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_state2);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_state2 OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_city2 = new UserAplicationCompany()
                    {
                        IdAplication = _city.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja2.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_city2);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_city2 OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_Category = new UserAplicationCompany()
                    {
                        IdAplication = _Category.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_Category);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_Category OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_Entry = new UserAplicationCompany()
                    {
                        IdAplication = _Entry.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_Entry);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_Entry OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_product = new UserAplicationCompany()
                    {
                        IdAplication = _Product.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_product);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_product OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_Supplier = new UserAplicationCompany()
                    {
                        IdAplication = _Supplier.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_Supplier);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_Supplier OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_Customer = new UserAplicationCompany()
                    {
                        IdAplication = _customer.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_Customer);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_Customer OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_formOfPayment = new UserAplicationCompany()
                    {
                        IdAplication = _formOfPayment.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_formOfPayment);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_formOfPayment OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_sale = new UserAplicationCompany()
                    {
                        IdAplication = _sale.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_sale);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_sale OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_stockApp = new UserAplicationCompany()
                    {
                        IdAplication = _stockApp.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.Full
                    };
                    context.Permitions.Add(usuarioAplicationLoja_stockApp);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_stockApp OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_dailyEntries = new UserAplicationCompany()
                    {
                        IdAplication = _dailyEntries.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.View
                    };
                    context.Permitions.Add(usuarioAplicationLoja_dailyEntries);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_dailyEntries OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_dailyProfit = new UserAplicationCompany()
                    {
                        IdAplication = _dailyProfit.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.View
                    };
                    context.Permitions.Add(usuarioAplicationLoja_dailyProfit);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_dailyProfit OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_monthSales = new UserAplicationCompany()
                    {
                        IdAplication = _monthSales.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.View
                    };
                    context.Permitions.Add(usuarioAplicationLoja_monthSales);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_monthSales OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_monthEntries = new UserAplicationCompany()
                    {
                        IdAplication = _monthEntries.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.View
                    };
                    context.Permitions.Add(usuarioAplicationLoja_monthEntries);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_monthEntries OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_monthProfit = new UserAplicationCompany()
                    {
                        IdAplication = _monthProfit.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.View
                    };
                    context.Permitions.Add(usuarioAplicationLoja_monthProfit);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_monthProfit OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_dailySales = new UserAplicationCompany()
                    {
                        IdAplication = _dailySales.Id,
                        IdUser = usuario.Id,
                        IdCompany = loja.Id,
                        AccessLevel = Core.Domain.Enum.AccessLevel.View
                    };
                    context.Permitions.Add(usuarioAplicationLoja_dailySales);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_dailySales OK"));
                    ////////////////////////////////////////////////////////////////

                    UserAplicationCompany usuarioAplicationLoja_documentType = new UserAplicationCompany()
                    {
                        IdAplication = _documentType.Id,
                        IsGlobal = true,
                        AccessLevel = Core.Domain.Enum.AccessLevel.View
                    };
                    context.Permitions.Add(usuarioAplicationLoja_documentType);
                    context.SaveChanges();
                    Log.Instance.Function(new System.Text.StringBuilder().AppendLine("Seed usuarioAplicationLoja_documentType OK"));
                    ////////////////////////////////////////////////////////////////
                    dbContextTransaction.Commit();
                }

            }
            catch (Exception ex)
            {
                Log.Instance.ErrorLog(ex);
            }
        }
    }
}
