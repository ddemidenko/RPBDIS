using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstateAgencyL3.Data;
using System.Threading.Tasks;
using RealEstateAgencyL3.Services;
using RealEstateAgencyL3.Models;
using System.Text.Json;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;

        string connection = builder.Configuration.GetConnectionString("SqlServerConnection");

        services.AddDbContext<RealEstateAgencyContext>(options => options.UseSqlServer(connection));

        services.AddMemoryCache();

        services.AddDistributedMemoryCache();
        services.AddSession();

        services.AddScoped<ICacheContractsService, CacheContractsService>();

        var app = builder.Build();

        app.UseStaticFiles();

        app.UseSession();

        
        app.Map("/info", (appBuilder) =>
        {
            appBuilder.Run(async (context) =>
            {
                string strResponse = "<HTML><HEAD><TITLE>Информация</TITLE></HEAD>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY><H1>Информация:</H1>";
                strResponse += "<BR> Сервер: " + context.Request.Host;
                strResponse += "<BR> Путь: " + context.Request.PathBase;
                strResponse += "<BR> Протокол: " + context.Request.Protocol;
                strResponse += "<BR><A href='/'>Главная</A></BODY></HTML>";
                await context.Response.WriteAsync(strResponse);
            });
        });

        app.Map("/contracts", (appBuilder) =>
        {
            appBuilder.Run(async (context) =>
            {
                ICacheContractsService cachedContractsService = context.RequestServices.GetService<ICacheContractsService>();
                IEnumerable<Contract> Contracts = cachedContractsService.GetContracts("Contracts20");
                string HtmlString = "<HTML><HEAD><TITLE>Договора</TITLE></HEAD>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY><H1>Список договоров</H1>" +
                "<TABLE BORDER=1>";
                HtmlString += "<TR>";
                HtmlString += "<TH>Номер договора</TH>";
                HtmlString += "<TH>Дата</TH>";
                HtmlString += "<TH>Номер продавца</TH>";
                HtmlString += "<TH>Номер покупателя</TH>";
                HtmlString += "<TH>Сумма сделки</TH>";
                HtmlString += "<TH>Стоимость услуги</TH>";
                HtmlString += "<TH>Сотрудник</TH>";
                HtmlString += "<TH>ФИО покупателя</TH>";
                HtmlString += "</TR>";
                foreach (var Contract in Contracts)
                {
                    HtmlString += "<TR>";
                    HtmlString += "<TD>" + Contract.ContractId + "</TD>";
                    HtmlString += "<TD>" + Contract.DateOfContract + "</TD>";
                    HtmlString += "<TD>" + Contract.SellerId + "</TD>";
                    HtmlString += "<TD>" + Contract.BuyerId + "</TD>";
                    HtmlString += "<TD>" + Contract.DealAmount + "</TD>";
                    HtmlString += "<TD>" + Contract.ServiceCost + "</TD>";
                    HtmlString += "<TD>" + Contract.Employee + "</TD>";
                    HtmlString += "<TD>" + Contract.Fiobuyer + "</TD>";
                    HtmlString += "</TR>";
                }
                HtmlString += "</TABLE>";
                HtmlString += "<BR><A href='/'>Главная</A></BR>";
                HtmlString += "<BR><A href='/Contracts'>Договора</A></BR>";
                HtmlString += "<BR><A href='/form'>Данные пользователя</A></BR>";
                HtmlString += "<BR><A href='/searchForm1'>Поиско договора по номеру через куки</A></BR>";
                HtmlString += "<BR><A href='/SeacrhForm2'>Поиско договора по номеру через сессию</A></BR>";

                HtmlString += "</BODY></HTML>";

                await context.Response.WriteAsync(HtmlString);
            });
        });
        app.Map("/searchForm1", (appBuilder) =>
        {
            appBuilder.Run(async (context) =>
            {
      
                ICacheContractsService cachedContractsService = context.RequestServices.GetService<ICacheContractsService>();
                IEnumerable<Contract> Contracts = cachedContractsService.GetContracts("Contracts20");
                string HtmlString = "<HTML><HEAD><TITLE>Договора</TITLE></HEAD>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<form method = 'get' action='/SearchContract1'>";
                if (context.Request.Cookies.TryGetValue("ContractName", out string savedContract))
                {
                    HtmlString += "<input type='text' name='ContractName' placeholder='Введите номер договора' value='" + savedContract + "' />";
                }
                else
                {
                    HtmlString += "<input type='text' name='ContractName' placeholder='Введите номер договора' />";
                }
                HtmlString += "<button type='submit'>Поиск</button>" +
                "</form>" +
            "<BODY><H1>Список договоров</H1>" +
                "<TABLE BORDER=1>";
                HtmlString += "<TR>";
                HtmlString += "<TH>Номер договора</TH>";
                HtmlString += "<TH>Дата</TH>";
                HtmlString += "<TH>Номер продавца</TH>";
                HtmlString += "<TH>Номер покупателя</TH>";
                HtmlString += "<TH>Сумма сделки</TH>";
                HtmlString += "<TH>Стоимость услуги</TH>";
                HtmlString += "<TH>Сотрудник</TH>";
                HtmlString += "<TH>ФИО покупателя</TH>";
                HtmlString += "</TR>";
                foreach (var Contract in Contracts)
                {
                    HtmlString += "<TR>";
                    HtmlString += "<TD>" + Contract.ContractId + "</TD>";
                    HtmlString += "<TD>" + Contract.DateOfContract + "</TD>";
                    HtmlString += "<TD>" + Contract.SellerId + "</TD>";
                    HtmlString += "<TD>" + Contract.BuyerId + "</TD>";
                    HtmlString += "<TD>" + Contract.DealAmount + "</TD>";
                    HtmlString += "<TD>" + Contract.ServiceCost + "</TD>";
                    HtmlString += "<TD>" + Contract.Employee + "</TD>";
                    HtmlString += "<TD>" + Contract.Fiobuyer + "</TD>";
                    HtmlString += "</TR>";
                }
                HtmlString += "</TABLE>";
                HtmlString += "<BR><A href='/'>Главная</A></BR>";
                HtmlString += "<BR><A href='/Contracts'>Договора</A></BR>";
                HtmlString += "<BR><A href='/form'>Данные пользователя</A></BR>";
                HtmlString += "<BR><A href='/searchForm1'>Поиско договора по номеру через куки</A></BR>";
                HtmlString += "<BR><A href='/SeacrhForm2'>Поиско договора по номеру через сессию</A></BR>";

                HtmlString += "</BODY></HTML>";

                await context.Response.WriteAsync(HtmlString);
            });
        });
        app.Map("/SearchContract1", (appBuilder) =>
        {
            appBuilder.Run(async (context) =>
            {

                string contractName = context.Request.Query["contractName"];
                Console.WriteLine(contractName);

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1), 
                    IsEssential = true 
                };

                context.Response.Cookies.Append("contractName", contractName, cookieOptions);

                ICacheContractsService cachedContractsService = context.RequestServices.GetService<ICacheContractsService>();
                IEnumerable<Contract> Contracts = cachedContractsService.GetContractById(int.Parse(contractName));
                string HtmlString = "<HTML><HEAD><TITLE>Договора</TITLE></HEAD>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY><H1>Список договоров</H1>" +
               "<TABLE BORDER=1>";
                HtmlString += "<TR>";
                HtmlString += "<TH>Номер договора</TH>";
                HtmlString += "<TH>Дата</TH>";
                HtmlString += "<TH>Номер продавца</TH>";
                HtmlString += "<TH>Номер покупателя</TH>";
                HtmlString += "<TH>Сумма сделки</TH>";
                HtmlString += "<TH>Стоимость услуги</TH>";
                HtmlString += "<TH>Сотрудник</TH>";
                HtmlString += "<TH>ФИО покупателя</TH>";
                HtmlString += "</TR>";
                foreach (var Contract in Contracts)
                {
                    HtmlString += "<TR>";
                    HtmlString += "<TD>" + Contract.ContractId + "</TD>";
                    HtmlString += "<TD>" + Contract.DateOfContract + "</TD>";
                    HtmlString += "<TD>" + Contract.SellerId + "</TD>";
                    HtmlString += "<TD>" + Contract.BuyerId + "</TD>";
                    HtmlString += "<TD>" + Contract.DealAmount + "</TD>";
                    HtmlString += "<TD>" + Contract.ServiceCost + "</TD>";
                    HtmlString += "<TD>" + Contract.Employee + "</TD>";
                    HtmlString += "<TD>" + Contract.Fiobuyer + "</TD>";
                    HtmlString += "</TR>";
                }
                HtmlString += "</TABLE>";
                HtmlString += "<BR><A href='/'>Главная</A></BR>";
                HtmlString += "<BR><A href='/Contracts'>Договора</A></BR>";
                HtmlString += "<BR><A href='/form'>Данные пользователя</A></BR>";
                HtmlString += "<BR><A href='/searchForm1'>Поиско договора по номеру через куки</A></BR>";
                HtmlString += "<BR><A href='/SeacrhForm2'>Поиско договора по номеру через сессию</A></BR>";

                HtmlString += "</BODY></HTML>";

                await context.Response.WriteAsync(HtmlString);
            });
        });
        app.Map("/SeacrhForm2", (appBuilder) =>
        {
            appBuilder.Run(async (context) =>
            {
                ICacheContractsService cachedContractsService = context.RequestServices.GetService<ICacheContractsService>();
                IEnumerable<Contract> Contracts = cachedContractsService.GetContracts("Contracts20");
                string json = File.ReadAllText("session.json");

                string HtmlString = "<HTML><HEAD><TITLE>Учители</TITLE></HEAD>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<form method='get' action='/SearchContract2'>";
                if (context.Session.TryGetValue("SearchFormState", out byte[] searchFormStateBytes))
                {
                    var searchFormState = JsonSerializer.Deserialize<SearchFormState>(json);
                    Console.WriteLine(searchFormState.ContactId);
                    HtmlString += "<input type='text' name='ContractName' placeholder='Введите имя учителя' value='" + searchFormState.ContactId + "' />";
                }
                else
                {
                    
                    HtmlString += "<input type='text' name='ContractName' placeholder='Введите номер договора' />";
                }
                HtmlString += "<button type='submit'>Поиск</button>" +
                "</form>" +
                "<BODY><H1>Список договоров</H1>" +
                "<TABLE BORDER=1>";
                HtmlString += "<TR>";
                HtmlString += "<TH>Номер договора</TH>";
                HtmlString += "<TH>Дата</TH>";
                HtmlString += "<TH>Номер продавца</TH>";
                HtmlString += "<TH>Номер покупателя</TH>";
                HtmlString += "<TH>Сумма сделки</TH>";
                HtmlString += "<TH>Стоимость услуги</TH>";
                HtmlString += "<TH>Сотрудник</TH>";
                HtmlString += "<TH>ФИО покупателя</TH>";
                HtmlString += "</TR>";
                foreach (var Contract in Contracts)
                {
                    HtmlString += "<TR>";
                    HtmlString += "<TD>" + Contract.ContractId + "</TD>";
                    HtmlString += "<TD>" + Contract.DateOfContract + "</TD>";
                    HtmlString += "<TD>" + Contract.SellerId + "</TD>";
                    HtmlString += "<TD>" + Contract.BuyerId + "</TD>";
                    HtmlString += "<TD>" + Contract.DealAmount + "</TD>";
                    HtmlString += "<TD>" + Contract.ServiceCost + "</TD>";
                    HtmlString += "<TD>" + Contract.Employee + "</TD>";
                    HtmlString += "<TD>" + Contract.Fiobuyer + "</TD>";
                    HtmlString += "</TR>";
                }
                HtmlString += "</TABLE>";
                HtmlString += "<BR><A href='/'>Главная</A></BR>";
                HtmlString += "<BR><A href='/Contracts'>Договора</A></BR>";
                HtmlString += "<BR><A href='/form'>Данные пользователя</A></BR>";
                HtmlString += "<BR><A href='/searchForm1'>Поиско договора по номеру через куки</A></BR>";
                HtmlString += "<BR><A href='/SeacrhForm2'>Поиско договора по номеру через сессию</A></BR>";

                HtmlString += "</BODY></HTML>";

                await context.Response.WriteAsync(HtmlString);
            });
        });
        app.Map("/SearchContract2", (appBuilder) =>
        {
            appBuilder.Run(async (context) =>
            {

                string ContractName = context.Request.Query["ContractName"];

                var searchFormState = new SearchFormState
                {
                    ContactId = int.Parse(ContractName)
                };
                string json = JsonSerializer.Serialize(searchFormState);

                File.WriteAllText("session.json", json);
                var searchFormStateBytes = JsonSerializer.SerializeToUtf8Bytes(searchFormState);

                context.Session.Set("SearchFormState", searchFormStateBytes);

                 ICacheContractsService cacheContractsService = context.RequestServices.GetService<ICacheContractsService>();
                IEnumerable<Contract> Contracts = cacheContractsService.GetContractById(int.Parse(ContractName));
                string HtmlString = "<HTML><HEAD><TITLE>Договора</TITLE></HEAD>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY><H1>Список договоров</H1>" +
                   "<TABLE BORDER=1>";
                HtmlString += "<TR>";
                HtmlString += "<TH>Номер договора</TH>";
                HtmlString += "<TH>Дата</TH>";
                HtmlString += "<TH>Номер продавца</TH>";
                HtmlString += "<TH>Номер покупателя</TH>";
                HtmlString += "<TH>Сумма сделки</TH>";
                HtmlString += "<TH>Стоимость услуги</TH>";
                HtmlString += "<TH>Сотрудник</TH>";
                HtmlString += "<TH>ФИО покупателя</TH>";
                HtmlString += "</TR>";
                foreach (var Contract in Contracts)
                {
                    HtmlString += "<TR>";
                    HtmlString += "<TD>" + Contract.ContractId + "</TD>";
                    HtmlString += "<TD>" + Contract.DateOfContract + "</TD>";
                    HtmlString += "<TD>" + Contract.SellerId + "</TD>";
                    HtmlString += "<TD>" + Contract.BuyerId + "</TD>";
                    HtmlString += "<TD>" + Contract.DealAmount + "</TD>";
                    HtmlString += "<TD>" + Contract.ServiceCost + "</TD>";
                    HtmlString += "<TD>" + Contract.Employee + "</TD>";
                    HtmlString += "<TD>" + Contract.Fiobuyer + "</TD>";
                    HtmlString += "</TR>";
                }
                HtmlString += "</TABLE>";
                HtmlString += "<BR><A href='/'>Главная</A></BR>";
                HtmlString += "<BR><A href='/Contracts'>Договора</A></BR>";
                HtmlString += "<BR><A href='/form'>Данные пользователя</A></BR>";
                HtmlString += "<BR><A href='/searchForm1'>Поиско договора по номеру через куки</A></BR>";
                HtmlString += "<BR><A href='/SeacrhForm2'>Поиско договора по номеру через сессию</A></BR>";

                HtmlString += "</BODY></HTML>";

                await context.Response.WriteAsync(HtmlString);
            });
        });
        app.Run((context) =>
        {
            ICacheContractsService cachedContractsService = context.RequestServices.GetService<ICacheContractsService>();
            cachedContractsService.AddContracts("Contracts20");
            string HtmlString = "<HTML><HEAD><TITLE>Договора</TITLE></HEAD>" +
            "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
            "<BODY><H1>Главная</H1>";
            HtmlString += "<H2>Данные записаны в кэш сервера</H2>";
            HtmlString += "<BR><A href='/'>Главная</A></BR>";
            HtmlString += "<BR><A href='/Contracts'>Договора</A></BR>";
            HtmlString += "<BR><A href='/form'>Данные пользователя</A></BR>";
            HtmlString += "<BR><A href='/searchForm1'>Поиско договора по номеру через куки</A></BR>";
            HtmlString += "<BR><A href='/SeacrhForm2'>Поиско договора по номеру через сессию</A></BR>";
            HtmlString += "</BODY></HTML>";

            return context.Response.WriteAsync(HtmlString);

        });
        app.Run();
    }
    public class SearchFormState
    {
        public int ContactId { get; set; }
    }
}
