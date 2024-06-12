using System.Collections;
using System.Collections.Generic;
using FeSimpleHelpers;
using UnityEngine;

[CreateAssetMenu(fileName = "MyGameConfig", menuName = "FeSimpleHelpers/MyGameConfig")]
public class MyGameConfig : ConfigSingleton<MyGameConfig>
{
	public float someValue = 1;
}
