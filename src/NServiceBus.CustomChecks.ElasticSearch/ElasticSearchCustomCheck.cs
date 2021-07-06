using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using Nest;
using NServiceBus.Logging;

namespace NServiceBus.CustomChecks.ElasticSearch
{
    public abstract class ElasticSearchCustomCheck : CustomCheck
    {
        private static readonly ConcurrentDictionary<string, ElasticClient> Connections =
            new ConcurrentDictionary<string, ElasticClient>();

        private readonly Uri _elasticSearchUri;
        private static readonly ILog Log = LogManager.GetLogger<ElasticSearchCustomCheck>();

        protected ElasticSearchCustomCheck(Uri elasticSearchUri, TimeSpan? repeatAfter) : base("Monitor ElasticSearch",
            "Database", repeatAfter)
        {
            _elasticSearchUri = elasticSearchUri;
            Connections.TryAdd(elasticSearchUri.ToString(), new ElasticClient(elasticSearchUri));
        }

        public override async Task<CheckResult> PerformCheck()
        {
            var start = Stopwatch.StartNew();
            try
            {
                Connections.TryGetValue(_elasticSearchUri.ToString(), out ElasticClient lowLevelClient);

                var result = await lowLevelClient.PingAsync().ConfigureAwait(false);
                var isSuccess = result.ApiCall.HttpStatusCode == 200;
                if (isSuccess)
                {
                    Log.Info($"Ping successful");
                    return CheckResult.Pass;
                }

                var error = $"Ping Failed for '{_elasticSearchUri}. Error: {result.ApiCall.HttpStatusCode}";
                Log.Error(error);
                return CheckResult.Failed(error);
            }
            catch (Exception exception)
            {
                var error =
                    $"Failed to contact '{_elasticSearchUri}'. Duration: {start.Elapsed} Error: {exception.Message}";
                Log.Error(error);
                return CheckResult.Failed(error);
            }
        }
    }
}