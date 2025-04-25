using System;
using Tymski;
using UnityEngine;

namespace FeSimpleHelpers.GameBase_v001
{
	public class GameManager : MonoBehaviourSingleton<GameManager>
	{
		private LevelConfig currentLevel;
		[SerializeField]
		private SceneReference mainMenuScene;

		public static event Action<bool> OnGamePaused;

		protected override void Awake()
		{
			base.Awake();
			//TODO FloorCheatsConfig.GetInherit.StartPlayState();
			LevelController.OnTryPause += TryPause;

			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = 60;
		}

		protected void OnDisable()
		{
			//TODO FloorCheatsConfig.GetInherit.EndPlayState();
			LevelController.OnTryPause -= TryPause;
		}

		private void TryPause()
		{
			SetPause(true);
		}

		private bool gamePaused = false;
		public void SetPause(bool b)
		{
			gamePaused = b;
			if (gamePaused)
			{
				Time.timeScale = 0;
				InputConfig.Get().EnableMenu();
			}
			else
			{
				Time.timeScale = 1;
				InputConfig.Get().EnableIngame();
			}

			OnGamePaused?.Invoke(gamePaused);
		}

		public void StartGame()
		{
			LoadLevel(LevelsConfig.Get().firstLevel);
		}

		public void ContinueGame()
		{
			Debug.Log("TODO Continue Game");
		}

		public void LoadLevel(LevelConfig levelConfig)
		{
			currentLevel = levelConfig;
			LoaderManager.Get().LoadScene(currentLevel.levelScene, 2, LoaderManager.LoadingType.Simple);
		}

		public void GoToMainMenu()
		{
			LoaderManager.Get().LoadScene(mainMenuScene, 2, LoaderManager.LoadingType.Simple);
		}

		public void QuitGame()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}
	}
}