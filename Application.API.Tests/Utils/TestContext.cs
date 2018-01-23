using Infraestructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net;
using System.Net.Http;

namespace Application.API.Tests
{
    public class TestContext
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly DatabaseContext _dbContext;


        public TestContext()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<StartupTests>());


            _client = _server.CreateClient();
            _dbContext = (DatabaseContext)Server.Host.Services.GetService(typeof(DatabaseContext));
        }

        public HttpClient Client { get { return _client; } }

        public TestServer Server { get { return _server; } }

        public DatabaseContext Database { get { return _dbContext; } }
    }
}
