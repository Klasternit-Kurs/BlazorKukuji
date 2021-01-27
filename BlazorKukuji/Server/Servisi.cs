using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcStvari;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BlazorKukuji.Server
{
	public class Servisi : GrpcAuth.GrpcAuthBase 
	{
		private readonly UserManager<IdentityUser> _um;
		private readonly SignInManager<IdentityUser> _sim;
		private readonly ILogger<Servisi> _log;

		public Servisi(UserManager<IdentityUser> um, SignInManager<IdentityUser> sim, ILogger<Servisi> log)
		{
			_um = um;
			_sim = sim;
			_log = log;
		}

		public override async Task<OdgovorMsg> Registruj(RegistracijaMsg request, ServerCallContext context)
		{
			var rez = await _um.CreateAsync(new IdentityUser { UserName = request.Korisnicko }, request.Pass);

			if (rez.Succeeded)
				return new OdgovorMsg { Uspeh = true, Greske = "" };
			else
			{
				return new OdgovorMsg
				{
					Uspeh = false,
					Greske = rez.Errors.Select(e => e.Description)
						.Aggregate((t, e) => t += e + Environment.NewLine)
				};

				/*string t = "";
				foreach (var er in rez.Errors)
				{
					t += er.Description + Environment.NewLine;
				}*/
			}
		}

		public override Task<KorisnikInfoMsg> Provera(PrazanMsg request, ServerCallContext context)
		{ 
			if (!_sim.Context.User.Identity.IsAuthenticated)
				return Task.FromResult(new KorisnikInfoMsg { Auth=false, Ime = ""});
			var kor = new KorisnikInfoMsg { Auth = true, Ime = _sim.Context.User.Identity.Name };
			_sim.Context.User.Claims.ToList().ForEach(k => 
			{ 
				kor.TipKlejma.Add(k.Type); 
				kor.VrednostKlejma.Add(k.Value); 
			});
			return Task.FromResult(kor);
		}
	}
}
