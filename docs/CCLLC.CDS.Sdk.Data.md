# CCLLC.CDS.Sdk.Data

Provides data access helpers when using the Microsoft.Xrm.Sdk 
IOrganizationService to access CDS data.

###### Nuget Packages

- [Assembly - CCLLC.CDS.Sdk.Data](https://www.nuget.org/packages/CCLLC.CDS.Sdk.Data/)
- [Source Code - CCLLC.CDS.Sdk.Data.Sources](https://www.nuget.org/packages/CCLLC.CDS.Sdk.Data.Sources/)

## Fluent Query Expression Builder

The FluentQueryExpressionBuilder is used to build standard CDS Query Expressions
using a Fluent implementation with data filtering and joining syntax that is similar 
to SQL. Credit to Florian Kronert's <a href="https://github.com/DigitalFlow/Xrm-Fluent-Query/">Xrm-Fluent-Query</a> 
query project and Daryl LaBar's <a href="https://github.com/daryllabar/DLaB.Xrm/">DLaB.Xrm</a> 
framework project for inspiration and code examples.

```c#
using CCLLC.CDS.Sdk;

var qryExpression = new QueryExpressionBuilder<Account>()
                    .Select(cols => new { cols.AccountNumber, cols.Name })
                    .WhereAll(a => a
                        .IsActive()
                        .Attribute("accountnumber").IsNotNull())
                    .LeftJoin<Contact>("primarycontactid", "contactid", c => c
                        .With.Alias("pc")
                        .Select("fullname")
                        .WhereAll(c1 => c1.IsActive())
                        .InnerJoin<SystemUser>("ownerid","systemuserid", u => u
                            .With.Alias("owner")
                            .Select(cols => new { cols.SystemUserId } )))
                    .Build();


```

## Executable Fluent Query

The ExecutableFluentQuery is used to directly retrieve typed CDS data based on a
Fluent Query.

```C#

using CCLLC.CDS.Sdk;

IList<Account> accounts = new ExecutableFluentQuery<Account>(service)
                    .Select(cols => new { cols.AccountNumber, cols.Name })
                    .WhereAll(a => a
                        .IsActive()
                        .Attribute("accountnumber").IsNotNull())
                    .LeftJoin<Contact>("primarycontactid", "contactid", c => c
                        .With.Alias("pc")
                        .Select("fullname")
                        .WhereAll(c1 => c1.IsActive())
                        .InnerJoin<SystemUser>("ownerid", "systemuserid", u => u
                             .With.Alias("owner")
                             .Select(cols => new { cols.SystemUserId })))
                    .RetrieveAll();

``` 

## Extension Methods

This module includes the following extensions for the Microsoft.Xrm.Sdk IOrganizationService:

##### Query

Provides direct access to the ExecutableFluentQuery for simplified data queries.

```C#

 var records = orgService.Query<Account>()
                .Select(cols => new { cols.Name, cols.AccountNumber })
                .WhereAll(e => e
                    .IsActive()
                    .Attribute("name").Is<string>(ConditionOperator.BeginsWith,"C"))
                .OrderByAsc("name","accountnumber")
                .Retrieve();

var contact = orgService.Query<Contact>()
                    .WhereAll(c => c
                        .IsActive()
                        .Attribute("firstname").IsEqualTo("Scott"))                        
                    .FirstOrDefault();

```

##### GetRecord

Provides a concise method to retrieve a single record based on its record id.

```C#

// Early bound with all fields by default
var earlyBoundRecord = orgService.GetRecord<Account>(accountId);

// Late bound with selected fields based on field names.
var lateBoundRecord = orgService.GetRecord(accountId, "name", "accountnumber");

// Early bound with selected fields based on projection.
earlyBoundRecord = orgService.GetRecord<Account>(accountId, cols => new { cols.Id, cols.Name, cols.AccountNumber });

```
