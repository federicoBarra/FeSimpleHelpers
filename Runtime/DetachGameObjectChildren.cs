using UnityEngine;

public class DetachGameObjectChildren : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
	    int sceneIndex = 1;
		for (int i = 0; i < transform.childCount; ++i)
		{
			Transform child = transform.GetChild(i);
			child.SetParent(null);
			child.SetSiblingIndex(sceneIndex);
			sceneIndex++;
			i--;
		}

		transform.SetSiblingIndex(0);
		gameObject.name = "--------- START OF Scene Needed Game Objects -------";
		GameObject separator = new GameObject("--------- END OF Scene Needed Game Objects -------");
		separator.transform.SetSiblingIndex(sceneIndex);
    }
}
