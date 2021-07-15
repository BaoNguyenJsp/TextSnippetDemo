# TextSnippetDemo
Demo a simple text snippet feature

## Features
+ EF Core with SQL
+ Identity with SQL
+ Authentication with JWT
+ Distributed Cache with SQL
+ CQRS and MediatR
+ Custom filter
+ Lazy loading Routing
+ Rxjs
+ NgRx Store and NgRx Effect
+ Authentication interceptor
+ Custom pipe
+ Regex
+ Sanitizer in Angular

## Prerequirements
Install .NET SDK 5.0 or above: https://dotnet.microsoft.com/download/dotnet/5.0
Install nodejs and npm: https://nodejs.org/en/download/
Install Angular Cli: 
```bash
$ npm install -g @angular/cli
```

## Server side - .NET 5.0 API
1. Go to backend\TextSnippetDemo
2. Run
```bash
$ dotnet restore
```
3. Run
```bash
$ dotnet ef database update --startup-project TextSnippetDemo.API --project TextSnippetDemo.Infra
```
4. Run
```bash
$ dotnet run --project .\TextSnippetDemo.API\TextSnippetDemo.API.csproj
```


## Client side - Angular 12
1. Go to frontend\textsnippetclient
2. Run 
```bash
$ npm install
```
3. Run
```bash
$ npm start
```
