using UnityEngine;
using System.Collections;

public class MovingObject{
    public DirectionEnum direction; //-1 or 1;
    public Transform transform;
	public int Row;

//    public MovingObject(DirectionEnum dir, Transform trans)
//    {
//        direction = dir;
//        transform = trans;
//    }

	public MovingObject(DirectionEnum dir, Transform trans, int Row)
	{
		direction = dir;
		transform = trans;
		this.Row = Row;
	}
}

[System.Serializable]
public enum DirectionEnum{left = -1, right= 1}

[System.Serializable]
public enum RowEnum{Top = 1, Middle= 2,Bottom=3}

public class ObjectSpawner : MonoBehaviour {

    public Transform objectPrefab;
    public DirectionEnum spawnDirection = DirectionEnum.right;
	public RowEnum Row = RowEnum.Top;
    public static ObjectSpawner SP;

    private float farLeft;
    private float farRight;    
    private float lastSpawnTime;
    private float spawnInterval;   
	private bool firstRun = true;

	void Awake () {
        SP = this;
        
        spawnInterval = Random.Range(3.5f, 5.5f);
        lastSpawnTime = Time.time + Random.Range(0.0f, 1.5f);
	}

    
	void Update () {
        //Spawn new object..
        if ((lastSpawnTime + spawnInterval) < Time.time || firstRun)
        {
			firstRun = false;

            SpawnObject();
        }      
	}

    void SpawnObject()
    {        
        lastSpawnTime = Time.time;
        spawnInterval *= 0.99f;//Speed up spawning

        DirectionEnum direction = spawnDirection; //-1 or 1

        Transform newObj = (Transform)Instantiate(objectPrefab, transform.position, transform.rotation);
		newObj.tag = "ShootingObject";
		MovingObject movObj = new MovingObject(direction, newObj,(int)Row);        
        GameManager.SP.AddTarget( movObj );
    }
}
