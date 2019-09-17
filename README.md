# Transavia Airports
![Transavia missing image](docs/Transavia.png?raw=true "Transavia")
Transavia technical assessment

# How to use

## Prerequisites
* docker 
* docker-compose
* .NET Core SDK
* PowerShell

## Before runing scripts
* Run cmd 
* Navigate to the app root folder
* ...

## Run 
* Next command will build application, create docker images, run docker-compose up and seed DB by data provided by http feed.
```
     .\build.ps1 -Target Run
```
* MVC is accesable on http://localhost:8888/
* API is accesable with swagger on http://localhost:7777/swagger
* Redis GUI is accesable on http://localhost:8081/

## Seed DB
Before running it be sure that services are running. 
Execute 
```
     .\build.ps1 -Target Run-Services
```
first.
* Next command will seed database by data provided by http feed.
```
     .\build.ps1 -Target Run-DBSeeder
```

## Performance Tests
The test command will run application, seed DB, build performance tests and run them.
```
     .\build.ps1 -Target PerformanceTests
```

# What was used
## Tech Stack:
* ASP.NTET Core
* EF
* Dapper
* MS SQL Server
* Redis
* Redis-commander
* Docker
* Docker Compose
* Cake
* Swagger
* Automapper
* MediatR
* PowerShell
* NUnit
* NSubstitute

## Principles and patterns:
* SOLID
* REST
* CQRS
* MVC
* Dependency Injection
* Repository pattern
* Factory pattern
* Unit of Work

# License

This is open source software, licensed under the terms of MIT license. 
See [LICENSE](LICENSE) for details.
