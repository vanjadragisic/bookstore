<Query Kind="Program">
  <Reference Relative="..\Bookstore.dll">Bookstore.dll</Reference>
  <Reference Relative="..\Bookstore.deps.json">Bookstore.deps.json</Reference>
  <Reference Relative="..\Bookstore.runtimeconfig.json">Bookstore.runtimeconfig.json</Reference>
  
  <Reference Relative="..\Autofac.dll">..\Autofac.dll</Reference>
  <Reference Relative="..\EntityFramework.dll">..\EntityFramework.dll</Reference>
  <Reference Relative="..\EntityFramework.SqlServer.dll">..\EntityFramework.SqlServer.dll</Reference>
  <Reference Relative="..\Microsoft.CodeAnalysis.CSharp.dll">..\Microsoft.CodeAnalysis.CSharp.dll</Reference>
  <Reference Relative="..\Microsoft.CodeAnalysis.dll">..\Microsoft.CodeAnalysis.dll</Reference>
  <Reference Relative="..\Microsoft.Extensions.Localization.Abstractions.dll">..\Microsoft.Extensions.Localization.Abstractions.dll</Reference>
  <Reference Relative="..\Microsoft.Win32.SystemEvents.dll">..\Microsoft.Win32.SystemEvents.dll</Reference>
  <Reference Relative="..\Newtonsoft.Json.dll">..\Newtonsoft.Json.dll</Reference>
  <Reference Relative="..\NLog.dll">..\NLog.dll</Reference>
  <Reference Relative="..\Oracle.ManagedDataAccess.dll">..\Oracle.ManagedDataAccess.dll</Reference>
  <Reference Relative="..\Rhetos.Compiler.dll">..\Rhetos.Compiler.dll</Reference>
  <Reference Relative="..\Rhetos.Compiler.Interfaces.dll">..\Rhetos.Compiler.Interfaces.dll</Reference>
  <Reference Relative="..\Rhetos.Configuration.Autofac.dll">..\Rhetos.Configuration.Autofac.dll</Reference>
  <Reference Relative="..\Rhetos.DatabaseGenerator.DefaultConcepts.dll">..\Rhetos.DatabaseGenerator.DefaultConcepts.dll</Reference>
  <Reference Relative="..\Rhetos.DatabaseGenerator.dll">..\Rhetos.DatabaseGenerator.dll</Reference>
  <Reference Relative="..\Rhetos.DatabaseGenerator.Interfaces.dll">..\Rhetos.DatabaseGenerator.Interfaces.dll</Reference>
  <Reference Relative="..\Rhetos.Deployment.dll">..\Rhetos.Deployment.dll</Reference>
  <Reference Relative="..\Rhetos.Deployment.Interfaces.dll">..\Rhetos.Deployment.Interfaces.dll</Reference>
  <Reference Relative="..\Rhetos.Dom.DefaultConcepts.dll">..\Rhetos.Dom.DefaultConcepts.dll</Reference>
  <Reference Relative="..\Rhetos.Dom.DefaultConcepts.Interfaces.dll">..\Rhetos.Dom.DefaultConcepts.Interfaces.dll</Reference>
  <Reference Relative="..\Rhetos.Dom.dll">..\Rhetos.Dom.dll</Reference>
  <Reference Relative="..\Rhetos.Dom.Interfaces.dll">..\Rhetos.Dom.Interfaces.dll</Reference>
  <Reference Relative="..\Rhetos.Dsl.DefaultConcepts.dll">..\Rhetos.Dsl.DefaultConcepts.dll</Reference>
  <Reference Relative="..\Rhetos.Dsl.dll">..\Rhetos.Dsl.dll</Reference>
  <Reference Relative="..\Rhetos.Dsl.Interfaces.dll">..\Rhetos.Dsl.Interfaces.dll</Reference>
  <Reference Relative="..\Rhetos.Dsl.Parser.dll">..\Rhetos.Dsl.Parser.dll</Reference>
  <Reference Relative="..\Rhetos.Extensibility.dll">..\Rhetos.Extensibility.dll</Reference>
  <Reference Relative="..\Rhetos.Extensibility.Interfaces.dll">..\Rhetos.Extensibility.Interfaces.dll</Reference>
  <Reference Relative="..\Rhetos.Host.Net.dll">..\Rhetos.Host.Net.dll</Reference>
  <Reference Relative="..\Rhetos.Logging.dll">..\Rhetos.Logging.dll</Reference>
  <Reference Relative="..\Rhetos.Logging.Interfaces.dll">..\Rhetos.Logging.Interfaces.dll</Reference>
  <Reference Relative="..\Rhetos.Persistence.dll">..\Rhetos.Persistence.dll</Reference>
  <Reference Relative="..\Rhetos.Persistence.Interfaces.dll">..\Rhetos.Persistence.Interfaces.dll</Reference>
  <Reference Relative="..\Rhetos.Processing.DefaultCommands.dll">..\Rhetos.Processing.DefaultCommands.dll</Reference>
  <Reference Relative="..\Rhetos.Processing.DefaultCommands.Interfaces.dll">..\Rhetos.Processing.DefaultCommands.Interfaces.dll</Reference>
  <Reference Relative="..\Rhetos.Processing.dll">..\Rhetos.Processing.dll</Reference>
  <Reference Relative="..\Rhetos.Processing.Interfaces.dll">..\Rhetos.Processing.Interfaces.dll</Reference>
  <Reference Relative="..\Rhetos.Security.dll">..\Rhetos.Security.dll</Reference>
  <Reference Relative="..\Rhetos.Security.Interfaces.dll">..\Rhetos.Security.Interfaces.dll</Reference>
  <Reference Relative="..\Rhetos.Utilities.dll">..\Rhetos.Utilities.dll</Reference>
  <Reference Relative="..\Rhetos.Utilities.Interfaces.dll">..\Rhetos.Utilities.Interfaces.dll</Reference>
  <Namespace>Autofac</Namespace>
  <Namespace>Oracle.ManagedDataAccess.Client</Namespace>
  <Namespace>Rhetos</Namespace>
  <Namespace>Rhetos.Configuration.Autofac</Namespace>
  <Namespace>Rhetos.Dom</Namespace>
  <Namespace>Rhetos.Dom.DefaultConcepts</Namespace>
  <Namespace>Rhetos.Dsl</Namespace>
  <Namespace>Rhetos.Dsl.DefaultConcepts</Namespace>
  <Namespace>Rhetos.Logging</Namespace>
  <Namespace>Rhetos.Persistence</Namespace>
  <Namespace>Rhetos.Processing</Namespace>
  <Namespace>Rhetos.Processing.DefaultCommands</Namespace>
  <Namespace>Rhetos.Security</Namespace>
  <Namespace>Rhetos.Utilities</Namespace>
  <Namespace>System.Data.Entity</Namespace>
  <Namespace>System.DirectoryServices</Namespace>
  <Namespace>System.Runtime.Serialization.Json</Namespace>
  <Namespace>System.Xml.Serialization</Namespace>
