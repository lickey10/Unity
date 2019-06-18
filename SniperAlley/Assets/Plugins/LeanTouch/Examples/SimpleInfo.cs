using UnityEngine;

// This script will spam the console with finger info
public class SimpleInfo : MonoBehaviour
{
	protected virtual void OnEnable()
	{
		// Hook events
		LeanTouch.LeanTouch.OnFingerDown     += OnFingerDown;
		LeanTouch.LeanTouch.OnFingerSet      += OnFingerSet;
		LeanTouch.LeanTouch.OnFingerUp       += OnFingerUp;
		LeanTouch.LeanTouch.OnFingerDrag     += OnFingerDrag;
		LeanTouch.LeanTouch.OnFingerTap      += OnFingerTap;
		LeanTouch.LeanTouch.OnFingerSwipe    += OnFingerSwipe;
		LeanTouch.LeanTouch.OnFingerHeldDown += OnFingerHeldDown;
		LeanTouch.LeanTouch.OnFingerHeldSet  += OnFingerHeld;
		LeanTouch.LeanTouch.OnFingerHeldUp   += OnFingerHeldUp;
		LeanTouch.LeanTouch.OnMultiTap       += OnMultiTap;
		LeanTouch.LeanTouch.OnDrag           += OnDrag;
		LeanTouch.LeanTouch.OnSoloDrag       += OnSoloDrag;
		LeanTouch.LeanTouch.OnMultiDrag      += OnMultiDrag;
		LeanTouch.LeanTouch.OnPinch          += OnPinch;
		LeanTouch.LeanTouch.OnTwistDegrees   += OnTwistDegrees;
		LeanTouch.LeanTouch.OnTwistRadians   += OnTwistRadians;
	}
	
	protected virtual void OnDisable()
	{
		// Unhook events
		LeanTouch.LeanTouch.OnFingerDown     -= OnFingerDown;
		LeanTouch.LeanTouch.OnFingerSet      -= OnFingerSet;
		LeanTouch.LeanTouch.OnFingerUp       -= OnFingerUp;
		LeanTouch.LeanTouch.OnFingerDrag     -= OnFingerDrag;
		LeanTouch.LeanTouch.OnFingerTap      -= OnFingerTap;
		LeanTouch.LeanTouch.OnFingerSwipe    -= OnFingerSwipe;
		LeanTouch.LeanTouch.OnFingerHeldDown -= OnFingerHeldDown;
		LeanTouch.LeanTouch.OnFingerHeldSet  -= OnFingerHeld;
		LeanTouch.LeanTouch.OnFingerHeldUp   -= OnFingerHeldUp;
		LeanTouch.LeanTouch.OnMultiTap       -= OnMultiTap;
		LeanTouch.LeanTouch.OnDrag           -= OnDrag;
		LeanTouch.LeanTouch.OnSoloDrag       -= OnSoloDrag;
		LeanTouch.LeanTouch.OnMultiDrag      -= OnMultiDrag;
		LeanTouch.LeanTouch.OnPinch          -= OnPinch;
		LeanTouch.LeanTouch.OnTwistDegrees   -= OnTwistDegrees;
		LeanTouch.LeanTouch.OnTwistRadians   -= OnTwistRadians;
	}
	
	public void OnFingerDown(LeanTouch.LeanFinger finger)
	{
		Debug.Log("Finger " + finger.Index + " began touching the screen");
	}
	
	public void OnFingerSet(LeanTouch.LeanFinger finger)
	{
		Debug.Log("Finger " + finger.Index + " is still touching the screen");
	}
	
	public void OnFingerUp(LeanTouch.LeanFinger finger)
	{
		Debug.Log("Finger " + finger.Index + " finished touching the screen");
	}
	
	public void OnFingerDrag(LeanTouch.LeanFinger finger)
	{
		Debug.Log("Finger " + finger.Index + " moved " + finger.DeltaScreenPosition + " pixels across the screen");
	}
	
	public void OnFingerTap(LeanTouch.LeanFinger finger)
	{
		Debug.Log("Finger " + finger.Index + " tapped the screen");
	}
	
	public void OnFingerSwipe(LeanTouch.LeanFinger finger)
	{
		Debug.Log("Finger " + finger.Index + " swiped the screen");
	}
	
	public void OnFingerHeldDown(LeanTouch.LeanFinger finger)
	{
		Debug.Log("Finger " + finger.Index + " began touching the screen for a long time");
	}
	
	public void OnFingerHeld(LeanTouch.LeanFinger finger)
	{
		Debug.Log("Finger " + finger.Index + " is still touching the screen for a long time");
	}
	
	public void OnFingerHeldUp(LeanTouch.LeanFinger finger)
	{
		Debug.Log("Finger " + finger.Index + " stopped touching the screen for a long time");
	}
	
	public void OnMultiTap(int fingerCount)
	{
		Debug.Log("The screen was just tapped by " + fingerCount + " finger(s)");
	}
	
	public void OnDrag(Vector2 pixels)
	{
		Debug.Log("One or many fingers moved " + pixels + " across the screen");
	}
	
	public void OnSoloDrag(Vector2 pixels)
	{
		Debug.Log("One finger moved " + pixels + " across the screen");
	}
	
	public void OnMultiDrag(Vector2 pixels)
	{
		Debug.Log("Many fingers moved " + pixels + " across the screen");
	}
	
	public void OnPinch(float scale)
	{
		Debug.Log("Many fingers pinched " + scale + " percent");
	}
	
	public void OnTwistDegrees(float angle)
	{
		Debug.Log("Many fingers twisted " + angle + " degrees");
	}
	
	public void OnTwistRadians(float angle)
	{
		Debug.Log("Many fingers twisted " + angle + " radians");
	}
}