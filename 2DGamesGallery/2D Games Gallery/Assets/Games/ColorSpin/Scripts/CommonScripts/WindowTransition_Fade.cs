using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if HBDOTween
using DG.Tweening;
#endif

[RequireComponent(typeof(CanvasGroup))]
public class WindowTransition_Fade : MonoBehaviour 
{
	[SerializeField]
	private bool animateOnEnable = true;

	[SerializeField]
	private bool animateOnDisable = true;

	[SerializeField]
	private bool destroyOnDisable = false;

	[SerializeField]
	private float enableDelay = 0.3F;

	private CanvasGroup canvasGroup;

	private void Awake() {
		canvasGroup = GetComponent<CanvasGroup>();	
	}

	private void OnEnable() {
		Activate();
	}

	public void Activate() 
	{
		gameObject.SetActive(true);
		if(animateOnEnable)
		{
			#if HBDOTween
			canvasGroup.DOFade(1F,0.3F).SetDelay(enableDelay).SetEase(Ease.Linear);
			#endif
		}	
	}

	public void Deactivate()
	{
		if(animateOnDisable)
		{
			#if HBDOTween
			canvasGroup.DOFade(0F,0.3F).SetDelay(enableDelay).SetEase(Ease.Linear).OnComplete(()=>
			{
				if(destroyOnDisable)
				{
					Destroy(gameObject);
				} else {
					gameObject.SetActive(false);		
				}
			});
			#endif
		}
	}
	
}
