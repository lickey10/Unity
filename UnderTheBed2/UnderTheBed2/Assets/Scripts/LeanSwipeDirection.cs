using UnityEngine;
using UnityEngine.UI;

namespace Lean.Touch
{
    // This script will tell you which direction you swiped in
    public class LeanSwipeDirection : MonoBehaviour
	{
		[Tooltip("The text element we will display the swipe information in")]
		public Text InfoText;
        public GameObject ScrollingMenu;

        protected virtual void OnEnable()
		{
			// Hook into the events we need
			LeanTouch.OnFingerSwipe += OnFingerSwipe;
		}
	
		protected virtual void OnDisable()
		{
			// Unhook the events
			LeanTouch.OnFingerSwipe -= OnFingerSwipe;
		}
	
		public void OnFingerSwipe(LeanFinger finger)
		{
            // Store the swipe delta in a temp variable
            var swipe = finger.SwipeDelta;
            //doubleTap = false;

            if (swipe.x < -Mathf.Abs(swipe.y) || swipe.x > Mathf.Abs(swipe.y))
            {
                //swipeLeft = true;
                //swipeRight = false;
                //swipeDown = false;
                //swipeUp = false;
                //scripts.BroadcastMessage("SelectNextWeapon");

                if (finger.Age > .2)
                {
                    if(GameObject.FindGameObjectWithTag("ScrollingMenu") == null)
                        Instantiate(ScrollingMenu);//, new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, GameObject.FindGameObjectWithTag("Player").transform.position.z), GameObject.FindGameObjectWithTag("Player").transform.rotation);
                    //GameObject.FindGameObjectWithTag("ScrollingMenu").SetActive(true);
                    //GameObject.FindGameObjectWithTag("MenuCamera").GetComponent<Camera>().enabled = true;
                    //GameObject.FindGameObjectWithTag("MenuCameraMain").GetComponent<Camera>().enabled = true;
                    //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = false;


                    Time.timeScale = 0;
                }

                //print("You swiped left! or Right!");
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
}