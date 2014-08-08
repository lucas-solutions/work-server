using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Stores
{
    using Lucas.Solutions.Network;
    using Lucas.Solutions.Persistence;
    using Newtonsoft.Json;

    public class LogglyStore : IEntityStore
    { 
        public LogglyStore(string inputKey, string url = "logs.loggly.com/")
        {
            InputKey = inputKey;
            Url = url;
        }

        /*protected override void Dispose(bool disposing)
        {
            //base.Dispose(disposing);
        }*/

        public string InputKey
        {
            get;
            private set;
        }

        public string Url
        {
            get;
            private set;
        }
    }

    public class LogglyStore<TEntity> : LogglyStore, IEntityStore<TEntity>
        where TEntity : class
    {
        public LogglyStore(string inputKey, string url = "logs.loggly.com/")
            : base(inputKey, url)
        {
        }

        public Task CreateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        /*public void Create(TEntity entity)
        {
            var context = (LogglyContext<TEntity>)entity;
            var communicator = new LogglyHttpCommunicator(this);

            LogglyResponse response;
            Action<Response> callbackWrapper;

            var synchronizer = entity.Synchronous ? new AutoResetEvent(false) : null;

            callbackWrapper = (Response r) =>
            {
                if (r.Success)
                {
                    response = JsonConvert.DeserializeObject<LogglyResponse>(r.Raw);
                    response.Success = true;
                }
                else
                {
                    response = new LogglyResponse { Success = false };
                }

                if (synchronizer != null)
                    synchronizer.Set();

                var callback = (Action<LogglyResponse>)context;
                if (callback != null)
                    callback(response);
            };

            if (synchronizer != null)
                synchronizer.WaitOne();

            communicator.SendPayload(LogglyHttpCommunicator.POST, string.Concat("inputs/", InputKey), context.ToString(), true, callbackWrapper);
        }*/

        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
