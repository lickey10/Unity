using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if HBDOTween
using DG.Tweening;
#endif

public class SpinningArrow : MonoBehaviour 
{
	public bool isSpinning = false;
	bool rotateClockWise = false;

	Transform spinningObject = null;

	[SerializeField] private float spinSpeed = 200F;

	private void Awake() {
		spinningObject = transform;	
	}

	private void Start() 
	{
		rotateClockWise = (UnityEngine.Random.Range(0,2) == 0) ? true : false;
	}

	//Spin speed veried based on the level and user progress.
	public void SetSpinSpeed(int currentLevel) {
		if (currentLevel < 5) {
			spinSpeed = 200;
		} else if(currentLevel  < 10) {
			spinSpeed = 225;
		} else if(currentLevel  < 15) {
			spinSpeed = 260;
		} else if(currentLevel  < 20) {
			spinSpeed = 270;
		} else {
			spinSpeed = 250 + currentLevel;
		}
		spinSpeed = Mathf.Clamp(spinSpeed,0,300);
	}

	// Start spinning.
	void Spin()
	{
		isSpinning = true;
		#if HBDOTween
		spinningObject.DOLocalRotate(new Vector3(0,0,(rotateClockWise) ?-360 : 360),spinSpeed,RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetSpeedBased();
		#endif
		rotateClockWise = !rotateClockWise;
	}

	//Respin and reverse direction after successful attempt.
	void ReSpin() {
		if(GamePlay.Instance.currentTargetObject == GamePlay.Instance.currentTouchingObject)
		{
			GamePlay.Instance.OnSpinSuccess();
			#if HBDOTween
			spinningObject.DOLocalRotate(new Vector3(0,0,(rotateClockWise) ?-360 : 360),spinSpeed,RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetSpeedBased();
			#endif
			rotateClockWise = !rotateClockWise;
		}
		else
		{
			GamePlay.Instance.OnLevelFail();
		}
	}

	//Stop spin on failure.
	public void StopSpinningOnFail()
	{
		#if HBDOTween
		spinningObject.DOKill();
		#endif

	}

	//Stop spinning on level completion.
	public void StopSpinningOnComplete()
	{
		#if HBDOTween
		spinningObject.DOKill();
		#endif
		spinningObject.transform.localEulerAngles = Vector3.zero;
	}

	// On User input detectetion.
	private void OnTriggerEnter2D(Collider2D other) 
	{
		GamePlay.Instance.currentTouchingObject = other.transform;
	}

	//Calls when target color missed. Each target colors image contains collider which helps detecting successful attempt or failure.
	private void OnTriggerExit2D(Collider2D other) 
	{
		if(other.transform == GamePlay.Instance.currentTargetObject) // && GamePlay.Instance.isGamePlay)
		{
			GamePlay.Instance.OnLevelFail();
		}
	}

	public void OnSpinButtonPressed()
	{
		if(GamePlay.Instance.btnPause.activeSelf)
		{
			GamePlay.Instance.btnPause.SetActive(false);
		}
		
		if(!isSpinning ) {
			Spin();
		} else {
			ReSpin();
		}
	}
}
