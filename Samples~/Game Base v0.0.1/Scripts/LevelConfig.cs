using Tymski;
using UnityEngine;

namespace FeSimpleHelpers.GameBase_v001
{
	[CreateAssetMenu(fileName = "LevelConfig", menuName = "FeSimpleHelpers/LevelConfig")]
	public class LevelConfig : ScriptableObject
	{
		public SceneReference levelScene;
		public LevelConfig nextLevelConfig;
		[Header("Level Selection Config")] 
		public string levelName = "Level X";
		[TextArea] 
		public string levelDescription = "Level Description";
		public Sprite levelIcon;
	}
}