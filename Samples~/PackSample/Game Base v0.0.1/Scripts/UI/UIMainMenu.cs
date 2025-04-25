using FeSimpleHelpers;

namespace FeSimpleHelpers.GameBase_v001
{
	public class UIMainMenu : UIWindow
	{
		public UIWindow uiKeyconfig;
		public UIWindow uiOptions;
		public UIWindow uiCredits;

		public void StartGame()
		{
			GameManager.Get().StartGame();
		}

		public void ContinueGame()
		{
			GameManager.Get().ContinueGame();
		}

		public void ShowOptions()
		{
			uiOptions.Show();
		}

		public void ShowKeysConfig()
		{
			uiKeyconfig.Show();
		}

		public void ShowCredits()
		{
			uiCredits.Show();
		}

		public void QuitGame()
		{
			GameManager.Get().QuitGame();
		}
	}
}