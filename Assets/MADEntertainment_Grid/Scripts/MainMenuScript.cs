using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour 
{
	SaveDataScript SDS;
	ThemeChangeScript CGS;

	GameObject DataManager;
	public Image SoundImage;

	[SerializeField]
	GameObject Buttons;
	[SerializeField]
	GameObject GameModes;

	public Text LogoText;
	public Image PlayButton;
	public Image QuitButton;
	public Image SoundButton;

	public Image GameMode1ButtonImage;
	public Image GameMode2ButtonImage;
	public Image GameMode3ButtonImage;

	public Text GameMode1ButtonText;
	public Text GameMode2ButtonText;
	public Text GameMode3ButtonText;

	public Text GameMode1Text;
	public Text GameMode2Text;
	public Text GameMode3Text;

	public Image BackButtonImage;
	public Text BackButtonText;

	void Start () 
	{
		DataManager = GameObject.FindGameObjectWithTag ("DM");
		SDS = DataManager.GetComponent<SaveDataScript> ();
		CGS = DataManager.GetComponent<ThemeChangeScript> ();

		CGS.ChangeTheme ();
		LogoText.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
		PlayButton.color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);
		QuitButton.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
		SoundButton.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);

		GameMode1ButtonImage.color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);
		GameMode2ButtonImage.color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);
		GameMode3ButtonImage.color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);

		GameMode1ButtonText.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
		GameMode2ButtonText.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
		GameMode3ButtonText.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);

		GameMode1Text.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
		GameMode2Text.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);
		GameMode3Text.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);

		BackButtonImage.color = new Color32 (CGS.R1, CGS.G1, CGS.B1,255);
		BackButtonText.color = new Color32 (CGS.R2, CGS.G2, CGS.B2,255);

		Camera.main.backgroundColor = new Color32 (CGS.R3, CGS.G3, CGS.B3,255);

		if(AudioListener.volume == 1)
			SoundImage.color = new Color32 (0,255,0,255);
		else
			SoundImage.color = new Color32 (255,0,0,255);
	}

	public void Mute()
	{
		if(AudioListener.volume == 1)
		{
			AudioListener.volume= 0;
			SoundImage.color = new Color32 (255,0,0,255);
		}
		else
		{
			AudioListener.volume= 1;
			SoundImage.color = new Color32 (0,255,0,255);
		}
	}

	public void Play()
	{
		Buttons.SetActive (false);
		GameModes.SetActive (true);
	}

	public void Back()
	{
		Buttons.SetActive (true);
		GameModes.SetActive (false);
	}

	public void Quit()
	{
		Application.Quit ();
	}

	public void SelectGameMode(int GameMode)
	{
		SDS.GameMode = GameMode;
		SceneManager.LoadScene (2);
	}
}
