
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif
#if APPADVISORY_ADS
using AppAdvisory.Ads;
#endif
#if VS_SHARE
using AppAdvisory.SharingSystem;
#endif
using AppAdvisory.UI;
#if APPADVISORY_LEADERBOARD
using AppAdvisory.social;
#endif

namespace AppAdvisory.ab
{
	public class GameManagerAB : MonoBehaviour {

		public int numberOfPlayToShowInterstitial = 5;

		public string VerySimpleAdsURL = "http://u3d.as/oWD";

		Text textPreviousLevel;
		Text textNextLevel;
		public Text levelText;

		public Button buttonPrevious;
		public Button buttonNext;

		public static GameManagerAB self;

		List<DotManagerAB> DotsBottom;
		List<DotManagerAB> DotsTop;

		Vector3 rotateVectorTOP;

		private Ease easeType = Ease.Linear;

		private LoopType loopType = LoopType.Yoyo;

		private float rotateCircleDelay = 6f;

		[SerializeField] private int numberDotsToCreate;

		[SerializeField] private int numberDotsOnCircle;

		float centerCircle;

		AudioSource audioSource;
		public AudioClip MUSIC;
		public AudioClip FX_FAIL;
		public AudioClip FX_SUCCESS;
		public AudioClip FX_SHOOT;
		void PlaySoundFail()
		{
			audioSource.PlayOneShot(FX_FAIL);
		}
		void PlaySoundSuccess()
		{
			audioSource.PlayOneShot(FX_SUCCESS);
		}
		void PlaySoundShoot()
		{
			audioSource.PlayOneShot(FX_SHOOT);
		}


		SpriteRenderer[] allSprites
		{
			get
			{
				var f = FindObjectsOfType<SpriteRenderer>();

//				return Array.FindAll(f, spr => spr.name.CompareTo("Sprite") == 0);

				return f;
			}
		}

		//		public static Color GetDotColor()
		//		{
		//			return self.DotColor;
		//		}

		public static bool ISGameOver
		{
			get
			{
				return self.isGameOver;
			}
		}

		public Color FailColor;

		public Color SuccessColor;

		public Color BackgroundColor;

		public Color DotColor;

		public Color PhareColor;

		public int Level;

		[NonSerialized]
		public bool success;
		public bool isGameOver;

		Camera cam;
		float height;

		public Transform CIRCLE;
		public Transform DOTS_PARENT;
		float positionTouchBorder;

		public SpriteRenderer CircleCenterSprite;

		public GameObject DotPrefab;

		public float speed = 1f;

		float sizeBorder;

		public Transform CircleSprite;

		void Awake()
		{
			self = this;

			audioSource = GetComponent<AudioSource>();

			cam = Camera.main;
			height = 2f * cam.orthographicSize;

			isGameOver = true;

			CIRCLE.position = new Vector3 (0, height / 3f, 0);

			cam.transform.position = new Vector3 (0, 0, -10);


			DOTween.Init();

			sizeDot = -1;
		}

		void Start()
		{
			CreateGame ();

			foreach(SpriteRenderer s in allSprites)
			{
				s.color = BackgroundColor;
			}
		}

		void CleanMemory()
		{
			GC.Collect();
			Application.targetFrameRate = 60;
			Time.fixedDeltaTime = 1f/60f;
			Time.maximumDeltaTime = 3 * Time.fixedDeltaTime;
			GC.Collect();
		}

		int levelPlayed
		{
			get
			{
				int lp = PlayerPrefs.GetInt ("LEVEL_PLAYED", 1);

				if(lp == 0)
				{
					lp = 1;
					levelPlayed = 1;
				}


				return lp;
			}
			set
			{
				int lp = value;
				if(lp <= 0)
					lp = 1;

				PlayerPrefs.SetInt ("LEVEL_PLAYED", lp);
				PlayerPrefs.Save();
			}
		}

		int level
		{
			get
			{
				int lp = PlayerPrefs.GetInt ("LEVEL", 1);

				if(lp == 0)
				{
					lp = 1;
					level = 1;
				}

				return lp;
			}
			set
			{
				int lp = value;
				if(lp <= 0)
					lp = 1;

				PlayerPrefs.SetInt ("LEVEL", lp);
				PlayerPrefs.Save();
			}
		}

