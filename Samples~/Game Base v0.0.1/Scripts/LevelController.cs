using System;
using UnityEngine;

namespace FeSimpleHelpers.GameBase_v001
{
	public class LevelController : MonoBehaviour
	{
		public static event Action OnTryPause;

		void Start()
		{
			InputConfig.Get().EnableIngame();
		}

		public void TryPause()
		{
			OnTryPause?.Invoke();
		}
	}
}