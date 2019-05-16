using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class UILogic : MonoBehaviour
{
	public Material mat;
    [SerializeField] private GameLogic _gameLogic;

    [Header("Main Panel")]
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private Text _mainRecordText;
  
    [Header("Game Panel")]
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private Text _gameRecordText;
    [SerializeField] private Text _gameCurrentText;

    [Header("Pause panel")]
    [SerializeField]
    private GameObject _pausePanel;

    [Header("Game Over panel")]
    [SerializeField]
    private GameObject _gameOverPanel;


    private int _currentPoints;
    private int _recordsPoints;

	private GameObject cam;
	private int numberOfTouch = 0;
	private float a = 1;

    private void Start()
    {
		if (PlayerPrefs.GetInt ("SoundBoolean") == 0) {
			numberOfTouch = 0;
		}
		if (PlayerPrefs.GetInt ("SoundBoolean") == 1) {
			numberOfTouch = 1;
		}
		if (PlayerPrefs.GetInt ("empty") == 1) {
            GameState2.GetInstance().currentPoints = 0;
			PlayerPrefs.SetInt("empty",0);
		}
		cam = GameObject.Find("Main Camera");
        _recordsPoints = GameState2.GetInstance().recordPoints;
        _currentPoints = GameState2.GetInstance().currentPoints;
        AddPoints(0);
    }
	
    public void PlayGame()
    {
        _mainPanel.SetActive(false);
        _gamePanel.SetActive(true);
        _gameLogic.PlayGame();
		if(PlayerPrefs.GetInt("SoundBoolean") == 0){
			gameObject.GetComponent<AudioSource>().Play ();
		}
    }


	public void Inversion()
    {
		if (cam.GetComponent<Camera> ().backgroundColor == new Color (1, 1, 1, 0)) {
			cam.GetComponent<Camera> ().backgroundColor = new Color (0.14f,0.14f,0.14f, 0);
			if(PlayerPrefs.GetInt("SoundBoolean") == 0){
				gameObject.GetComponent<AudioSource>().Play ();
			}
		}
		else{
			cam.GetComponent<Camera> ().backgroundColor = new Color (1, 1, 1, 0);
			if(PlayerPrefs.GetInt("SoundBoolean") == 0){
				gameObject.GetComponent<AudioSource>().Play ();
			}
		}
    }

 

    
    public void MoreAPPS()
    {
		Application.OpenURL("https://assetstore.unity.com/publishers/19053");
		if(PlayerPrefs.GetInt("SoundBoolean") == 0){
			gameObject.GetComponent<AudioSource>().Play ();
		}
    }

    public void AddPoints(int points)
    {
        _currentPoints += points;
        if (_currentPoints > _recordsPoints)
            _recordsPoints = _currentPoints;

        _gameCurrentText.text = _currentPoints.ToString();
        _gameRecordText.text = _recordsPoints.ToString();
        _mainRecordText.text = _recordsPoints.ToString();

        GameState2.GetInstance().recordPoints = _recordsPoints;
        GameState2.GetInstance().currentPoints = _currentPoints;
    }

    public void GameOver()
    {
        _gamePanel.SetActive(false);
        _gameOverPanel.SetActive(true);
    }

    public void OpenPause()
    {
        _pausePanel.SetActive(true);
		if(PlayerPrefs.GetInt("SoundBoolean") == 0){
			gameObject.GetComponent<AudioSource>().Play ();
		}
        
    }
	public void Home()
	{
		Application.LoadLevel(Application.loadedLevel);
		if(PlayerPrefs.GetInt("SoundBoolean") == 0){
			gameObject.GetComponent<AudioSource>().Play ();
		}
	}
	public void HomeGameOver()
	{
		PlayerPrefs.SetInt("empty",1);
        GameState2.GetInstance().currentPoints = 0;
		Application.LoadLevel(Application.loadedLevel);
		if(PlayerPrefs.GetInt("SoundBoolean") == 0){
			gameObject.GetComponent<AudioSource>().Play ();
		}
	}
	public void Continue()
	{
		_pausePanel.SetActive(false);
		if(PlayerPrefs.GetInt("SoundBoolean") == 0){
			gameObject.GetComponent<AudioSource>().Play ();
		}
	}
	public void Sound(){
		if(a <= 0){
			if(numberOfTouch == 0){
				a = 1;
				numberOfTouch = 1;
				PlayerPrefs.SetInt("SoundBoolean", 1);
				PlayerPrefs.Save();
			}
		}
		if(a <= 0){
			if(numberOfTouch == 1){
				a = 1;
				numberOfTouch = 0;
				PlayerPrefs.SetInt("SoundBoolean", 0);
				PlayerPrefs.Save();
				if(PlayerPrefs.GetInt("SoundBoolean") == 0){
					gameObject.GetComponent<AudioSource>().Play ();
				}
			}
		}
	}
	void Update(){
		if(a >= 0){
			a -= 0.1f;
		}
		if (PlayerPrefs.GetInt ("SoundBoolean") == 0) {
			mat.color = new Color(1,1,1,1);
		}
		if (PlayerPrefs.GetInt ("SoundBoolean") == 1) {
			mat.color = new Color(0.85f,0.85f,0.85f,1);
		}
	}
}