		void SetLevelButtonsVisibility()
		{
			if(levelPlayed < level)
				buttonNext.gameObject.SetActive(true);
			else
				buttonNext.gameObject.SetActive(false);

			if(levelPlayed > 1)
				buttonPrevious.gameObject.SetActive(true);
			else
				buttonPrevious.gameObject.SetActive(false);
		}

		public void OnClickedButtonLast()
		{
			int levelPlayerNew = Mathf.Max(0,levelPlayed - 1);

			print("levelPlayerNew = " + levelPlayerNew);

			levelPlayed = levelPlayerNew;

			CleanMemory();

			SetTextLevel();
		}

		public void OnClickedButtonNext()
		{

			int levelPlayerNew = Mathf.Min(level, levelPlayed + 1);

			levelPlayed = levelPlayerNew;

			CleanMemory();

			SetTextLevel();
		}

		void RestartLevel(bool success)
		{
			if (success)
			{
				levelPlayed++;

				int max = Mathf.Max (level, levelPlayed);

				level = max;

				#if APPADVISORY_LEADERBOARD
				LeaderboardManager.ReportScore(level);
				#endif
			}

			CleanMemory();

			#if UNITY_5_3_OR_NEWER
			SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
			#else
			Application.LoadLevel(Application.loadedLevel);
			#endif

			CleanMemory();
		}

		public static void DOGameOver()
		{
			self.GameOver();
		}

		void LevelCleared()
		{
			ShowAds();

			#if VS_SHARE
			VSSHARE.DOTakeScreenShot();
			#endif

			PlaySoundSuccess();
			isGameOver = true;

			canShoot = false;


			DOColorSuccess(() => {
				RestartLevel (true);
			});
		}

		void GameOver()
		{
			ShowAds();

			#if VS_SHARE
			VSSHARE.DOTakeScreenShot();
			#endif

			DOTween.KillAll();

			StopAllCoroutines ();

			PlaySoundFail();

			isGameOver = true;

			canShoot = false;

			DOColorFail(() => {
				RestartLevel (false);
			});
		}

		void DOColorFail(Action callback)
		{
			canShoot = false;

			foreach(var s in allSprites)
			{
				s.DOColor(FailColor,0.1f).SetEase(Ease.Linear).SetDelay(1f/30f);;
			}

			DOVirtual.DelayedCall(0.3f, () => {
				foreach(var s in allSprites)
				{
					s.DOColor(BackgroundColor,0.3f).SetEase(Ease.Linear);
				}

				DOVirtual.DelayedCall(0.40f, () => {
					if(callback != null)
						callback();
				});
			});
		}

		void DOColorSuccess(Action callback)
		{
			canShoot = false;

			foreach(var s in allSprites)
			{
				s.DOColor(SuccessColor,0.1f).SetEase(Ease.Linear);
			}

			DOVirtual.DelayedCall(0.3f, () => {
				foreach(var s in allSprites)
				{
					s.DOColor(BackgroundColor,0.3f).SetEase(Ease.Linear);
				}

				DOVirtual.DelayedCall(0.40f, () => {
					if(callback != null)
						callback();
				});
			});
		}

		private void ContinueGame()
		{
			StartCoroutine (SetBoolWithDelay (1));
		}

		IEnumerator SetBoolWithDelay(float delay)
		{
			yield return new WaitForSeconds (delay);

			StopCoroutine ("ShootDot");

			foreach (Transform t in DOTS_PARENT)
			{
				t.GetComponent<DotManagerAB>().DotSprite.color = this.DotColor;
			}

			if (lastShootTop != null)
			{
				lastShootTop.Replace ();
				lastShootTop.transform.SetParent (transform);
				DotsBottom.Add (lastShootTop);
			}
			if (lastShootBottom != null)
			{
				lastShootBottom.Replace ();
				lastShootBottom.transform.SetParent (transform);
			}

			StartCoroutine (PositioningDots ());

			yield return new WaitForSeconds (delay);

			LaunchRotateCircle ();

			isGameOver = false;
			canShoot = true;
		}


