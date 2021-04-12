using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf {
	public class AngleInvalidExeption : Exception {
		public AngleInvalidExeption()
			: base("Felaktig vinkeln - försök igen") {
		}
	}
}
