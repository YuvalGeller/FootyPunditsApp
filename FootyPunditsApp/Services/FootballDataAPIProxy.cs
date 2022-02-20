using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using FootballDataApi;

namespace FootyPunditsApp.Services
{
    class FootballDataAPIProxy
    {
        private HttpClient client;
        private static FootballDataAPIProxy proxy = null;
        public CompetitionProvider Competition { private set; get;}
        public AreaProvider Area { private set; get; }
        public MatchProvider Match { private set; get; }

        public StandingProvider Standing { private set; get; }

        public TeamProvider Team { private set; get; }

        public static FootballDataAPIProxy CreateProxy()
        {
            if (proxy == null)
                proxy = new FootballDataAPIProxy();
            return proxy;
        }


        private FootballDataAPIProxy()
        {
            //Create client with the handler!
            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Add("X-Auth-Token", App.APIKey);

            this.Competition = CompetitionProvider.Create()
            .With(client)
            .Build();

            this.Area = AreaProvider.Create()
            .With(client)
            .Build();

            this.Team = TeamProvider.Create()
            .With(client)
            .Build();

            this.Match = MatchProvider.Create()
            .With(client)
            .Build();

            this.Standing = StandingProvider.Create()
            .With(client)
            .Build();
        }
    }
}
