using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorKukuji.Client.Servisi
{
	public class AuthStateProvider : AuthenticationStateProvider
	{
		private readonly IAuthServis _authApi;

		public AuthStateProvider (IAuthServis authApi)
		{
			_authApi = authApi;
		}
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var kor = await _authApi.ProveriKorisnika();

			var identitet = new ClaimsIdentity();
			

			if (!kor.Ulogovan)
				Console.WriteLine("Nije ulogovan");
			else
			{
				List<Claim> klejmovi = new List<Claim>();
				kor.Klejmovi.Keys.ToList().ForEach(k => klejmovi.Add(new Claim(k, kor.Klejmovi[k])));
				klejmovi.Add(new Claim(ClaimTypes.Name, kor.Ime));
				identitet = new ClaimsIdentity(klejmovi, "Server authentication");
			}

			return new AuthenticationState(new ClaimsPrincipal(identitet));
		}
	}
}
