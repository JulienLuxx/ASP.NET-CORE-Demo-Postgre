using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.Core;

namespace Test.Service
{
    public class CustomizeConnectionPool : StaticConnectionPool
    {
        public CustomizeConnectionPool(IOptions<ESConnectionStrings> options) : base(options.Value.Nodes.Select(x => new Uri(x)))
        { }
    }

    public class CustomizeElasticClient : ElasticClient
    {
        public CustomizeElasticClient(IConnectionPool pool) : base(new ConnectionSettings(pool))
        { }
    }

    public class ESSvc : IESSvc
    {
        private IElasticClient _client { get; set; }
        public ESSvc(IElasticClient client)
        {
            _client = client;
        }

        public void Test()
        { }
    }
}
