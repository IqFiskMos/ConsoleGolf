using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf {
	public class Course {
		#region Statics and Constants
		
		// Hur nära man måste komma för att det skall räknas som att bollen är i hålet
		
		public const double Tolerance = 0.1;
		#endregion
		#region Fields and Properties
		
		// Max slag tillåtna på hålet
		
		public int MaxSwings { get; set; }

		
		// Uträkningen för hålets längd och vad som är tillåtet innan kommer utanför gränsen
		
		public double CourseRough { get; set; }

		
		// räknare för antal slag
		
		private int swingCounter;

		
		// funktionen för slagräknaren
		
		public int SwingCounter
		{
			get { return swingCounter; }
			set
			{
				if(value >= this.MaxSwings) {
					HasEnded = true;
					HasLost = true;
				}
				swingCounter = value;
			}
		}
		
		
		// Lista över svingen på hålet
		public List<Swing> Swings { get; set; }

		
		// Har bålen nått hålet
		public bool HasEnded { get; set; }
		
		// Har spelaren misslyckats
		public bool HasLost { get; set; }
		
		// Har spelaren lyckats
		public bool HasWon { get; set; }
		
		// Har spelaren misslyckats genom att slå utanför hålet
		public bool IsOutOfBounds { get; set; }

		
		// Uträkning över hur lång bollen har flyttat sig
		public double DistanceTravelled {
			get
			{
				double totalDistance = 0;
				foreach(Swing swing in Swings) {
					totalDistance += swing.Distance;
				}
				return totalDistance;
			}
		}

		
		// Uträkning av hur lång ifrån hålet vi är
		public double DistanceFromHole
		{
			get
			{
				double distanceFromHole = CourseLength;
				foreach(Swing swing in Swings) {
					distanceFromHole = Math.Abs(distanceFromHole - swing.Distance);
				}
				return distanceFromHole;
			}
		}

		
		// Uträkning på hålet från start till slut
		public double CourseLength { get; set; }
		#endregion
		#region Constructors
		
		private Course() {
			this.Swings = new List<Swing>();
		}

		public Course(int maxSwings) : this() {
			this.MaxSwings = maxSwings;
		}

		public Course(
				double courseLength,
				int maxSwings = 8,
				double courseRough = 1000
			)
			: this(maxSwings: maxSwings) {
			this.CourseLength = courseLength;
			this.CourseRough = courseRough;
		}
		#endregion
		#region Methods

		public void Swing(double angle, double velocity) {
			// Skapa svingen med hjälp av vinkeln och styrkan
			Swing swing = new Swing(angle, velocity, this);

			// Lägger till svingen till listan
			Swings.Add(swing);

			// Ökar slagräknaren
			this.SwingCounter++;

			// Ifall vi är inom tolleransen av vad som är att bollen är i hålet
			if(this.DistanceFromHole
				<= Course.Tolerance) {
				this.HasEnded = true;
				this.HasWon = true;

			// Ifall bollen har flugit utanför gränsen
			} else if(this.DistanceFromHole > (CourseLength + CourseRough)) {
				this.IsOutOfBounds = true;
				this.HasEnded = true;
				this.HasLost = true;
			}
		}

		#endregion
	}
}
