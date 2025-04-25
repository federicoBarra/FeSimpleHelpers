using UnityEngine;

namespace FeSimpleHelpers.GameBase_v001
{
	[RequireComponent(typeof(UIWindowFocusHandler))]
	public class UIWindowInteractHandler : MonoBehaviour
	{
		private UIWindowFocusHandler focusHandler;
		void Awake()
		{
			focusHandler = GetComponent<UIWindowFocusHandler>();
			InputConfig.OnMenuCancel += BackCurrentFocusedUIWindow;
		}

		void OnDestroy()
		{
			InputConfig.OnMenuCancel -= BackCurrentFocusedUIWindow;
		}

		private void BackCurrentFocusedUIWindow()
		{
			focusHandler?.BackCurrentFocusedUIWindow();
		}
	}
}