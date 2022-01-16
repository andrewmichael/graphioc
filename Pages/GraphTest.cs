using System;
using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using GraphQL.Client.Http;
using GraphQL.Client.Abstractions;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace graph.Pages
{
    public partial class GraphTest
    {
        [Inject] private IJobGraph JobGraph { get; set; }
        [Inject] private ICmsGraph CmsGraph { get; set; }

        private LanguageType _languageType = null;

        private PersonType _personType = null;
        
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var request = new GraphQLHttpRequest( @"query languageQuery($code: ID!) {
                                                           	    language(code: $code) {
		                                                            name
                                                                }
                                                            }",
                    operationName: "languageQuery", variables: new { code = "pt"} );
                var response = await CmsGraph.SendQueryAsync(request, () => new { language = new LanguageType()});

                _languageType = response.Data.language;

                var request2 = new GraphQLHttpRequest(@"query peopleQuery($personID: ID!) { 
                                                                person(personID: $personID) {
                                                                    name
                                                                }
                                                            }",
                    operationName: "peopleQuery", variables: new { personID = 1} );
                var response2 = await JobGraph.SendQueryAsync(request2, () => new { person = new PersonType()});

                _personType = response2.Data.person;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

        }
    }

    public class LanguageType
    {
        public string Name { get; set; }
    }

    public class PersonType
    {
        public string Name { get; set; }
    }
    
}