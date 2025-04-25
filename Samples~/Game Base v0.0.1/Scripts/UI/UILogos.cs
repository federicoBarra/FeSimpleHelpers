using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FeSimpleHelpers.GameBase_v001
{
	public class UILogos : MonoBehaviour
	{
		public List<CanvasGroup> thingsToShow;

		public float logosDuration = 3;
		public AnimationCurve alphaCurve;

		void Start()
		{
			foreach (CanvasGroup cg in thingsToShow)
			{
				cg.alpha = 0;
			}

			StartCoroutine(LaunchLogos());
		}

		IEnumerator LaunchLogos()
		{
			int logoIndex = 0;
			float t = 0;
			CanvasGroup cg = thingsToShow[logoIndex];
			cg.alpha = 0;

			while (t <= logosDuration)
			{
				cg.alpha = alphaCurve.Evaluate(t / logosDuration);

				t += Time.deltaTime;

				if (t >= logosDuration)
				{
					logoIndex++;
					if (logoIndex < thingsToShow.Count)
					{
						cg.alpha = 0;
						cg = thingsToShow[logoIndex];
						t = 0;
					}
				}

				yield return null;
			}

			yield return null;

			EndIntro();
		}

		void EndIntro()
		{
			GameManager.Get().GoToMainMenu();
		}
	}
}