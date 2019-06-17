using UnityEngine;
using System.Collections;

public class generate : MonoBehaviour {
	
	public GameObject Obstacle1;
	public int NumToGenerateForLevel = 30;
	public bool generating = true;
	public float RepeatRate = 1.5f;
	public float theTimeRate = .5f;
	
	private Transform[] obstacles;
	private int PoolSize = 20;
	private int currentPoolIndex;
	private int numGenerated = 0;
	private bool paused = false;
	
	int score = 0;
	
	void Awake()
	{
		obstacles = new Transform[PoolSize];
		currentPoolIndex = 0;
	}
	
	// Use this for initialization
	void Start()
	{
		InvokeRepeating("CreateObstacle", theTimeRate, RepeatRate);
	}
	
	void Update()
	{

	}

	void CreateObstacle()
	{
		// Pool
		if (obstacles[currentPoolIndex] != null)
		{
			Destroy(obstacles[currentPoolIndex].gameObject);
			obstacles[currentPoolIndex] = null;
		}
		
		//obstacles[currentPoolIndex] = Instantiate(Obstacle1) as Transform;

		Vector3 v3Pos = new Vector3(1.10f, .5f, 10);
		v3Pos = Camera.main.ViewportToWorldPoint(v3Pos);
		obstacles[currentPoolIndex] = Instantiate(Obstacle1,v3Pos,Quaternion.identity) as Transform;

		currentPoolIndex++;

		if(currentPoolIndex >= NumToGenerateForLevel)
		{
			generating = false;
			CancelInvoke("CreateObstacle");
		}

		if (currentPoolIndex >= PoolSize) currentPoolIndex = 0;
	}
}
