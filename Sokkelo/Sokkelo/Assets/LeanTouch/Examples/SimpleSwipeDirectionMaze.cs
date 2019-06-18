using UnityEngine;
using UnityEngine.UI;

// This script will tell you which direction you swiped in
public class SimpleSwipeDirectionMaze : MonoBehaviour
{
	public Text InfoText;
    GameObject thePlayer;
    public float Speed = 2;
	
    public void Update()
    {
        thePlayer.transform.position += thePlayer.transform.forward * Speed * Time.deltaTime;
    }

	protected virtual void OnEnable()
	{
		// Hook into the OnSwipe event
		Lean.LeanTouch.OnFingerSwipe += OnFingerSwipe;
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        
    }
	
	protected virtual void OnDisable()
	{
		// Unhook into the OnSwipe event
		Lean.LeanTouch.OnFingerSwipe -= OnFingerSwipe;
	}
	
	public void OnFingerSwipe(Lean.LeanFinger finger)
	{
		// Make sure the info text exists
		//if (InfoText != null)
		//{
			// Store the swipe delta in a temp variable
			var swipe = finger.SwipeDelta;
			
			if (swipe.x < -Mathf.Abs(swipe.y))
			{
                thePlayer.transform.Rotate(new Vector3(0, 90, 0));
				//InfoText.text = "You swiped left!";

			}
			
			if (swipe.x > Mathf.Abs(swipe.y))
			{
                thePlayer.transform.Rotate(new Vector3(0, -90, 0));
                //InfoText.text = "You swiped right!";
            }
			
			if (swipe.y < -Mathf.Abs(swipe.x))
			{
				//InfoText.text = "You swiped down!";
			}
			
			if (swipe.y > Mathf.Abs(swipe.x))
			{
				//InfoText.text = "You swiped up!";
			}
		//}
	}
}