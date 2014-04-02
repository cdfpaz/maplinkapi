maplinkapi - MapLink Api sample application
------------

MapLink Api sample application - 
  is a .NET 4.5 ASP.NET/WCF/C# project, developed in response to the challenge posted by MapLink in they site: https://github.com/maplinkapi/desafio-dev-senior/ 

Solution proposed
------------

The solution proposed consists of 3 projects: 

- `RouteCalculator` - WCF WebService
- `HostRouteServer` - ASP.NET host site
- `RouteCalculatorClient` - C# Console WebService tester

RouteCalculator
------------
`RouteCalculator` is a WCF class library. It has to be hosted in an environment so that client applications may access it. We've decided to host it using IIS Express.

HostRouteServer
------------
`HostRouteServer` is an ASP.NET Website,  `HostRouteServer` is actually running inside IIS Express. 

start it from outside Visual Studio:

    "C:\Program Files\IIS Express\iisexpress"
    /path:C:\PathTo\HostRouteServer /port:2873 /clr:v4.5

note: please change "C:\Program Files" and "C:\PathTo" directory to match your system. 

Important files:
* Web.config - Web site configuration
* RouteCalculatorService.config - WCF Service configuration, contains MapLink client endpoint configuration


RouteCalculatorClient
------------
`RouteCalculatorClient` is C# console application developed to test `RouteCalculator` Webservice.

sample usage and output:

    C:\>RouteCalculatorClient.exe
    Checking route details:
            From Av Paulista, 1682 To Av Brigadeiro Faria Lima, 2232
            
    Server Results:
            Time=PT7M, Fuel Cost=0, Fuel Cost with Taxes=0, Distance=5,33
            
    Client is exiting

