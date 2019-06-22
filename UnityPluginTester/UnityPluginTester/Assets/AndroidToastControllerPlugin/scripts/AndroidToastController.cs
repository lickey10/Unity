using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class AndroidToastController : MonoBehaviour {
	private AndroidToastControllerHelperClass androidToastControllerHelperClass;

	// Use this for initialization
	void Start () {
		androidToastControllerHelperClass = AndroidToastControllerHelperClass.Instance;
	}

	public void DisplayText()
	{
		androidToastControllerHelperClass.DisplayText("Center");
	}

	public void DisplayTextBottomCenter()
	{
		androidToastControllerHelperClass.DisplayTextBottomCenter("Bottom Center",false);
	}
	
	public void DisplayTextTopCenter()
	{
		androidToastControllerHelperClass.DisplayTextTopCenter("Top Center",false);
	}
	
	public void DisplayTextLeftCenter()
	{
		androidToastControllerHelperClass.DisplayTextLeftCenter("Left Center",false);
	}
	
	public void DisplayTextRightCenter()
	{
		androidToastControllerHelperClass.DisplayTextRightCenter("Right Center",false);
	}
	
	public void DisplayTextBottomCenterWOffset()
	{
		androidToastControllerHelperClass.DisplayTextBottomCenterWOffset("Bottom Center W/Offset",false, 100, 100);
	}
	
	public void DisplayTextTopCenterWOffset()
	{
		androidToastControllerHelperClass.DisplayTextTopCenterWOffset("Top Center W/Offset",false, 100, 100);
	}
	
	public void DisplayTextLeftCenterWOffset()
	{
		androidToastControllerHelperClass.DisplayTextLeftCenterWOffset("Left Center W/Offset",false, 100, 100);
	}
	
	public void DisplayTextRightCenterWOffset()
	{
		androidToastControllerHelperClass.DisplayTextRightCenterWOffset("Right Center W/Offset",false, 100, 100);
	}
}

