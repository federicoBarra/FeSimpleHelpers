using Tymski;
using UnityEngine;

namespace FeSimpleHelpers.GameBase_v001
{
	public class GameBoot : MonoBehaviour
	{
		public SceneReference introScene;

		void Start()
		{
			LoaderManager.Get().LoadScene(introScene, -1, LoaderManager.LoadingType.NoInterface);
		}
	}
}