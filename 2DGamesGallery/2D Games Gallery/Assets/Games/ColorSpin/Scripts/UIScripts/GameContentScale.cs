using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContentScale : MonoBehaviour 
{
	void Awake()
	{
		float screenAspect = (((float) Screen.height) / ((float) Screen.width));
		transform.localScale = (screenAspect > 1.5F) ? (Vector3.one) : (Vector3.one * 1.2F);
	}
}
