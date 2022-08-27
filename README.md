
# Stock Market Scrapper

This is a mainly a Web Scrapper project. We scrapped data from 
[Dhaka Stock Exchange](https://www.dsebd.org/) website. For this we implemented a
Worker Service project. Basically, it runs as a background service.

## Installation

Firstly, clone the project-
```
https://github.com/nayeemsweb/Stock-Market-Scrapper.git
```
Secondly, Open the project in Visual Studio by running the `StockData.sln` solution file - 
```
cd Stock-Market-Scrapper/src/StockData
```
Thirdly, From the `Tools` menu go to `NuGet Package Manager` option and click to
`Package Manager Console`. Now, update migration using the following command - 
```
dotnet ef database update --project StockData.Service --context StockDataScrapingDbContext
```
This will create a database named `StockDataDb` in your SQL Server (actually SSMS) and
also the table(s) accordingly.

⚠️ ***Your must have `SQL Server` and* `SQL Server Management Studio` 
installed on your machine.***


    
## Environment Variables

In the `Stock-Market-Scrapper/src/StockData/StockData.Service` path 
there is a file named `appsettings.json`. 
If you face in updating the migration then configure this line - 
```
"DefaultConnection": "Server=.\\SQLEXPRESS;Database=StockDataDb;Trusted_Connection=True;"
```
Keep the same `DefaultConnection` value in the `appsettings.json` file of 
`Stock-Market-Scrapper/src/StockData/StockData.Worker` project. 
You may change the `Server` vaule according to your configuration.


## Tech Stack

**Web Scrapper:** [HTML Agility Pack](https://html-agility-pack.net/) |
[NuGet Package](https://www.nuget.org/packages/HtmlAgilityPack/)

**Backend:** ASP.NET (Core) 6, Worker Service, Entity Framework (Core), Sql Server

**Logger:** Serilog

**Dependency Injection:** Autofac

**Design Patterns:** Repository & Unit of Work

**Architecture:** Layered Architecture (UI, Business Logic & Data Access Layer)




## Features

- Web Scrapper
- Runs in the background (Service)
- Stores data in the database


## Support

❤️ If you do like my work, hit the ⭐️ button above. ❤️


## License

[MIT](https://choosealicense.com/licenses/mit/)

