using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CrappyGame
{
	public class InputManager
	{
		public delegate void InputActionDelegate(Key key, GameTime time);

		private class InputActionHandler
		{
			public event InputActionDelegate KeyDown;
			public event InputActionDelegate KeyPressed;
			public event InputActionDelegate KeyUp;

			public bool WasDown { get; set; }

			public void FireKeyDown(Key key, GameTime time)
			{
				if(KeyDown != null)
					KeyDown(key, time);
			}

			public void FireKeyPressed(Key key, GameTime time)
			{
				if(KeyPressed != null)
					KeyPressed(key, time);
			}

			public void FireKeyUp(Key key, GameTime time)
			{
				if(KeyUp != null)
					KeyUp(key, time);
			}
		}

		Dictionary<Key, InputActionHandler> KeyHandlers { get; set; }

		public InputManager()
		{
			KeyHandlers = new Dictionary<Key, InputActionHandler>();
		}

		public void ProcessInput(GameTime time)
		{
			foreach(var keyHandlerPair in KeyHandlers)
			{
				Key key = keyHandlerPair.Key;
				InputActionHandler handler = keyHandlerPair.Value;
				bool keyIsDown = Keyboard.IsKeyDown(key);

				if(keyIsDown)
				{
					if(!handler.WasDown)
					{
						handler.WasDown = true;
						KeyHandlers[key].FireKeyDown(key, time);
					}

					KeyHandlers[key].FireKeyPressed(key, time);
				}
				else if(handler.WasDown)
				{
					handler.WasDown = false;
					KeyHandlers[key].FireKeyUp(key, time);
				}
				
			}
		}

		public void RegisterKeyDown(Key key, InputActionDelegate action)
		{
			GetInputAction(key).KeyDown += action; ;
		}

		public void RegisterKeyPressed(Key key, InputActionDelegate action)
		{
			GetInputAction(key).KeyPressed += action; ;
		}

		public void RegisterKeyUp(Key key, InputActionDelegate action)
		{
			GetInputAction(key).KeyUp += action;
		}

		private InputActionHandler GetInputAction(Key key)
		{
			if(!KeyHandlers.ContainsKey(key))
				KeyHandlers[key] = new InputActionHandler();

			return KeyHandlers[key];
		}
	}
}