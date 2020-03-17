# CCLLC.Core.IocContainer

The IoCContainer is a lightweight container implementation that supports 
constructor [dependency injection](https://stackify.com/dependency-injection-c-sharp/). 

The _CCLLC.Core.IReadOnlyIocContainer_ interface provides a _Resolve\<T>()_ method that searches
the container for a registered implementation for the interface type _T_ and then returns that implementation
with all constructor dependencies created and injected.

The _CCLLC.Core.IocContainer_ interface extends _IReadOnlyIocContainer_ to add the _Implement\<T>()_ method 
initiate the registration of an implementation. _Implement\<T>()_ is the start of a fluent expression that 
relies on a the _.Using\<C>() method do define the implementing class. 

For example _Container.Implement\<IFoo>().Using\<Foo>()_ directs the container to use class _Foo_ to resolve a
request for interface _IFoo_.

The _Implement\<C>()_ method supports two fluent modifiers. The _AsSingleInstance()_ modifier tells the container
to create only one instance of the implementing class and pass it out for all requests. For example 
_Container.Implement\<IFoo>().Using\<Foo>().AsSingleInstance()_ directs the container to always return the same
instance of _Foo_.

If code attempts to register more than one implementation for a specified interface, the container honors the first
registration and ignores all additional implementation instructions for that interface. The _WithOverwrite()_ 
modifier is an explicit means to indicate that the new implementation takes priority.


