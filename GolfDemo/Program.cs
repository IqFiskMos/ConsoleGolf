using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf {
	public class Program {
		static void Main(string[] args) {
			// Låt oss generera ett hål mellan 200 och 800
			Random rng = new Random();
			double length = rng.Next(200, 800);
			int maxSwings = (int)(length / 100) + 1;

			// Skapar hålet med värdet ovan
			Course course = new Course(courseLength: length, maxSwings: maxSwings);

			while(!course.HasEnded) {
				// Skriver ut spelets basinfo
				Console.WriteLine($"Längd till hålet {course.CourseLength} meter");
				Console.WriteLine($"Hålets längd {course.CourseRough} meters (Max gräns: {course.CourseRough + course.CourseLength} meter)");
                Console.WriteLine($"Distans till hålet: {course.DistanceFromHole.ToString("0.00")} meter");
				Console.WriteLine($"{course.SwingCounter} slag på hålet hitills. (Max {course.MaxSwings} slag)");
                Console.WriteLine("----------------");

				// Låt oss skriva ut varje slag
				foreach(var item in course.Swings) {
					Console.WriteLine($"\tVinkel:{item.Angle}\t{item.Velocity}m/s\tDistans:{item.Distance.ToString("N2")} meter");
				}
				Console.WriteLine("----------------");
				// Fråga efter en vinkel
				Console.Write("Vinkel: ");
				double inValue1 = double.Parse(Console.ReadLine());
				// Fråga efter styrkan på slaget
				Console.Write("m/s (endast positiva tal fungerar): ");
				double inValue2 = double.Parse(Console.ReadLine());

				// Genomför slaget, är vinkeln felaktig misslyckas man
				try {
					course.Swing(angle: inValue1, velocity: inValue2);
				} catch(AngleInvalidExeption e) {
					Console.WriteLine(e.Message);
					continue;
				}
				// Skriver ut distansen på slaget
				Console.WriteLine($"Distans: {course.Swings.Last().Distance.ToString("N2")} meter");
				Console.WriteLine("----------------");
				// Pausar tills spelaren trycker på en knapp
				Console.ReadKey();
				Console.Clear();
			}
			// Skriver ut resultatet och skulle du misslyckas så skriver den ut det
			if(course.HasLost) {
				Console.WriteLine("Du misslyckades att nå hålet");
				Console.WriteLine($"Distans kvar till hålet: {course.DistanceFromHole.ToString("0.00")} meter");
				if(course.IsOutOfBounds) {
					Console.WriteLine("För bollen gick utanför hålets gränser.");
				}
				if(course.SwingCounter >= course.MaxSwings) {
					Console.WriteLine("För du tog för många slag på dig.");
				}
			} else if(course.HasWon) {
				Console.WriteLine("Bollen rulla ner i hålet.");
				if(course.SwingCounter == 1) {
					Console.WriteLine("Hole in one!");
				}
			}
			Console.ReadKey();
		}
	}
}
