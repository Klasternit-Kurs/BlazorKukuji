using BlazorKukuji.Shared;
using GrpcStvari;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorKukuji.Client.Servisi
{
	public interface IAuthServis
	{
		Task Logout();
		Task<OdgovorMsg> Login(RegistracijaMsg reg);
		Task<OdgovorMsg> Registracija(RegistracijaMsg reg);
		Task<TrenutniKorisnik> ProveriKorisnika();
	}
}
