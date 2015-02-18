# Loosely coupled MVC solution structure

### About

December 2013 – February 2014

This is an presentation for building scalable, extendable and easy to maintain project structure. It follows these practices/patterns:
* [Loose coupling](http://en.wikipedia.org/wiki/Loose_coupling)
* [Single responsibility principle](http://en.wikipedia.org/wiki/Single_responsibility_principle)
* [Dependency Injection](http://stackoverflow.com/questions/130794/what-is-dependency-injection)
* [Service layer pattern](http://programmers.stackexchange.com/questions/162399/how-essential-is-it-to-make-a-service-layer)
* [Repository pattern](http://msdn.microsoft.com/en-us/library/ff649690.aspx)  
 
It is based on my experience with architecting/extending/working on applications with such structures. The presentation is accompanied with basic solution, which shows the practices in action and which I use as template when I design projects based on these patterns.

English version: [LooselyCoupledMVCStructure_EN.pdf](https://github.com/raste/LooselyCoupledStructure/blob/master/LooselyCoupledMVCStructure_EN.pdf)  
Bulgarian version: [LooselyCoupledMVCStructure_BG.pdf](https://github.com/raste/LooselyCoupledStructure/blob/master/LooselyCoupledMVCStructure_BG.pdf)

### Technologies used in code template

.NET, MVC, C#, Entity Framework - Code First, [Ninject](http://www.ninject.org/), [Glimpse](http://getglimpse.com/), [Elmah](https://code.google.com/p/elmah/)

### Poke/Edit

In order to open the solution you will need Visual Studio 2012 or greater. To run it, you will need also Microsoft SQL Server.

The application uses Code First approach with databases, which means that the database will be created on first run. But before that you need to configure the connection strings.

Locate in [web.config](https://github.com/raste/LooselyCoupledStructure/blob/master/Source/Web/Web.config) and [app.config](https://github.com/raste/LooselyCoupledStructure/blob/master/Source/DB/App.config) the following lines:
```
<connectionStrings>
    <add name="DBContext" providerName="System.Data.SqlClient" connectionString="Server=(local);Database=MVCTemplate;Integrated Security=True;" />
    <add name="ElmahConnectionString" providerName="System.Data.SqlClient" connectionString="Server=(local);Database=MVCTemplate;Integrated Security=True;" />
  </connectionStrings>
```
Replace `(local)` in `Server=(local);` with the name of your SQL Server instance. You can substitute `MVCTemplate` in `Database=MVCTemplate;` if you want to change the name of the created database.

When you build the solution the needed packages will be automatically downloaded from NUGET. If that doesn't happen, go to `TOOLS > NuGet package Manager > Package Manager Settings > check Allow NuGet to download missing packages`. If that doesn't help or some of the packages cannot be downloaded, get [packages.zip](https://github.com/raste/LooselyCoupledStructure/blob/master/Packages/packages.zip) and extract it in the directory of the solution (this is archive of the used packages).

### Schemes in presentation

![alt text](https://github.com/raste/LooselyCoupledStructure/blob/master/screenshots/Structure.png "Solution structure")

---

![alt text](https://github.com/raste/LooselyCoupledStructure/blob/master/screenshots/Operation.png "Operation dependencies")

--- 

![alt text](https://github.com/raste/LooselyCoupledStructure/blob/master/screenshots/Extensions.png "Lazy loading")


