using System;
using System.Dynamic;
using System.Threading.Tasks;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace graph.Pages
{
    public partial class GraphTest
    {
        [Inject] private IJobGraph JobGraph { get; set; }

        [Inject] private ICmsGraph CmsGraph { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var request = new GraphQLHttpRequest( @"query MyQuery(code: CODE) {
                                                            language(code: CODE){
                                                            name
                                                        }}",
                operationName: "MyQuery", variables: new { code = "GB"} );
            var response = await CmsGraph.SendQueryAsync<Language>(request);

            Console.WriteLine(response.Data.name);
        }
    }

    public class Language
    {
        [JsonProperty]
        public string name { get; set; }
    }
}