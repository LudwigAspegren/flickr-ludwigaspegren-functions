using System;
using Ludwigaspegren_functions_app.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Newtonsoft.Json;

[assembly:FunctionsStartup(typeof(Ludwigaspegren_functions_app.Startup))]

namespace Ludwigaspegren_functions_app
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient<IFlickrService, FlickrService>(client =>
            {
                client.BaseAddress = new Uri("https://www.flickr.com/services/rest/");
            });
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

    }
}