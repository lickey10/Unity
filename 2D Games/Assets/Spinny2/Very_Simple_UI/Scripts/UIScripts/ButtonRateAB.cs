using UnityEngine;
using System.Collections;

namespace AppAdvisory.UI
{
	/// <summary>
	/// Attached to rate button
	/// </summary>
	public class ButtonRateAB : MonoBehaviour 
	{
				/// <summary>
		/// If player clicks on the rate button, we call this method.
		/// </summary>
		public void OnClickedRate()
		{
			Application.OpenURL(FindObjectOfType<UIControllerAB>().URL_STORE);
		}
	}
}