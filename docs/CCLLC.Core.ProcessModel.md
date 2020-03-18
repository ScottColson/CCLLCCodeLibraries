# CCLLC.Core.ProcessModel.

Objects in the CCLLC.CoreProcessModel define interfaces and provide some default implementations for 
creating processes that are independent of data access implementations and therefore more portable and extensible. 

Processes built on this model generally have three distinct architectural layers: 

- Process Orchestrator - In most cases this level of the process is reacting to 
some event and is dictating the response to that event. So this may be the event 
handler section of a CDS plugin, an Azure Function, an API etc.
- Business Logic - This is the core of what the process is supposed to do. It reacts to requests
from the orchestrator and when needed makes requests to the data layer. The business logic layer
defines data types as POCO objects and/or interfaces. It also typically defines one or more "data
connectors" that define the requests sent to the data layer.
- Data Layer - this is where the Business Logic interacts with the underlying 
data system. The data layer responds with objects based on data model interfaces rather than
specific entities.

###### Dependencies

- [CCLLC.Core.IocContainer](/CCLLC.Core.IocContainer.md)

###### Nuget Packages

- [Assembly - CCLLC.Core.ProcessModel](https://www.nuget.org/packages/CCLLC.Core.ProcessModel/)
- [Source Code - CCLLC.Core.ProcessModel.Sources](https://www.nuget.org/packages/CCLLC.Core.ProcessModel.Sources/)


# Why Should I Do This?

For someone like me, whose career evolved around SQL Server, Microsoft CRM, and now CDS and 
now the Power Platform, the idea of separating your logic code from your entity structure might 
seem a bit silly. Until a few years ago I agreed with you.

What I have discovered as I started working on larger projects is that there are more unknowns 
in the beginning of the project than you think there are. Even when you absolutely know your 
going to use a certain technology, such as Model Driven PowerApps and CDS, there is still a lot
of value in separating your business logic from you data storage and access. Here are some real world
examples from my recent experience where making the separation in the beginning of the project would 
have paid dividends later:

- Changing field types and names on an entity. If you use early bound this can be easy to fix but
still may required making changes in many different areas of the code base. However, is business
logic is separated from data access the scope of changes is generally much smaller and easier to test.
- Duplicating a processes implemented in a CDS plugins in other process spaces. For me this became
a code management problem in a Windows Service that implemented batch processes, some of which were
already implemented as plugin processes in a CRM 2016 deployment. Had we made the original implementation
process and data agnostic it would have been much easier to reuse the business logic and we could
have easily switched to the REST API for data access. 

One other, and perhaps, more important personal outcome of switching to this model has been that 
my solutions are easier to read, better thought out, and far more testable.

All that being said, it does not make sense to use this model for trivial processes, but there
are many, many, non-trivial processes in the world.

# How It Works

This module addresses six items that I routinely need in when coding a business process for
either a CDS plugin, An Azure Function, an IIS API, or a Windows Service:

1. An execution context that gives my process access to the tools and data it needs.
2. A system agnostic way to point to a record.
3. A system agnostic way to pass data access components through the process to the data layer 
that will use them.
4. A cache to store items that don't change often and therefore cut down on unneeded trips 
through the data layer.
5. A settings provider that allows reconfiguration of parts of the process when desired. 
6. An easy way to capture tracing and track particular events to a logging system. 

## Process Execution Context

The process execution context is defined by the _CCLLC.Core.IProcessExecutionContext_ interface. Code
built on the this module assumes that any "execution request" from the calling process event handler (e.g.
plugin or Azure Function) will pass in an execution context that implements this interface. The process
will use the items provided in the execution context to complete the work. The _IProcessExecutionContext_
provides getters or methods for the following:

1. A handle to the data service that is passed from the process orchestration level, through the business
logic level, to the data layer level. The orchestration and data layers share some knowledge of how
data access works, but the business layer is ignorant and literally has now means to perform CRUD operations.
2. Access to a cache that can store and retrieve typed objects.
3. Access to a setting provider that provides a simple name-value-pair settings access
4. Access to an [IoC/DI container] (CCLLC.Core.IoCContainer.md) for creating dependencies that were not 
directly injected into the process at object creation or execution.
5. A mechanism to log trace messages to a logging system.
6. A mechanism to track named events to a logging system.
7. A mechanism to track exceptions to a logging system.


## Data Access and Data Pointers

The _CCLLC.ProcessModel.IDataService_ interface provides the handle for passing a data service or connection
from the orchestration layer to the data layer. The data layer needs to know how to convert the underlying 
object into an interface that can actually interact with data.

The _CCLLC.ProcessModel.IRecordPointer_ interface provides a system agnostic way to point to a records in a data
table based system. The _IRecordPointer_ provides getters for Record Type (e.g. entity name) and Id. This is 
analogous to the EntityReference provided by the Microsoft.Xrm.Sdk. However, _IRecordPointer_ is not locked in 
to GUID based unique record identifiers. It can represent alternate pointer types such as int, or strings.

The _CCLLC.ProcesModel.RecordPointer_ class provides a default implementation of the _IRecordPointer_ interface.

## Cache

The _CCLLC.Core.ICache_ interface defines the cache implementation used in the process model and the 
_CCLLC.Core.DefaultCache_ implements that interface using the System.Runtime MemoryCache.  

## Settings Provider

Access to name-value paired settings is provided through the _CCLLC.Core.ISettingsProvider_ interface.
_CCLLC.Core.SettingsProvider_ provides the default implementation of that interface. The default settings 
provider is created using the _CCLLC.Core.SettingsProviderFactory_ class.

The _GetValue\<T>(key, defaultValue)_ method supports type casting the string value into most common data types 
such as int, decimal, and Boolean. These type conversions are based on the Convert.ChangeType method. In addition to 
these standard conversions, the following specific conversions are supported:
- _GetValue\<TimeSpan?>_ will convert a numeric setting value to a nullable TimeSpan object assuming the number 
represents the number of 'seconds' for the TimeSpan.
- _GetValue\<string[]>_ will convert the value to a string array assuming comma or semicolon value delineations. 

The _SettingsProviderFactory_ requires a data connector that implements the _CCLLC.Core.SettingsProviderDataConnector_ 
interface. This data connector implementation must be registered in the IoC Container as in the orchestration 
layer of the overall process. The container will create the data connector and inject it into the factory which
uses it when creating a loaded setting provider.

The factory caches the created settings provider object for 15 minutes. This default cache timeout can be altered
by creating a setting in the setting provider named "CCLLC.SettingsCacheTimeOut" with a numeric value indicating
the number of seconds to cache the settings. 
 

## Tracing and Tracking

_IProcessExectionContext_ provides four method signatures for capturing logging information:

- _Trace(severityLevel, message, args)_ writes a message with severity level flagging and optional 
string formatting arguments to the logging system.
- _Trace(message, args)_ writes a message with severity level 'information' and optional string 
formatting arguments to the logging system.
- _TrackEvent(name)_ captures the name of a specific event in the logging system, depending on exact implementation 
this might just be a specifically formatted trace message.
- _TrackException(exception)_ captures information about an code execution exception to the logging system.;

The actual implementation of these methods and therefore the outcome of the method calls is left to the implementing
class.