		public void OnclickedPlay ()
		{
			Application.targetFrameRate = 60;
			GC.Collect();
			Time.fixedDeltaTime = 1f/60f;
			Time.maximumDeltaTime = 3 * Time.fixedDeltaTime;

			success = false;
			canShoot = true;
			isGameOver = false;

			LaunchRotateCircle ();

			foreach(SpriteRenderer s in allSprites)
			{
				s.color = BackgroundColor;
				s.DOColor(DotColor,0.3f)
					.SetEase(Ease.Linear);
			}

			#if VS_SHARE
			VSSHARE.DOHideScreenshotIcon();
			#endif
		}



		void SetTextLevel()
		{
			textPreviousLevel = FindObjectOfType<UIControllerAB>().textLast;
			textNextLevel = FindObjectOfType<UIControllerAB>().textBest;

			textPreviousLevel.gameObject.SetActive(false);
			textNextLevel.text = "LEVEL\n" + levelPlayed;

			levelText.text = "LEVEL " + level;

			SetLevelButtonsVisibility();
		}

		public void CreateGame()
		{
			SetTextLevel();

			transformVectorLines = new List<Transform> ();

			canShoot = false;

			cam.orthographicSize = 20f;
			cam.transform.position = new Vector3 (0, 0, -10);

			StopAllCoroutines ();

			isGameOver = true;
			cam.backgroundColor = this.BackgroundColor; //this.GRAY;
			CircleCenterSprite.color = this.DotColor;

			Level l = LevelManagerAB.GetLevel (levelPlayed);

			numberDotsOnCircle = l.numberDotsOnCircle;
			numberDotsToCreate = l.numberDotsToCreate;

			rotateCircleDelay = l.rotateDelay;
			easeType = l.rotateEaseType;
			loopType = l.rotateLoopType;

			centerCircle = 0 * height / 4.5f;

			CIRCLE.position = new Vector3 (0, centerCircle, 0);

			positionTouchBorder = centerCircle - l.sizeRayonRation * CIRCLE.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y;

			rotateVectorTOP = new Vector3 (0, 0, 1);

			if (Level % 2 == 0)
				rotateVectorTOP = new Vector3 (0, 0, -1);

			CIRCLE.rotation = Quaternion.Euler (Vector3.zero);

			DOTS_PARENT.rotation = Quaternion.Euler (Vector3.zero);

			DOTS_PARENT.localScale = Vector3.one;

			levelText.text = "LEVEL " + levelPlayed;

			CreateDotOnCircle ();

			CreateListDots ();

			StartCoroutine (PositioningDots ());

			PositioningCamera ();
		}

		void PositioningCamera()
		{
			float H = Screen.height;
			float sizeBanner = 50f;

			if (PlayerPrefs.HasKey ("NOADS"))
			{
				if (PlayerPrefs.GetInt ("NOADS") == 1)
				{
					sizeBanner = 0f;
				}
			} 

			float ratio = (H - sizeBanner) / H;

			float positionCamY = (1f - ratio) * this.height;

			cam.transform.position = new Vector3 (0, -positionCamY, cam.transform.position.z);
		}

		public List<Transform> transformVectorLines;

		void LaunchRotateCircle()
		{
			CIRCLE.rotation = Quaternion.Euler (Vector3.zero);

			DOTS_PARENT.DORotate (rotateVectorTOP * 360, rotateCircleDelay, RotateMode.FastBeyond360)
				.SetEase(easeType)
				.SetLoops(-1, loopType);
		}

		DotManagerAB InstantiateDot(Vector3 posTOP, Quaternion rot, Transform parent)
		{
			var dm = InstantiateDot();

			dm.transform.SetParent(parent);
			dm.transform.position = posTOP;
			dm.transform.rotation = rot;

			return dm;
		}


		void CreateDotOnCircle()
		{
			Time.timeScale = 1;

			GameObject prefab = this.DotPrefab;
			Quaternion rot = prefab.transform.rotation;

			Vector3 posTOP = new Vector3 (0, positionTouchBorder, 0);
			Transform parentTOP = DOTS_PARENT;


			float numberTOP = numberDotsOnCircle;


			for (int i = 0; i < (int)numberTOP ; i++)
			{

				CIRCLE.rotation = Quaternion.Euler( new Vector3 (0, 0, i * 360f / (int)numberTOP) );

				DotManagerAB dm = InstantiateDot(posTOP, rot, parentTOP);

				dm.DOParentAndActivateLine (dm.transform.position,dm.transform.parent, true);
			}
		}

