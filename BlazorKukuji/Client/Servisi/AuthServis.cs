using BlazorKukuji.Shared;
using GrpcStvari;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorKukuji.Client.Servisi
{
	public class AuthServis : IAuthServis
	{
		private readonly GrpcAuth.GrpcAuthClient _kanal;
		public AuthServis(GrpcAuth.GrpcAuthClient kanal)
		{
			_kanal = kanal;
		}
		public Task<OdgovorMsg> Login(RegistracijaMsg reg)
		{
			throw new NotImplementedException();
		}

		public Task Logout()
		{
			throw new NotImplementedException();
		}

		public Task<TrenutniKorisnik> ProveriKorisnika()
		{
			throw new NotImplementedException();
		}

		public async Task<OdgovorMsg> Registracija(RegistracijaMsg reg)
			=> await _kanal.RegistrujAsync(reg);
	}
}
