using System.Collections.Generic;
using UnityEngine;

namespace FeSimpleHelpers.GameBase_v001
{
	[CreateAssetMenu(fileName = "LevelsConfig", menuName = "FeSimpleHelpers/LevelsConfig")]
	public class LevelsConfig : ConfigSingleton<LevelsConfig>
	{
		public LevelConfig firstLevel;
		public List<LevelConfig> levels;
	}
}
