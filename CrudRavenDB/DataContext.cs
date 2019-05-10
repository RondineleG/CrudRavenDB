using Raven.Client.Documents;
using System;

namespace CrudRavenDB
{
    public class DataContext
    {
        private static readonly Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);
        public static string[] Urls { get; set; }
        public static string DatabaseName { get; set; }

        public static IDocumentStore Store
        {
            get { return store.Value; }
        }

        private static IDocumentStore CreateStore()
        {
            var documentStore = new DocumentStore
            {
                Urls = Urls,
                Database = DatabaseName,
            };
            documentStore.Conventions.IdentityPartsSeparator = "-";

            documentStore.Initialize();

            return documentStore;
        }
    }
}