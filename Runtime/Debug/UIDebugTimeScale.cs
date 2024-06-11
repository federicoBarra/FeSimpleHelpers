using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDebugTimeScale : MonoBehaviour
{
	public bool emparejar;
	public Slider sliderTimeScale;
	public Slider sliderFixedDelta;

	public TMP_Text timeScaleText;
	public TMP_Text fixedDeltaTimeText;

	public float systemFixedDeltaTime;

	public float timeScaleLastValue;
	public float fixedScaleLastValue;

	// Start is called before the first frame update
	IEnumerator Start()
    {
	    yield return new WaitForSeconds(.1f);
	    systemFixedDeltaTime = Time.fixedDeltaTime;

	    sliderFixedDelta.value = 1;
	    fixedScaleLastValue = 1;
		//if (!CheatManager.Exists || !CheatManager.Get().CheatsEnabled)
		//{
		//	Destroy(gameObject);
		//	yield break;
		//}
		timeScaleLastValue = Time.timeScale;
	    sliderTimeScale.value = timeScaleLastValue;
	    timeScaleText.text = timeScaleLastValue.ToString("N02");
    }

    // Update is called once per frame
    void Update()
    {
		if (Mathf.Epsilon < Math.Abs(sliderTimeScale.value - timeScaleLastValue))
	    {
		    timeScaleLastValue = sliderTimeScale.value;
			Time.timeScale = timeScaleLastValue;
			timeScaleText.text = timeScaleLastValue.ToString("N02");
		}

		if (Mathf.Epsilon < Math.Abs(sliderFixedDelta.value - fixedScaleLastValue))
		{
			fixedScaleLastValue = sliderFixedDelta.value;
			fixedDeltaTimeText.text = fixedScaleLastValue.ToString("N02");
			Time.fixedDeltaTime = systemFixedDeltaTime * fixedScaleLastValue;
		}
	}
}
