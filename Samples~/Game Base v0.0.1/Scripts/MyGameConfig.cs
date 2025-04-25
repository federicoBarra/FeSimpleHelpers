using UnityEngine;

namespace FeSimpleHelpers.GameBase_v001
{
	[CreateAssetMenu(fileName = "MyGameConfig", menuName = "FeSimpleHelpers/MyGameConfig")]
	public class MyGameConfig : ConfigSingleton<MyGameConfig>
	{
		public float someValue = 1;
	}
}