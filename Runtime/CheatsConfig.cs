using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Key = UnityEngine.InputSystem.Key;

namespace FeSimpleHelpers
{
	//[CreateAssetMenu(fileName = "CheatsConfig", menuName = "FedeTools/CheatsConfig")]
	public class CheatsConfig : ConfigSingleton<CheatsConfig>
	{
		public bool cheatsEnabled;

		[Header("Basics")] public GameObject debugObjPrefab;
		GameObject debugObj;
		public GameObject somePrefab;
		public Vector3 spawnPrefabPos;
		public DebugArrow debugArrowPrefab;

		[Header("Colors")] public Color colorRed;
		public Color colorGreen;
		public Color colorBlue;
		public Color colorYellow;
		[Header("DEBUG")] public bool verbose;

		[Serializable]
		public class CheatCommand
		{
			public CheatCommand(string name, Action callback, Key mainKey, Key modifierKey)
			{
				this.name = name;
				this.callback = callback;
				this.mainKey = mainKey;
				this.modifierKey = modifierKey;
			}

			public string name;
			public Action callback;
			public Key mainKey;
			public Key modifierKey;
		}

		List<CheatCommand> staticCheats = new List<CheatCommand>();
		List<CheatCommand> dynamicCheats = new List<CheatCommand>();
		List<CheatCommand> allCheats = new List<CheatCommand>();

		List<string> cheatInfo = new List<string>();

		//private bool running = false;

		public override void OnFirstLoad()
		{
			base.OnFirstLoad();
			if (verbose)
				Debug.Log("First Load cheats");
			Recreate();
		}

		void OnDestroy()
		{
			if (verbose)
				Debug.Log("On Destroy");
		}


		public virtual void Recreate()
		{
			cheatInfo.Clear();
			cheatInfo = new List<string>();
			dynamicCheats.Clear();
			staticCheats.Clear();
			staticCheats.Add(new CheatCommand(nameof(SpawnPrefab), SpawnPrefab, Key.V, Key.None));
			allCheats.Clear();
			allCheats.AddRange(staticCheats);
		}

		public bool AddCheat(Action callback, Key mainKey)
		{
			return AddCheat(callback, mainKey, Key.None);
		}

		public bool AddCheat(Action callback, Key mainKey, Key modifierKey)
		{
			if (verbose)
				Debug.Log("add Cheat: " + callback.Method.Name);
			for (int i = 0; i < allCheats.Count; i++)
			{
				var cheat = allCheats[i];
				if (cheat.mainKey == mainKey && cheat.modifierKey == modifierKey)
				{
					Debug.LogError(callback.Method.Name + " cannont be added, already a cheat with those commands: " +
								   mainKey +
								   " + " + modifierKey);
					return false;
				}
			}

			CheatCommand newCheat = new CheatCommand(callback.Method.Name, callback, mainKey, modifierKey);
			allCheats.Add(newCheat);
			dynamicCheats.Add(newCheat);
			string newCheatInfo = callback.Method.Name + " - " + mainKey.ToString() + "/" + modifierKey.ToString();
			cheatInfo.Add(newCheatInfo);
			return true;
		}

		public void StartPlayState()
		{
			if (verbose)
				Debug.Log("StartPlayState");
			Recreate();
			//running = true;
		}

		// Update is called once per frame
		public void Process()
		{
			CheatCommand targetCheat = null;

			int i = 0;
			while (i < allCheats.Count)
			{
				CheatCommand cheat = allCheats[i];
				if (Keyboard.current[cheat.mainKey].wasPressedThisFrame)
				{
					targetCheat = cheat;
					while (i < allCheats.Count)
					{
						CheatCommand cheat02 = allCheats[i];
						if (cheat02.modifierKey != Key.None && targetCheat.mainKey == cheat02.mainKey &&
							Keyboard.current[cheat02.modifierKey].isPressed)
						{
							targetCheat = cheat02;
							i = allCheats.Count;
						}

						i++;
					}
				}

				i++;
			}

			targetCheat?.callback();
		}

		public void EndPlayState()
		{
			//running = false;
			if (verbose)
				Debug.Log("EndPlayState");
			dynamicCheats.Clear();
			staticCheats.Clear();
			allCheats.Clear();
		}

		public DebugArrow GetNewArrow(Color c, string _name, Transform _parent, float _lengthMultiplier = 1,
			float _fat = 0)
		{
			DebugArrow da = Instantiate(debugArrowPrefab, _parent);
			da.transform.localPosition = Vector3.zero;
			da.Init(c, _name, _fat);
			da.SetLengthMultiplier(_lengthMultiplier);
			da.SetLength(1);
			return da;
		}

		public void DumpCheatInfo()
		{
			Debug.Log("Cheats: ");
			foreach (string s in cheatInfo)
			{
				Debug.Log(s);
			}
		}

		// ////////////////////////// CHEATS METHODS ////////////////////////////////////////////////////////////

		void SpawnPrefab()
		{
			if (somePrefab == null)
				return;

			GameObject p = Instantiate(somePrefab, spawnPrefabPos, Quaternion.identity);
		}

		public void SetDebugObject(Transform t)
		{
			SetDebugObject(t.position, t.rotation);
		}

		public void SetDebugObject(Vector3 pos, Quaternion rot = default)
		{
			if (!debugObj)
				debugObj = Instantiate(debugObjPrefab);

			debugObj.transform.position = pos;
			debugObj.transform.rotation = rot;
		}

		public void NewDebugObject(Vector3 pos, Quaternion rot = default)
		{
			GameObject newGO = Instantiate(debugObjPrefab);

			newGO.transform.position = pos;
			newGO.transform.rotation = rot;
		}
	}

}