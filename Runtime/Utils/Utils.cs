using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace FeSimpleHelpers
{
	public static class Utils
	{
		[Serializable]
		public class CurverFloat
		{
			public AnimationCurve curve;
			public float duration = 1;
			public float min = 0;
			public float max = 1;
			public float Eval(float t) => Mathf.Lerp(min, max, curve.Evaluate(t));
			public float EvalUnclamped(float t) => Mathf.LerpUnclamped(min, max, curve.Evaluate(t));
		}

		[Serializable]
		public class CurverV3
		{
			public AnimationCurve curve;
			public Vector3 min = Vector3.zero;
			public Vector3 max = Vector3.one;
			public Vector3 EvalUnclamped(float t) => Vector3.LerpUnclamped(min, max, curve.Evaluate(t));
		}

		[Serializable]
		public class ErrorFloat
		{
			public float val;
			public float error;

			public float Get(float errorFactor = -1)
			{
				float ret = val;
				float errorVal = Random.Range(-error, error);

				errorVal = errorFactor >= 0 ? errorVal * errorFactor : errorVal;
				ret += errorVal;

				return ret;
			}
		}

		public static Vector3 RandomV3(float min, float max)
		{
			Vector3 ret = Vector3.zero;
			ret.x = Random.Range(min, max);
			ret.y = Random.Range(min, max);
			ret.z = Random.Range(min, max);
			return ret;
		}

		public static int seed = -1;

		public static int RandomFix(int min, int maxNonInclusive)
		{
			if (seed == -1)
				seed = Random.Range(Int32.MinValue, Int32.MaxValue);

			seed += maxNonInclusive;
			Random.InitState(seed);

			return Random.Range(min, maxNonInclusive);
		}

		public static float RandomFix(float min, float max)
		{
			if (seed == -1)
				seed = Random.Range(Int32.MinValue, Int32.MaxValue);

			seed += (int)max;
			Random.InitState(seed);

			return Random.Range(min, max);
		}

		public static bool Dice01(float chance)
		{
			float rnd = RandomFix(0f, 1f);
			return chance > rnd;
		}

		public static T GiveRandom<T>(List<T> list)
		{
			int rnd = RandomFix(0, list.Count);
			return list[rnd];
		}


		public static bool HasFloatChanged(float newVal, ref float lastVal)
		{
			float dif = newVal - lastVal;
			lastVal = newVal;
			return (Mathf.Abs(dif) > 0.001f);
		}

		public static bool HasIntChanged(int newVal, ref int lastVal)
		{
			int dif = newVal - lastVal;
			lastVal = newVal;
			return dif != 0;
		}


		public static void ClearContentEditor(Transform content, Transform except = null)
		{
			for (int i = content.childCount - 1; i >= 0; i--)
			{
				Transform child = content.GetChild(i);
				if (child == except)
					continue;
				child.SetParent(null);
				GameObject.DestroyImmediate(child.gameObject);
			}
		}


		public const float kRelocateDistance = 50.0f;
		public const int kRelocateAreaMask = 1;
		public const int kRelocateAgentID = -1;

		public static Vector3 GetPointInNavmesh(Vector3 pos)
		{
			NavMeshHit hit;
			NavMeshQueryFilter filter = new NavMeshQueryFilter();
			filter.agentTypeID = kRelocateAgentID;
			filter.areaMask = kRelocateAreaMask;
			if (NavMesh.SamplePosition(pos, out hit, kRelocateDistance, filter))
				pos = hit.position;
			else if (NavMesh.FindClosestEdge(pos, out hit, 0))
				pos = hit.position;

			return pos;
		}

	}
}