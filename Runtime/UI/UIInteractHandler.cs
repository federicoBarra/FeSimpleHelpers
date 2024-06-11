using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FeSimpleHelpers
{
	public class UIInteractHandler : MonoBehaviour
	{
		private UIWindow currentFocusWindow;

		void Awake()
		{
			//InputConfig.OnMenuCancel += CancelPressed;
			UIWindow.OnFocusIntent += WindowTryFocus;
		}

		private void CancelPressed()
		{
			if (!currentFocusWindow)
				return;

			UIWindow lastWindow = currentFocusWindow;

			currentFocusWindow.Back();

			if (lastWindow == currentFocusWindow)
				currentFocusWindow = null;
		}

		void OnDestroy()
		{
			//InputConfig.OnMenuCancel -= CancelPressed;
			UIWindow.OnFocusIntent -= WindowTryFocus;
		}

		public void WindowTryFocus(UIWindow window)
		{

			currentFocusWindow = window;
		}
	}
}