</Query>

void Main()
{
    ConsoleLogger.MinLevel = EventType.Info; // Use EventType.Trace for more detailed log.
    string rhetosHostAssemblyPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), @"..\Bookstore.dll");
    using (var scope = LinqPadRhetosHost.CreateScope(rhetosHostAssemblyPath))
    {
        var context = scope.Resolve<Common.ExecutionContext>();
        var repository = context.Repository;

        // Query data from the `Common.Claim` table:
        
        var claims = repository.Common.Claim.Query()
            .Where(c => c.ClaimResource.StartsWith("Common.") && c.ClaimRight == "New")
            .ToSimple(); // Removes ORM navigation properties from the loaded objects.
            
        claims.ToString().Dump("Common.Claims SQL query");
        claims.Dump("Common.Claims items");
        
        // Add and remove a `Common.Principal`:
        
        var testUser = new Common.Principal { Name = "Test123", ID = Guid.NewGuid() };
        repository.Common.Principal.Insert(new[] { testUser });
        repository.Common.Principal.Delete(new[] { testUser });
        
        // Print logged events for the `Common.Principal`:
        
        repository.Common.LogReader.Query()
            .Where(log => log.TableName == "Common.Principal" && log.ItemId == testUser.ID)
            .ToList()
            .Dump("Common.Principal log");
            
        Console.WriteLine("Done.");
        
        //scope.CommitAndClose(); // Database transaction is rolled back by default.
    }
}