		Vector3 GetPositionDot(int i)
		{
			Vector3 target = new Vector3 (0f, -10 - i * sizeDot * 1.2f, 0);
			return target;
		}

		void SetPositionDot(int i, Transform t)
		{
			Vector3 target = GetPositionDot(i);
			t.position = target;
		}

		DotManagerAB InstantiateDot()
		{
			var go = Instantiate(this.DotPrefab) as GameObject;
			return go.GetComponent<DotManagerAB> ();
		}

		void CreateListDots()
		{
			canShoot = false;
			DotsBottom = new List<DotManagerAB>();
			DotsTop = new List<DotManagerAB>();

//			if (sizeDot == 0)
//			{
//				DotManager dm = InstantiateDot();
//
//				sizeDot = dm.DotSprite.bounds.size.x;
//			}
//
			float numberTOP = numberDotsToCreate;

			for (int i = 0; i < numberTOP; i++)
			{
				DotManagerAB dm = InstantiateDot();

				if(sizeDot == -1)
					sizeDot = dm.DotSprite.bounds.size.x;

				dm.GetComponent<Collider2D>().enabled = false;
				SetPositionDot(i,dm.transform);
				DotsBottom.Add (dm);


				DotManagerAB dm2 = InstantiateDot();
				dm2.GetComponent<Collider2D>().enabled = false;
				dm2.transform.position = new Vector3(dm.transform.position.x, -dm.transform.position.y, dm.transform.position.z);
				DotsTop.Add(dm2);
			}
		}

		private void OnColorUpdated(Color color)
		{
			cam.backgroundColor = color;
		}

		private void OnCamSizeUpdated(float size)
		{
			cam.orthographicSize = size;
		}

		IEnumerator OnAnimationGameToMenuCompleted(float wait)
		{
			yield return new WaitForSeconds (wait);

			CreateGame ();
			isGameOver = true;

			ShowAds ();
		}

