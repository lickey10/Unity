using UnityEngine;
using System.Collections;

public class lightsOut : MonoBehaviour {

	private static lightsOut instance;
	Color FirstLightOutColor = new Color(0.67f,0.67f,0.67f);
	 Color SecondLightOutColor = new Color(0.46f,0.46f,0.46f);
	 Color ThirdLightOutColor = new Color(0.29f,0.29f,0.29f);
	 Color FourthLightOutColor = new Color(0.18f,0.18f,0.18f);

	private bool firstLightOut = false;
	private bool secondLightOut = false;
	private bool thirdLightOut = false;
	private bool fourthLightOut = false;

	private int currentColor = 0; 
	float changeTime; 
	Color[] colors = new Color[2];
	
	public static lightsOut Instance
	{
		get
		{
			if(instance == null)
			{
				instance = new GameObject("lightsOut").AddComponent<lightsOut> ();
				
				DontDestroyOnLoad(Instance);
			}
			
			return instance;
		}
	} 
	
	void NextColor()
	{ 
		if(currentColor>=colors.Length-1)
		{ currentColor = 0; }
		else
		{ currentColor +=1; } 
	}
	
	void SetChangeTime(float ct){ changeTime = ct; }


	// Use this for initialization
	void Start () {
		colors[0] = Color.gray;
		colors[1] = Color.black;
//		FirstLightOutColor.grayscale.gamma = 1;
//		SecondLightOutColor.a = 2;
//		ThirdLightOutColor.a = 20;
//		FourthLightOutColor.a = 1;


//		FirstLightOutColor.b = 192;
//		FirstLightOutColor.r = 192;
//		FirstLightOutColor.g = 192;
//
//		SecondLightOutColor.b = 148;
//		SecondLightOutColor.r = 152;
//		SecondLightOutColor.g = 148;
//
//		ThirdLightOutColor.b = 113;
//		ThirdLightOutColor.r = 113;
//		ThirdLightOutColor.g = 113;
//
//		FourthLightOutColor.b = 83;
//		FourthLightOutColor.r = 83;
//		FourthLightOutColor.g = 83;


	}
	
	// Update is called once per frame
	void Update () {

		RenderSettings.ambientLight = Color.Lerp (RenderSettings.ambientLight, colors[currentColor], changeTime*Time.deltaTime);
		
		//this is just to test
		if(Input.GetKeyDown("space")){
			NextColor();
		}
	}

	void OnCollisionEnter(Collision collisionInfo ) {
		Destroy(gameObject.collider);
		TurnOffALight();
	}

	public void ResetLights()
	{
		firstLightOut = false;
		secondLightOut = false;
		thirdLightOut = false;
		fourthLightOut = false;
	}

	public void TurnOffALight()
	{
		if(!firstLightOut)
		{
			RenderSettings.ambientLight = FirstLightOutColor;
			gamestate.Instance.currentLevelTreasureMultiplier += 1;
			firstLightOut = true;
		}

		else if(!secondLightOut)
		{
			RenderSettings.ambientLight = SecondLightOutColor;
			gamestate.Instance.currentLevelTreasureMultiplier += 1;
			secondLightOut = true;
		}

		else if(!thirdLightOut)
		{
			RenderSettings.ambientLight = ThirdLightOutColor;
			gamestate.Instance.currentLevelTreasureMultiplier += 1;
			thirdLightOut = true;
		}

		else if(!fourthLightOut)
		{
			RenderSettings.ambientLight = FourthLightOutColor;
			gamestate.Instance.currentLevelTreasureMultiplier += 2;
			fourthLightOut = true;
		}
	}
}
