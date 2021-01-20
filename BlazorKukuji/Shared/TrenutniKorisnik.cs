using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorKukuji.Shared
{
	public class TrenutniKorisnik
	{
		public bool Ulogovan { get; set; }
		public string Ime { get; set; }
		public Dictionary<string, string> Klejmovi { get; set; }
	}
}
