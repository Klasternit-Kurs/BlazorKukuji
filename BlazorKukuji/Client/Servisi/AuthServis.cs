using BlazorKukuji.Shared;
using GrpcStvari;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorKukuji.Client.Servisi
{
	public class AuthServis : IAuthServis
	{
		private readonly GrpcAuth.GrpcAuthClient _kanal;
		private readonly HttpClient _hc;

		public AuthServis(GrpcAuth.GrpcAuthClient kanal, HttpClient hc)
		{
			_kanal = kanal;
			_hc = hc;
		}

		public async Task<OdgovorMsg> Login(RegistracijaMsg reg)
			=> await (await _hc.PostAsJsonAsync("api/auth/login", reg))
				.Content
				.ReadFromJsonAsync<OdgovorMsg>();


		public async Task Logout()
			=> await _hc.GetAsync("api/auth/logout");

		public async Task<TrenutniKorisnik> ProveriKorisnika()
			=> await _hc.GetFromJsonAsync<TrenutniKorisnik>("api/auth/ProveriKorisnika");
		

		public async Task<OdgovorMsg> Registracija(RegistracijaMsg reg)
			=> await (await _hc.PostAsJsonAsync("api/auth/registracija", reg))
				.Content
				.ReadFromJsonAsync<OdgovorMsg>();
	}
}
