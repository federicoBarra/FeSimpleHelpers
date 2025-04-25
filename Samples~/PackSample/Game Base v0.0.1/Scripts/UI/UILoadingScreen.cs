using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FeSimpleHelpers.GameBase_v001
{
	public class UILoadingScreen : MonoBehaviourSingleton<UILoadingScreen>
	{
		public UILoadingNoInterface noInterface;
		public UILoadingSimple simple;
		public UILoadingBigLogo bigLogo;
		public UILoadingMulti multi;
		public bool loading;

		Dictionary<LoaderManager.LoadingType, UILoadingState> loadingStates = new Dictionary<LoaderManager.LoadingType, UILoadingState>();

		UILoadingState loadingState = null;

		protected override void Awake()
		{
			base.Awake();
			LoaderManager.OnLoadingStart += LoadingStart;
			LoaderManager.OnLoadingEnd += LoadingEnd;

			loadingStates.Add(LoaderManager.LoadingType.NoInterface, noInterface);
			loadingStates.Add(LoaderManager.LoadingType.Simple, simple);
			loadingStates.Add(LoaderManager.LoadingType.BigLogo, bigLogo);
			loadingStates.Add(LoaderManager.LoadingType.Multi, multi);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			LoaderManager.OnLoadingStart -= LoadingStart;
			LoaderManager.OnLoadingEnd -= LoadingEnd;
		}

		void LoadingStart(LoaderManager lm)
		{
			if (loadingState != null)
				Debug.LogError("This shouldn't happen");

			loadingState = loadingStates[lm.loadingType];
			loadingState.Start();
			loading = true;
		}

		void Update()
		{
			if (!loading)
				return;
			loadingState.Update(LoaderManager.Get().loadingProgress);
		}

		void LoadingEnd(LoaderManager lm)
		{
			loadingState.End();
			loading = false;
			loadingState = null;
		}
	}

	[Serializable]
	public abstract class UILoadingState
	{
		public GameObject panel;

		public virtual void Start()
		{
			panel.SetActive(true);
		}

		public virtual void Update(float loadingProgress)
		{
		}

		public virtual void End()
		{
			panel.SetActive(false);
		}
	}

	[Serializable]
	public class UILoadingNoInterface : UILoadingState
	{
	}

	[Serializable]
	public class UILoadingSimple : UILoadingState
	{
		public Canvas canvas;
		public Image image;
		public Color imageColor;
		public float duration = 0;
		public float sinSpeed = 2;

		public override void Start()
		{
			base.Start();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			duration = 0;
		}

		public override void Update(float loadingProgress)
		{
			base.Update(loadingProgress);
			duration += Time.deltaTime;
			imageColor.a = (MathF.Sin(duration * sinSpeed) + 1) / 2f;
			image.color = imageColor;
		}
	}

	[Serializable]
	public class UILoadingBigLogo : UILoadingState
	{
		public Canvas canvas;
		public Image image;

		public override void Start()
		{
			base.Start();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			//TODO fix this
			//canvas.renderMode = RenderMode.ScreenSpaceCamera;
			//Camera c = Camera.main;
			//canvas.worldCamera = c;
			//canvas.planeDistance = 1;
		}

		public override void Update(float loadingProgress)
		{
			base.Update(loadingProgress);
			image.fillAmount = loadingProgress;
		}
	}

	[Serializable]
	public class UILoadingMulti : UILoadingState
	{

	}
}


