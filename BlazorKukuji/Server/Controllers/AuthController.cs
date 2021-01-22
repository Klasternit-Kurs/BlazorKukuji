using BlazorKukuji.Shared;
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

		public AuthController(ILogger<AuthController> logger)
		{
			_logger = logger;
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
			

		public void Login()
		{

		}
	}
}
