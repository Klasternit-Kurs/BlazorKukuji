using BlazorKukuji.Client.Servisi;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorKukuji.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddScoped<IAuthServis, AuthServis>();


			builder.Services.AddSingleton(sr =>
			{
				string srv = sr.GetRequiredService<NavigationManager>().BaseUri;
				var httpKlijent = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));

				var kanal = GrpcChannel.ForAddress(srv, new GrpcChannelOptions { HttpClient = httpKlijent });

				return new GrpcStvari.GrpcAuth.GrpcAuthClient(kanal);
			});


			await builder.Build().RunAsync();
		}
	}
}
