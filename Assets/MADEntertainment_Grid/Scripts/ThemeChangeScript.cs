using UnityEngine;
using System.Collections;

public class ThemeChangeScript : MonoBehaviour 
{
	public byte R1,R2,R3,G1,G2,G3,B1,B2,B3;

	void Start () 
	{
	
	}

	public void ChangeTheme () 
	{
		switch (Random.Range (0, 2)) 
		{
		case 0:
			R1 = 255;
			G1 = 90;
			B1 = 24;

			R2 = 255;
			G2 = 255;
			B2 = 0;

			R3 = 77;
			G3 = 66;
			B3 = 109;
			break;
		
		case 1:
			R1 = 121;
			G1 = 183;
			B1 = 225;

			R2 = 117;
			G2 = 94;
			B2 = 210;

			R3 = 163;
			G3 = 234;
			B3 = 122;
			break;
		}
	}
}
