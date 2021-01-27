using BlazorKukuji.Shared;
using GrpcStvari;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorKukuji.Server.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ILogger<AuthController> _logger;
		private readonly UserManager<IdentityUser> _um;
		private readonly SignInManager<IdentityUser> _sim;

		public AuthController(ILogger<AuthController> logger, UserManager<IdentityUser> um, SignInManager<IdentityUser> sim)
		{
			_logger = logger;
			_um = um;
			_sim = sim;
		}

		[HttpGet]
		public TrenutniKorisnik ProveriKorisnika()
		{
			_logger.LogInformation("Proveravam korisnika...");
			var kor = new TrenutniKorisnik { Ulogovan = User.Identity.IsAuthenticated, Ime = User.Identity.Name,
				Klejmovi = User.Claims.ToDictionary(klejm => klejm.Type, klejm => klejm.Value) };
			
			string log = "-------------------" + Environment.NewLine;
			kor.Klejmovi.Keys.ToList().ForEach(k => log += $"{k}  --  {kor.Klejmovi[k]}" + Environment.NewLine);
			log += "-------------------";
			_logger.LogInformation(log);
			return kor;
		}

		[HttpPost]
		public async Task<OdgovorMsg> Registracija(RegistracijaMsg r)
		{
			_logger.LogInformation("Krece registracija");
			var rez = await _um.CreateAsync(new IdentityUser { UserName = r.Korisnicko }, r.Pass);

			if (rez.Succeeded)
			{
				await _um.AddToRoleAsync(await _um.FindByNameAsync(r.Korisnicko), "Admin");
				_logger.LogInformation("Uspesno registrovan");
				return new OdgovorMsg { Uspeh = true, Greske = "" };
			}

			_logger.LogInformation("Neuspesno registrovan");
			return new OdgovorMsg
			{
				Uspeh = false,
				Greske = rez.Errors.Select(e => e.Description)
				.Aggregate((t, e) => t += e + Environment.NewLine)
			};
		}

		[HttpPost]
		public async Task<OdgovorMsg> Login(RegistracijaMsg l)
		{
			_logger.LogInformation("Krece login");
			var rez = await _sim.PasswordSignInAsync(await _um.FindByNameAsync(l.Korisnicko), l.Pass, false, false);
			if (rez.Succeeded)
			{
				_logger.LogInformation("Uspesan login");
				return new OdgovorMsg { Uspeh = true };
			}
			_logger.LogInformation("Neuspesan login");
			return new OdgovorMsg { Uspeh = false, Greske = "Invalid user/pass" };
		}

		[HttpGet]
		public async void Logout()
			=> await _sim.SignOutAsync();
	}
}
