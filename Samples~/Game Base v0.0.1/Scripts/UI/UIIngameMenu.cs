namespace FeSimpleHelpers.GameBase_v001
{
	public class UIIngameMenu : UIWindow
	{
		public UIWindow uiKeyconfig;
		public UIWindow uiOptions;

		protected override void Awake()
		{
			base.Awake();
			GameManager.OnGamePaused += GameChangedPauseState;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			GameManager.OnGamePaused -= GameChangedPauseState;
		}

		private void GameChangedPauseState(bool _state)
		{
			if (_state)
				Show();
		}

		public override void Back()
		{
			base.Back();
			GameManager.Get().SetPause(false);
		}

		public void ShowKeysConfig()
		{
			uiKeyconfig.Show();
		}

		public void ShowOptions()
		{
			uiOptions.Show();
		}

		public void GoToMainMenu()
		{
			GameManager.Get().GoToMainMenu();
		}
		public void QuitGame()
		{
			GameManager.Get().QuitGame();
		}
	}
}
