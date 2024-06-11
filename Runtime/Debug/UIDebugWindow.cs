using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FeSimpleHelpers
{
	public class UIDebugWindow : MonoBehaviourSingleton<UIDebugWindow>
	{
		public TMP_Text text;

		public bool startOff;

		private Canvas canvas;

		protected override void Awake()
		{
			base.Awake();
			canvas = GetComponent<Canvas>();
			if (startOff)
				canvas.enabled = false;
			CheatsConfig.Get().AddCheat(Toggle, Key.F11);
		}

		public void Toggle()
		{
			canvas.enabled = !canvas.enabled;
		}

		public static void SetConsoleText(string t)
		{
			Get().text.text = t;
		}
	}
}