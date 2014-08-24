ONE PIECE FRAMEWORK 
===============

This is framework used anime ONE PIECE as the framework namespace. 
It is about an self .net libraries which collected in a long period time in the work.
If used deeply, will worked efficiency, take everything easy, to be a literary coder!

## NuGet

Project maintaned by NuGet, this is my first use managed tools to manage project by myself.
Because nuget package version updated frequently, this appearance will cause incompatibility,
so I'll record every package version as the reference when project builded.

Framework Contains 6-7 parts, ie:

  - [Framework/Core]
  - [Framework/Log]
  - [Framework/Redis]
  - [Framework/Cache]
  - [Framework/SubSonic]
  - [Framework/Web]
  - [Framework/Test]
    
### Core

Include Project Const, Data Set handle for sql, various extension method and helper class library,
usual signature ways, linq expression extension and core by the ioc realized basic assembly.
will add design pattern by later.

This library only use **System.ComponentModel.DataAnnotation** and **System.Configuration** except common lib.


### Log

Include common write log class library, here is **Utilities.Log4Net** and **NLog**.
Nuget package install:

    PM> Install-Package log4net

    PM> Install-Package NLog

log4net current verison is [2.0.3](https://www.nuget.org/packages/log4net/), NLog current version is [3.1.0](https://www.nuget.org/packages/NLog/)
Don't forget configuration for self.


### Redis

Use No Sql Database manager hotter data get and storge, take full useful k-v collection.
Nuget Package install:

    PM> Install-Package ServiceStack.Redis -Version 3.9.71
    PM> Install-Package Newtonsoft.Json

Don't forget add version [3.9.71](https://www.nuget.org/packages/ServiceStack.Redis/3.9.71) because the higher version will confict, 4.0 version try when free time
packages current version:
ServiceStack.Common==>3.9.11, ServiceStack.Redis==>3.9.71, erviceStack.Text==>4.0.30
ServiceStack.Interfaces dll in ServiceStack.Common folder.
Newtonsoft.Json current verison is [6.0.4](https://www.nuget.org/packages/Newtonsoft.Json/)


### Cache

Cache is important part in web, so as single class library manage.
As usual memory cache, no sql cache contains redis|memcache|and so so.
Either AOP technology can be used easier for cache. Here is SNAP Open sources and Structuremap Container execute.
Nuget Package install:

    PM> Install-Package structuremap
    PM> Install-Package SNAP
    PM> Install-Package ServiceStack.Redis -Version 3.9.71
    PM> Install-Package Castle.Core

structuremap current version is [3.1.0.133](https://www.nuget.org/packages/structuremap/)
SNAP current version is [1.8.3](https://www.nuget.org/packages/SNAP/) **it had dependencies contains fasterflect, Castle.Core, CommonServiceLocator**, the Castle.Core version is too lower, need to update.
Castle.Core current version is higher [3.3.0](https://www.nuget.org/packages/Castle.Core/)


### SubSonic

This is ADO.NET connect database tools, because the original coder stop it to Microsoft develop EF which is bigger and usefuler. so the latest version is 3.0.0.4 and has a bit little bug, is not stable.
So, the package from nuget need to replace with local lib. and at the same time the Castle.Core choose the latest version.
Nuget Package install:

    PM> Install-Package SubSonic
    PM> Install-Package Castle.Core

SubSonic current version is [3.0.0.4](https://www.nuget.org/packages/SubSonic/)


#### Web


This class library is realized controller registry, service boot strap, handle http requst and route, html display and so so.
The class lib namespace most use core because easy to build same namespace class.
On the other side model map and object refection method is too many.
Nuget Package install:

    PM> Install-Package AutoMapper -Version 3.2.1
    PM> Install-Package Newtonsoft.Json
    PM> Install-Package SNAP
    PM> Install-Package SNAP.StructureMap
    PM> Install-Package structuremap
    PM> Install-Package Castle.Core
    PM> Install-Package MvcPaging

AutoMapper current version is [3.2.1](https://www.nuget.org/packages/AutoMapper/3.2.1) is stable
SNAP.StructureMap version is [1.8.3](https://www.nuget.org/packages/SNAP.StructureMap/)
MvcPaging current version is [2.1.1](https://www.nuget.org/packages/MvcPaging/) is too higher ,use 1.0 version to replace.
and add local lib reference for Mircosoft.Web.Mvc version is 2.0.0
