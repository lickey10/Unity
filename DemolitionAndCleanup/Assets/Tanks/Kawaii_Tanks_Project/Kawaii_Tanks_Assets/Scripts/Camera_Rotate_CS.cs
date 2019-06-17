using UnityEngine;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;

public class Camera_Rotate_CS : MonoBehaviour {

	// This script must be attached to "Camera_Pivot".

	Transform thisTransform ;
	Vector2 previousMousePos ;
	float angY ;
	float angZ ;

	int myID ;
	bool isCurrentID ;
    bool multiDrag = false;
    bool swipingUp = false;
    bool swipingDown = false;
    bool swipingRight = false;
    bool swipingLeft = false;

    void Start () {
		thisTransform = transform ;
		angY = thisTransform.eulerAngles.y ;
		angZ = thisTransform.eulerAngles.z ;
	}

	void Update () {
		if ( isCurrentID ) {
#if UNITY_ANDROID || UNITY_IPHONE
            if (multiDrag)
            {
                //angY += CrossPlatformInputManager.GetAxis("horizontal") * 2.0f;
                //angZ -= CrossPlatformInputManager.GetAxis("vertical");

                // This will rotate the current transform based on a multi finger twist gesture
                //Lean.LeanTouch.RotateObject(thisTransform, Lean.LeanTouch.TwistDegrees);

                float horizontal = (Input.mousePosition.x - previousMousePos.x) * 0.1f;
                float vertical = (Input.mousePosition.y - previousMousePos.y) * 0.1f;
                previousMousePos = Input.mousePosition;
                angY += horizontal * 3.0f;
                angZ -= vertical * 2.0f;
            }
#else
			if ( Input.GetMouseButtonDown ( 1 ) ) {
				previousMousePos = Input.mousePosition ;
			}
			if ( Input.GetMouseButton ( 1 ) ) {
				float horizontal = ( Input.mousePosition.x - previousMousePos.x ) * 0.1f ;
				float vertical = ( Input.mousePosition.y - previousMousePos.y ) * 0.1f ;
				previousMousePos = Input.mousePosition ;
				angY += horizontal * 3.0f ;
				angZ -= vertical * 2.0f ;
			}

            
#endif
            thisTransform.rotation = Quaternion.Euler(0.0f, angY, angZ);
        }
    }

	void Get_ID ( int idNum ) {
		myID = idNum ;
	}
	
	void Get_Current_ID ( int idNum ) {
		if ( idNum == myID ) {
			isCurrentID = true ;
		} else {
			isCurrentID = false ;
		}
	}

    public void OnMultiDrag(Vector2 pixels)
    {
        multiDrag = true;
        Debug.Log("Many fingers moved " + pixels + " across the screen");
    }

    public void OnFingerUp(Lean.LeanFinger finger)
    {
        multiDrag = false;
        Debug.Log("Finger " + finger.Index + " finished touching the screen");
    }

    protected virtual void OnEnable()
    {
        // Hook into the OnSwipe event
        Lean.LeanTouch.OnFingerSwipe += OnFingerSwipe;
        Lean.LeanTouch.OnFingerUp += OnFingerUp;
        Lean.LeanTouch.OnMultiDrag += OnMultiDrag;
    }

    protected virtual void OnDisable()
    {
        // Unhook into the OnSwipe event
        Lean.LeanTouch.OnFingerSwipe -= OnFingerSwipe;
        Lean.LeanTouch.OnFingerUp -= OnFingerUp;
        Lean.LeanTouch.OnMultiDrag -= OnMultiDrag;
    }

    public void OnFingerSwipe(Lean.LeanFinger finger)
    {
        // Store the swipe delta in a temp variable
        var swipe = finger.SwipeDelta;

        if (swipe.x < -Mathf.Abs(swipe.y))
        {
            swipingUp = false;
            swipingDown = false;
            swipingRight = false;
            swipingLeft = true;
            //InfoText.text = "You swiped left!";
        }
        else
        {
            swipingLeft = false;
        }

        if (swipe.x > Mathf.Abs(swipe.y))
        {
            swipingUp = false;
            swipingDown = false;
            swipingRight = true;
            swipingLeft = false;
            // InfoText.text = "You swiped right!";
        }
        else
        {
            swipingRight = false;
        }

        if (swipe.y < -Mathf.Abs(swipe.x))
        {
            swipingUp = false;
            swipingDown = true;
            swipingRight = false;
            swipingLeft = false;
            //InfoText.text = "You swiped down!";
        }
        else
        {
            swipingDown = false;
        }

        if (swipe.y > Mathf.Abs(swipe.x))
        {
            swipingUp = true;
            swipingDown = false;
            swipingRight = false;
            swipingLeft = false;
            //InfoText.text = "You swiped up!";
        }
        else
        {
            swipingUp = false;
        }
    }
}
