using System;
using System.Linq;
using Raven.Client.Documents;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace ChampionSelectionAnalyzer.JobRunner
{
    // TODO also start RavenDb from code, or powershell
    internal class RavenDb
    {
        private const string DatabaseName = "ChampionSelectionAnalyzer";
        private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

        public static IDocumentStore Store => store.Value;

        private static IDocumentStore CreateStore()
        {
            return new DocumentStore
            {
                Urls = new[] { "http://127.0.0.1:8080" },
                Database = DatabaseName
            }.Initialize();
        }

        public static void CreateDatabaseIfNotExists()
        {
            var operation = new GetDatabaseNamesOperation(0, 25);
            var databaseNames = Store.Maintenance.Server.Send(operation);
            var applicationDatabaseExists = databaseNames.Contains(DatabaseName);

            if (!applicationDatabaseExists)
            {
                var databaseRecord = new DatabaseRecord(DatabaseName);
                var createDbOperation = new CreateDatabaseOperation(databaseRecord);

                Store.Maintenance.Server.Send(createDbOperation);
            }
        }
    }
}
