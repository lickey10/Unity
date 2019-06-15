using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if HBDOTween
using DG.Tweening;
#endif

public class GamePlay : Singleton_colorSpin<GamePlay> 
{
	public List<Color> randomColors;
	public SpinningArrow spinningArrow;
	public GameObject btnPause;

	[SerializeField] private GameContent gameBoard1;
	[SerializeField] private GameContent gameBoard2;
	[SerializeField] private GameContent gameBoard3;
	[SerializeField] private RectTransform rectBgCircle;
	[SerializeField] private Image imgProgressBar;
	[SerializeField] private Text txtLevel;
	[SerializeField] private Text txtHelp;

	[HideInInspector] public GameContent currentGameBoard;
	[HideInInspector] public Transform currentTargetObject = null;
	[HideInInspector] public Transform currentTouchingObject = null;
	
	[System.NonSerialized]
	[HideInInspector] public bool isGamePlay = false;

	int targetScore = 0;
	int currentScore = 0;

	int currentLevel = 1;

	private void OnEnable() {
		randomColors.Shuffle();
		currentLevel = PlayerPrefs.GetInt("currentLevel",1);
		LoadLevel();
	}

	//Loads level.
	void LoadLevel(bool isRescued = false)
	{
		txtHelp.gameObject.SetActive( (currentLevel == 1) ? true : false);

		btnPause.SetActive(true);
		if(!isRescued)
		{
			randomColors.Shuffle();
			currentScore = 0;
			#if HBDOTween
			imgProgressBar.DOFillAmount(0F,0.4F);
			#endif

			transform.GetComponent<Image>().color = randomColors[6];
		}

		SelectBoard();
		currentGameBoard.gameObject.SetActive(true);
		txtLevel.text = "Level " + currentLevel.ToString();
		
		int index = 0;
		foreach(Transform t in currentGameBoard.playingContent.transform) {
			t.GetComponent<Image>().color = randomColors[index];
			index++;
		}
		
		currentTouchingObject = currentGameBoard.playingContent.transform.GetChild(0);

		targetScore = currentLevel;
		if(targetScore == 1) {
			targetScore = 2;
		}
		#if HBDOTween
		currentGameBoard.playingContent.GetComponent<RectTransform>().DOScale(Vector3.one,0.3F).SetDelay(0.1F);
		#endif
		
		spinningArrow.SetSpinSpeed(currentLevel);
		SetTarget();
		
		spinningArrow.isSpinning = false;
		isGamePlay = true;
	}

	//Board selection based on user progress. you can add as many as you want.
	void SelectBoard()
	{
		if(currentLevel < 5) {
			currentGameBoard = gameBoard1;
		}
		else if(currentLevel < 10) {
			currentGameBoard = gameBoard2;
		}
		else {
			currentGameBoard = gameBoard3;
		}
	}

	//Select target color on start or after every successful attempt.
	void SetTarget() 	
	{
		if(currentTouchingObject != null)
		{
			List<Transform> possibleTarget = new List<Transform>();
			foreach(Transform t in currentGameBoard.playingContent.transform) {
				if(t != currentTouchingObject) {
					possibleTarget.Add(t);
				}
			}
			int randomTarget = UnityEngine.Random.Range(0,possibleTarget.Count);
			currentTargetObject = possibleTarget[randomTarget];

			foreach(Transform t in spinningArrow.transform) {
				t.GetComponent<Image>().color = currentTargetObject.GetComponent<Image>().color;
			}
		}
	}

	//Increase score after successful attempt.
	public void OnSpinSuccess() {
		currentScore += 1;
		AnimateGameUI();
        AudioManager_colorSpin.Instance.PlayScoreIncreaseSound();

		if(currentScore >= targetScore) {
			Invoke("OnLevelComplete",0.1F);
		}
		else {
			SetTarget();
		}
	}

	//Level complete.
	public void OnLevelComplete() {
		isGamePlay = false;
		spinningArrow.StopSpinningOnComplete();
		StartCoroutine(PlayLevelCompleteAnimation());
		currentLevel++;
		PlayerPrefs.SetInt("currentLevel",currentLevel);
		
		if(currentLevel >= PlayerPrefs.GetInt("lastUnlockedLevel", currentLevel))
		{
			PlayerPrefs.SetInt("lastUnlockedLevel",currentLevel);
		}
		LoadLevel();
	}

	//Level Fail.
	public void OnLevelFail() 
	{
		isGamePlay = false;
		spinningArrow.StopSpinningOnFail();
        AudioManager_colorSpin.Instance.PlayGameOverSound();
        UIController_colorSpin.Instance.LoadLevelFail();
	}

	//Animation on every successful attempt.
	void AnimateGameUI() {
		#if HBDOTween
		rectBgCircle.DOSizeDelta(new Vector2(550,550),0.2F).OnComplete(()=>{
			rectBgCircle.DOSizeDelta(new Vector2(450,450),0.2F);
		});
		float fillAmount = (((float) currentScore) / ((float)targetScore));
		imgProgressBar.DOFillAmount(fillAmount,0.4F);
		#endif
	}

	//Animation on the time of level completion.
	IEnumerator PlayLevelCompleteAnimation() 
	{	
		if(currentGameBoard != null)
		{
			currentGameBoard.playingContent.SetActive(false);
			currentGameBoard.playingContent.transform.localScale = new Vector3(0,0,1);
			currentGameBoard.animatingContent.SetActive(true);

			int index = 0;
			foreach(Transform t in currentGameBoard.animatingContent.transform) {
				t.GetComponent<Image>().color = randomColors[index];
				#if HBDOTween
				t.GetComponent<RectTransform>().DOPivot(new Vector2(3F,0F),0.9F);
				t.GetComponent<Image>().DOFade(0,0.9F);
				index++;
				#endif
			}
		}
        AudioManager_colorSpin.Instance.PlayLevelUpSound();
		yield return new WaitForSeconds(1.0F);

		foreach(Transform t in currentGameBoard.animatingContent.transform) {
			t.GetComponent<RectTransform>().pivot = new Vector2(0.5F,1F);
		}
		currentGameBoard.animatingContent.SetActive(false);
	}

	public void Reset() {
		foreach(Transform t in currentGameBoard.animatingContent.transform) {
			t.GetComponent<RectTransform>().pivot = new Vector2(0.5F,1F);
		}
		currentGameBoard.animatingContent.SetActive(false);
		spinningArrow.StopSpinningOnComplete();
	}

	private void OnDisable() {
		Reset();
		currentGameBoard.gameObject.SetActive(false);
	}

	public void ReplayLevel(bool isRescued = false) {
		Reset();
		LoadLevel(isRescued);
	}


	public void OnPauseButtonPressed()
	{
        AudioManager_colorSpin.Instance.PlayButtonClickSound();
        UIController_colorSpin.Instance.LoadPauseScreen();
	}
}
