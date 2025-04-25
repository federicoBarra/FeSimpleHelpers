using UnityEngine;

namespace FeSimpleHelpers.GameBase_v001
{
	public class PlayerInputHandler : MonoBehaviour
	{
		private LevelController currentLevelController;
		private PlayerController currentPlayerController;
		// Start is called before the first frame update
		void Start()
		{
			currentLevelController = FindAnyObjectByType<LevelController>();
			currentPlayerController = FindAnyObjectByType<PlayerController>();

			InputConfig.OnTryInvokePause += PauseGame;
			InputConfig.OnTryInteract += TryInteract;
		}

		void OnDestroy()
		{
			InputConfig.OnTryInvokePause -= PauseGame;
			InputConfig.OnTryInteract -= TryInteract;
		}

		private void PauseGame()
		{
			currentLevelController?.TryPause();
		}

		private void TryInteract()
		{
			currentPlayerController?.TryInteract();
		}
	}
}