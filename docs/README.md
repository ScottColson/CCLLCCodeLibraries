This project provides standardized components for coding processes in PowerApps plugins and Azure functions. 

### Built for Extensibility
A key design principle of this project is extensibility through architectural 
standardization. It should be easy to modify or enhance processes that use the 
various components created in this project without having to make large scale 
changes. To that end, the code for this project adheres to the concepts of 
coding to an interface and inversion of control. 

If the implementation for a class doesn't meet your needs, you can change it 
either through inheritance and overrides, or by writing a completely different 
implementation for the defined interface. Either way, register your new implementation
 in the process IoC container and your done. Simple.

### Shared Libraries

This project is segregated into various related projects that consist of one or 
more Shared Projects that can be consumed in any C# project based that supports 
the Shared Project model.

The **CCLLC.Telementry** solutions provides the components needed to capture 
telemetry information for logging in an external system. It is a sandbox-safe 
implementation of a **_subset_** of the [Microsoft\Applicationinsights-dotnet Sdk][l_sdk]. 
It is fully compatible with [Application Insights][l_ai], but can also be extended 
to connect to custom telemetry system endpoints with alternate telemetry serialization schemes.

 [l_sdk]:(https://github.com/Microsoft/ApplicationInsights-dotnet)
 [l_ai]:(https://azure.microsoft.com/en-us/services/application-insights/)

The **CCLLC.Core** solution provides the following solution components. Each component is 
based on defined interfaces and at least one optional implementation that can be modified 
through inheritance:

- [CCLLC.Core.IocContainer](/docs/CCLLC.Core.IocContainer.md) : A lightweight inversion of control container implementation that is 
used within process to inject dependencies. Using a container simplifies maintenance and 
extensibility of any created processes.

- CCLLC.Core.Encryption: A standardized encryption implementation. 

- CCLLC.Core.Net: A standardized implementation for building HTTP web requests. This module 
provides an interface and implementation that wraps the System.Net HttpWebRequest which 
results in code that can be unit tested because the web request execution process can be mocked. 

- CCLLC.Core.Net.Instrumented: An enhanced implementation for making HTTP web requests that includes 
dependency telemetry capture.

- CCLLC.Core.Serialization: A standardized implementation for serializing objects to JSON or XML. 
The default implementations are based on System.Net DataContracts.

- CCLLC.Core.RestClient: A standardized implementation for using POCO objects to interact with 
REST based web services.

- [CCLLC.Core.ProcessModel](/docs/CCLLC.Core.ProcessModel.md) : A standardized, data agnostic, execution context model for creating 
processes independently of the platform that will ultimately host the process. Building to 
this model makes it easy to move business logic between different process implementation platforms
(e.g. CDS plugin or Azure Function.)

The **CCLLC.CDS** solution provides the following components that provide Power Apps/Common Data 
Service platform specific implementations of the **CCLLC.Core.ProcessModel** and related extensions.

- CCLLC.CDS.Sdk: Provides a standardized plugin development framework implementation based on 
CCLLC.Core interfaces. This allows any process created against the CCLLC.Core.Process model 
to operate as a CDS plugin component.

- CCLLC.CDS.Sdk.Instrumented: Provides the CCLLC.CDS.Sdk plugin framework with telemetry data capture.

- CCLLC.CDS.Sdk.SearchUtilities: Provides a standardized framework to implement alias searches 
using data in related entities. 

### Nuget Packages

In addition to simply downloading and consuming the Shared Projects, each project is also being distributed through
Nuget as either an assembly and as a source package using the project name (e.g. CCLLC.Core.Encryption) for assembly
distribution and project name with .Sources suffix (e.g. CCLLC.Core.Encryption.Sources) for source code. 

All source code packages are installed as folders under the projects App_Packages folder._

