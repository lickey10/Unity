using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtentionMethods 
{
	public static void Activate(this GameObject gameObject) {
		if(gameObject.GetComponent<WindowTransition_Fade>() != null) {
			gameObject.GetComponent<WindowTransition_Fade>().Activate();
		}
	}

	public static void Deactivate(this GameObject gameObject) {
		if(gameObject.GetComponent<WindowTransition_Fade>() != null) {
			gameObject.GetComponent<WindowTransition_Fade>().Deactivate();
		}
	}

	public static void Activate(this Transform transform) {
		if(transform.GetComponent<WindowTransition_Fade>() != null) {
			transform.GetComponent<WindowTransition_Fade>().Activate();
		}
	}

	public static void Deactivate(this Transform transform) {
		if(transform.GetComponent<WindowTransition_Fade>() != null) {
			transform.GetComponent<WindowTransition_Fade>().Deactivate();
		}
	}

	public static int TryParseInt(this string s, int defaultResult = 0)
	{
		int result = defaultResult;
		int.TryParse (s, out result);
		return result;
	}

	public static void Shuffle<T>(this List<T> list)  
	{  
		System.Random rng = new System.Random();  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}
}
