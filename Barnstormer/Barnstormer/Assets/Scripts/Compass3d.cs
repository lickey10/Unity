using UnityEngine;
using System.Collections;

public class Compass3d : MonoBehaviour {

	public Texture2D compass; // compass image 
	public Texture2D needle; // needle image (same size of compass) 
	public Rect r = new Rect(10, 10, 200, 200); // rect where to draw compass 
	public Transform Target;
	public Transform Player;
	GameObject Selected;
	// the tag to search for (set this value in the inspector) 
	string searchTag = "Waypoint";
	// the frequency with which to re-scane for new nearest target in seconds // (set in inspector) 
	float scanFrequency = 1f;

	void Start () 
	{
		// set up repeating scan for new targets: 
		InvokeRepeating("ScanForTarget", 0, scanFrequency );
	}

	void Update()
	{
		if (Target != null) 
		{ 
			transform.LookAt(Target); 
			transform.rotation = transform.rotation * Quaternion.Euler(90,90,0);
		} 
	}
	
	void OnGUI()
	{

	}

	void scanForTarget() 
	{ 
		// this should be called less often, because it could be an expensive 
		// process if there are lots of objects to check against 
		Target = GetNearestTaggedObject();
		
	}

	Transform GetNearestTaggedObject() 
	{ 
		// and finally the actual process for finding the nearest object:
		float nearestDistanceSqr = Mathf.Infinity;
		GameObject[] taggedGameObjects = GameObject.FindGameObjectsWithTag(searchTag); 
		Transform nearestObj = null;
		
		// loop through each tagged object, remembering nearest one found
		foreach (GameObject obj in taggedGameObjects) 
		{
			var objectPos = obj.transform.position;
			var distanceSqr = (objectPos - transform.position).sqrMagnitude;
			
			if (distanceSqr < nearestDistanceSqr) 
			{
				nearestObj = obj.transform;
				nearestDistanceSqr = distanceSqr;
			}
		}
		
		return nearestObj;
	}
	
//	void SelectClosest()
//	{
//		Selected = null;
//		float closestDistance = 0;
//
//		foreach (GameObject waypoint in GameObject.FindGameObjectsWithTag("Waypoint"))
//		{
//			float distance = distanceSquaredTo(Player,waypoint);
//
//			if (distance < closestDistance)
//			{
//				closestDistance = distance;
//				Selected = waypoint;
//			}
//		}
//
//		Target = Selected.transform;
//	}
//	
//	float distanceSquaredTo(Transform source, GameObject target)
//	{
//		return Vector3.SqrMagnitude(source.position - target.transform.position);
//	}
}
