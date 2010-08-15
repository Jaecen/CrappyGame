using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace CrappyGame
{
	public abstract class GameWindow : Window
	{
		protected GameTimer Timer;
		protected InputManager Input;

		public GameWindow()
		{
			Timer = new GameTimer();
			Input = new InputManager();

			Input.RegisterKeyDown(System.Windows.Input.Key.Escape, (k, d) => Close());

			Loaded += GameWindow_Loaded;
			CompositionTarget.Rendering += CompositionTarget_Rendering;
		}

		private void GameWindow_Loaded(object sender, RoutedEventArgs e)
		{
			Timer.Reset();
		}

		private void CompositionTarget_Rendering(object sender, EventArgs e)
		{
			GameTime time = Timer.GetGameTimeSinceLastCall();
			HandleInput(time);
			RenderFrame(time);
		}

		protected virtual void HandleInput(GameTime time)
		{
			Input.ProcessInput(time);
		}

		protected virtual void RenderFrame(GameTime time)
		{
		}
	}
}
