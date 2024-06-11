using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace FeSimpleHelpers.Editor
{
	public class SortChildren : ScriptableObject
	{
		[MenuItem("FeSimpleHelpers/Sort Children Alphabetically")]
		public static void MenuAddChild()
		{
			Sort(Selection.activeTransform);
		}

		static void Sort(Transform current)
		{
			List<Transform> children = new List<Transform>();
			foreach (Transform child in current)
				children.Add(child);
			children = children.OrderBy(o => o.name).ToList();

			foreach (Transform child in children)
			{
				child.parent = null;
			}

			foreach (Transform child in children)
			{
				child.parent = current;
			}
		}
	}
}
#endif
