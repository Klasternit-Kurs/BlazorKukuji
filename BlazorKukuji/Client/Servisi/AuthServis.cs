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
		public Task<OdgovorMsg> Login(RegistracijaMsg reg)
		{
			throw new NotImplementedException();
		}

		public Task Logout()
		{
			throw new NotImplementedException();
		}

		public async Task<TrenutniKorisnik> ProveriKorisnika()
			=> await _hc.GetFromJsonAsync<TrenutniKorisnik>("api/auth/ProveriKorisnika");
		

		public async Task<OdgovorMsg> Registracija(RegistracijaMsg reg)
			=> await _kanal.RegistrujAsync(reg);
	}
}
