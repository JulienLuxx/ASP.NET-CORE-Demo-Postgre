using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core;

namespace Test.Service
{
    public class CustomizeStaticConnectionPool : StaticConnectionPool
    {
        public CustomizeStaticConnectionPool(IOptions<ESConnectionStrings> options) : base(options.Value.Nodes.Select(x => new Uri(x)))
        { }
    }

    public class CustomizeElasticClient : ElasticClient
    {
        public CustomizeElasticClient(IConnectionPool pool) : base(new ConnectionSettings(pool))
        { }
    }

    public struct ScrollQueryParam
    {
        public string Index { get; set; }

        public int From { get; set; }

        public int Size { get; set; }

        public int ScrollTime { get; set; }

    }

    public class ESSvc : IESSvc
    {
        private IElasticClient _client { get; set; }

        public ESSvc(IElasticClient client)
        {
            _client = client;
        }

        public async Task<dynamic> ScrollAllData<T>(ScrollQueryParam param) where T : class 
        {
            var response = _client.Search<T>(x => x.Index(param.Index).From(param.From).Size(param.Size).Scroll(param.ScrollTime));
            throw new NotSupportedException();
        }
    }
}