		/// <summary>
		/// If using Very Simple Ads by App Advisory, show an interstitial if number of play > numberOfPlayToShowInterstitial: http://u3d.as/oWD
		/// </summary>
		public void ShowAds()
		{
			int count = PlayerPrefs.GetInt("GAMEOVER_COUNT",0);
			count++;
			PlayerPrefs.SetInt("GAMEOVER_COUNT",count);
			PlayerPrefs.Save();

			#if APPADVISORY_ADS
			if(count > numberOfPlayToShowInterstitial)
			{
			print("count = " + count + " > numberOfPlayToShowINterstitial = " + numberOfPlayToShowInterstitial);

			if(AdsManager.instance.IsReadyInterstitial())
			{
			print("AdsManager.instance.IsReadyInterstitial() == true ----> SO ====> set count = 0 AND show interstial");
			AdsManager.instance.ShowInterstitial();
			PlayerPrefs.SetInt("GAMEOVER_COUNT",0);
			}
			else
			{
			#if UNITY_EDITOR
			print("AdsManager.instance.IsReadyInterstitial() == false");
			#endif
		}

	}
	else
	{
		PlayerPrefs.SetInt("GAMEOVER_COUNT", count);
	}
	PlayerPrefs.Save();
			#else
	if(count >= numberOfPlayToShowInterstitial)
	{
		Debug.LogWarning("To show ads, please have a look to Very Simple Ad on the Asset Store, or go to this link: " + VerySimpleAdsURL);
		Debug.LogWarning("Very Simple Ad is already implemented in this asset");
		Debug.LogWarning("Just import the package and you are ready to use it and monetize your game!");
		Debug.LogWarning("Very Simple Ad : " + VerySimpleAdsURL);
		PlayerPrefs.SetInt("GAMEOVER_COUNT",0);
	}
	else
	{
		PlayerPrefs.SetInt("GAMEOVER_COUNT", count);
	}
	PlayerPrefs.Save();
			#endif
}


void Update ()
{

	if (Input.GetMouseButtonDown (0) && !isGameOver && canShoot)
	{
		StartCoroutine (ShootDot());
	}
}

bool canShoot;

DotManagerAB lastShootTop;
DotManagerAB lastShootBottom;

IEnumerator ShootDot()
{
	DotManagerAB dBottom = DotsBottom[0];
	DotManagerAB dTop = DotsTop[0];

	canShoot = false;

	lastShootTop = null;
	lastShootBottom = null;

	StopCoroutine("PositioningDots");
	StopCoroutine("MoveStartPositionDot");

	if (dBottom != null)
	{
		dBottom.GetComponent<Collider2D>().enabled = true;
		dBottom.GetComponent<Collider2D>().isTrigger = true;
	}
	if (dTop != null)
	{
		dTop.GetComponent<Collider2D>().enabled = true;
		dTop.GetComponent<Collider2D>().isTrigger = true;
	}

	for (int i = 0; i < DotsBottom.Count; i++)
	{
		DotsBottom [i].transform.localScale = Vector3.one;

		SetPositionDot(i, DotsBottom [i].transform);
	}

	yield return null;


	if (DotsBottom.Count != 0)
	{

		PlaySoundShoot();

		lastShootBottom = dBottom;
		lastShootTop = dTop;

		DotsBottom.RemoveAt (0);
		DotsTop.RemoveAt (0);

		StartCoroutine (PositioningDots ());

		Vector3 targetBottom = new Vector3 (0, positionTouchBorder, 0);
		Vector3 targetTop = new Vector3 (0, -positionTouchBorder, 0);

		while (true)
		{
			float step = speed * Time.deltaTime;

			if (dTop != null)
			{
				dBottom.transform.position = Vector3.MoveTowards (dBottom.transform.position, targetBottom, step);
				dTop.transform.position = Vector3.MoveTowards (dTop.transform.position, targetTop, step);
			}

			if (dBottom != null)
			{
				if (dBottom.transform.position == targetBottom || isGameOver)
				{

					canShoot = true;


					break;
				}

			}


			yield return new WaitForEndOfFrame ();
		}

		dBottom.SetParentShootedDot(DOTS_PARENT, true, true);
		dTop.SetParentShootedDot(DOTS_PARENT, true, false);


		yield return new WaitForSeconds (0.001f);

		if (DotsBottom.Count == 0 && !isGameOver)
		{

			success = true;
		}

		if (success && !isGameOver) 
		{
			LevelCleared();
		}

		for (int i = 0; i < DotsBottom.Count; i++)
		{
			DotsBottom [i].transform.localScale = Vector3.one;

			SetPositionDot(i, DotsBottom [i].transform);
		}
	}
}


float sizeDot = 0;
IEnumerator PositioningDots()
{
	for (int i = 0; i < DotsBottom.Count; i++)
	{
		if (DotsBottom.Count > 0)
		{
			DotsBottom [i].transform.localScale = Vector3.one;
			DotsBottom [i].transform.DOMove (GetPositionDot(i), speed / 500f).SetEase(Ease.Linear);
			DotsBottom [i].GetComponent<Collider2D>().enabled = false;

			DotsTop [i].transform.localScale = Vector3.one;
			DotsTop [i].transform.DOMove (new Vector3(GetPositionDot(i).x, -GetPositionDot(i).y, GetPositionDot(i).z), speed / 500f).SetEase(Ease.Linear);
			DotsTop [i].GetComponent<Collider2D>().enabled = false;

			yield return null;
		}
	}
}

public void OnApplicationPause(bool pause)
{
	if (!pause)
	{
		//Debug.Log ("OnApplicationPause FALSE");
		Resources.UnloadUnusedAssets ();
		Time.timeScale = 1.0f;
	} else {
		//Debug.Log ("OnApplicationPause TRUE");
		Resources.UnloadUnusedAssets ();
		Time.timeScale = 0.0f;
	}
}  

void OnApplicationQuit()
{
	PlayerPrefs.Save();
}

void OnDetectAppUpdated()
{
	//Debug.Log("A new version is installed. Current version: " + //UniRate.Instance.applicationVersion);
	PlayerPrefs.SetInt ("PLAY_COUNT",0);
}

void OnUserAttemptToRate()
{
	//Debug.Log("Yeh, great, user want to rate us!");
	PlayerPrefs.SetInt ("PLAY_COUNT",-1000);
}

void OnUserDeclinedToRate()
{
	//Debug.Log("User declined the rate prompt.");
	PlayerPrefs.SetInt ("PLAY_COUNT",-1000);
}

void OnUserWantReminderToRate()
{
	//Debug.Log("User wants to be reminded later.");
	PlayerPrefs.SetInt ("PLAY_COUNT",-15);
}
}
}