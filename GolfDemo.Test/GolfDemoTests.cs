using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GolfDemo.Test {
	[TestClass]
	public class GolfDemoTests {
		/// <summary>
		/// The swing should make the ball travel the
		/// correct distance, based on the angle and velocity.
		/// If it does not, this test should fail. 
		/// </summary>
		[TestMethod]
		public void TestCorrectDistance() {
			Course c = new Course(8);
			c.Swing(angle: 45, velocity: 56);
			Assert.IsTrue(c.DistanceTravelled == 320);
		}

		/// <summary>
		/// When a swing has been taken,
		/// the swing counter should increase.
		/// If it does not, this test should fail.
		/// </summary>
		[TestMethod]
		public void TestSwingCounterIncrease() {
			Course c = new Course(8);
			int init = c.SwingCounter;
			c.Swing(87, 160);
			Assert.AreEqual(expected: init + 1, actual: c.SwingCounter);
		}

		/// <summary>
		/// When too many swings have been taken,
		/// it should end the game in a failure state.
		/// If it does not, this test should fail.
		/// </summary>
		[TestMethod]
		public void TestGameEndTooManySwings() {
			Course c = new Course(maxSwings: 1);
			c.Swing(angle: 10, velocity: 10);
			Assert.IsTrue(c.HasEnded);
			Assert.IsTrue(c.HasLost);
		}

		/// <summary>
		/// When the ball hits the target,
		/// the game should end in a victory state.
		/// If it does not, this test should fail.
		/// </summary>
		[TestMethod]
		public void TestGameEndWin() {
			Course c = new Course(courseLength: 320);
			c.Swing(angle: 45, velocity: 56);
			Assert.IsTrue(c.HasEnded);
			Assert.IsTrue(c.HasWon);
		}

		/// <summary>
		/// If the ball travels too far away from the target,
		/// the game should end in a failure state unique to that scenario.
		/// If it does not, this test should fail.
		/// </summary>
		[TestMethod]
		public void TestGameEndTooFarFromGoal() {
			Course c = new Course(courseLength: 320, courseRough: 200);
			c.Swing(angle: 45, velocity: 100);
			Assert.IsTrue(c.HasEnded);
			Assert.IsTrue(c.HasLost);
			Assert.IsTrue(c.IsOutOfBounds);
		}

		/// <summary>
		/// The angle of the ball leaving the player should never
		/// go below 0 degrees, nor should it go above 90 degrees.
		/// If it does, an error should occur. If not, this test should fail.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(AngleInvalidExeption))]
		public void TestAngleTooLow() {
			Course c = new Course(8);
			c.Swing(angle: -1, velocity: 100);
		}

		/// <summary>
		/// The angle of the ball leaving the player should never
		/// go below 0 degrees, nor should it go above 90 degrees.
		/// If it does, an error should occur. If not, this test should fail.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(AngleInvalidExeption))]
		public void TestAngleTooHigh() {
			Course c = new Course(8);
			c.Swing(angle: 91, velocity: 100);
		}
		/// <summary>
		/// Negative force should never be possible for a swing.
		/// If it occurs, this test should fail.
		/// </summary>
		[TestMethod]
		public void TestValidVelocity() {
			Course c = new Course(8);
			c.Swing(angle: 45, velocity: -10);
			Assert.IsTrue(c.Swings.Last().Velocity == 0);
		}
	}
}
