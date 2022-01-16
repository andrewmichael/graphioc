using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace graph
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IJobGraph>(sp => new JobGraph("https://graph.microsoft.com/v1.0", new NewtonsoftJsonSerializer()));
            builder.Services.AddScoped<ICmsGraph>(sp => new CmsGraph("https://countries.trevorblades.com", new NewtonsoftJsonSerializer()));
            
            await builder.Build().RunAsync();
        }
    }
    
    public class JobGraph : GraphQLHttpClient, IJobGraph
    {
        public JobGraph(string endPoint, IGraphQLWebsocketJsonSerializer serializer) : base(endPoint, serializer)
        {
        }

        public JobGraph(Uri endPoint, IGraphQLWebsocketJsonSerializer serializer) : base(endPoint, serializer)
        {
        }

        public JobGraph(Action<GraphQLHttpClientOptions> configure, IGraphQLWebsocketJsonSerializer serializer) : base(configure, serializer)
        {
        }

        public JobGraph(GraphQLHttpClientOptions options, IGraphQLWebsocketJsonSerializer serializer) : base(options, serializer)
        {
        }

        public JobGraph(GraphQLHttpClientOptions options, IGraphQLWebsocketJsonSerializer serializer, HttpClient httpClient) : base(options, serializer, httpClient)
        {
        }
    }
    
    public class CmsGraph : GraphQLHttpClient, ICmsGraph
    {
        public CmsGraph(string endPoint, IGraphQLWebsocketJsonSerializer serializer) : base(endPoint, serializer)
        {
        }

        public CmsGraph(Uri endPoint, IGraphQLWebsocketJsonSerializer serializer) : base(endPoint, serializer)
        {
        }

        public CmsGraph(Action<GraphQLHttpClientOptions> configure, IGraphQLWebsocketJsonSerializer serializer) : base(configure, serializer)
        {
        }

        public CmsGraph(GraphQLHttpClientOptions options, IGraphQLWebsocketJsonSerializer serializer) : base(options, serializer)
        {
        }

        public CmsGraph(GraphQLHttpClientOptions options, IGraphQLWebsocketJsonSerializer serializer, HttpClient httpClient) : base(options, serializer, httpClient)
        {
        }
    }
    
    public interface IJobGraph : GraphQL.Client.Abstractions.IGraphQLClient
    {}
    
    public interface ICmsGraph : GraphQL.Client.Abstractions.IGraphQLClient
    {}
}

