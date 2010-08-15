using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrappyGame
{
	public class GameTimer
	{
		const double TicksPerSecond = 10000000.0;

		Int64 StartTicks;
		Int64 LastTicks;

		public GameTimer()
		{
			Reset();
		}

		public GameTime GetGameTimeSinceLastCall()
		{
			Int64 currentTicks = DateTime.Now.Ticks;
			Int64 elapsedTicks = currentTicks - LastTicks;
			Int64 totalTicks = currentTicks - StartTicks;

			LastTicks = currentTicks;

			return new GameTime
			{
				ElapsedSeconds = elapsedTicks / TicksPerSecond,
				TotalSeconds = totalTicks / TicksPerSecond,
			};
		}

		public void Reset()
		{
			StartTicks = LastTicks = DateTime.Now.Ticks;
		}
	}

	public class GameTime
	{
		public double TotalSeconds { get; set; }
		public double ElapsedSeconds { get; set; }
	}
}
