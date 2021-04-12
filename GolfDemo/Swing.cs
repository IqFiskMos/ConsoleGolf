using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf {
	public class Swing {

		// gravitation av hålet
		public const double GRAVITY = 9.8;
		
		
		// hålet vi är på just nu
		public Course Course { get; set; }

		
		// vinkeln som bollen lämnar marken i grader
		public double Angle { get; set; }

		
		// vinkeln som bollen lämnar marken i radianer
		public double AngleInRadians {
			get {
				return (Math.PI / 180) * this.Angle;
			}
		}

		// styrkan på slaget i m/s
		public double Velocity { get; set; }

		
		// uträkningen för hur långt slaget går
		public double Distance {
			get
			{
				return Math.Pow(this.Velocity, 2) / GRAVITY * 
					Math.Sin(2 * this.AngleInRadians);
			}
		}

		public Swing(double angle, double velocity, Course course) {
			// Kontrollerar så att vinkeln är rätt inskrivet
			if(angle >= 90 || angle <= 0) {
				throw new AngleInvalidExeption();
			}
			// Kontrollerar att det är ett positivt tal på styrkan
			if(velocity <= 0) {
				this.Velocity = 0;
			} else {
				this.Velocity = velocity;
			}

			// spara värdena för senare användning
			this.Angle = angle;
			this.Course = course;

		}
	}
}
