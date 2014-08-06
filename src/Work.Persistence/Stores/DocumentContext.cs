using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions
{
    using Raven.Abstractions.Data;
    using Raven.Client;
    using Raven.Client.Indexes;
    using Raven.Json.Linq;

    public abstract class DocumentContext
    {
        private static readonly object _lock = new object();

        private static IDocumentStore _store;

        private readonly string _name;

        protected DocumentContext(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        /*private string SessionName
        {
            get { return "RavenSession" + Name; }
        }

        public IDocumentSession Session
        {
            get
            {
                var session = System.Web.HttpContext.Current.Items[SessionName] as IDocumentSession;

                if (session != null)
                {
                    return session;
                }

                session = Store.OpenSession();

                System.Web.HttpContext.Current.Items[SessionName] = session;

                return session;
            }
            private set
            {
                var itemName = "RavenSession" + Name;
                System.Web.HttpContext.Current.Items[itemName] = value;
            }
        }*/

        public IDocumentStore Store
        {
            get
            {
                var store = _store;

                // return without locking if already exist
                if (store != null)
                    return store;

                // lock and check again
                lock (_lock)
                {
                    // create new instance if doesn't exist
                    store = _store ?? (_store = CreateDocumentStore());

                    InitializeDocumentStore(store);

                    // save store instance
                    _store = store;
                }

                return store;
            }
        }

        protected abstract IDocumentStore CreateDocumentStore();

        protected virtual void InitializeDocumentStore(IDocumentStore store)
        {
            //store.Conventions.IdentityPartsSeparator = "/";
            store.Conventions.SaveEnumsAsIntegers = false;

            store.Initialize();

            new RavenDocumentsByEntityName().Execute(store);
        }

        /*public void SaveChanges()
        {
            var session = System.Web.HttpContext.Current.Items[SessionName] as IDocumentSession; ;

            if (session != null)
            {
                System.Web.HttpContext.Current.Items[SessionName] = null;

                session.SaveChanges();
            }
        }*/
    }
}
