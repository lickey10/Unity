using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundButton_colorSpin : MonoBehaviour 
{
	// The button to turn on/off sound.
	public Button btnSound;	
	// Image of the button on which sound sprite will get assigned. Default on
	public Image btnSoundImage; 
	// Sound on sprite.
	public Sprite soundOnSprite;
	// Sounf off sprite.
	public Sprite soundOffSprite;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		btnSound.onClick.AddListener(() => 
		{
			if (InputManager_colorSpin.Instance.canInput ()) {
                AudioManager_colorSpin.Instance.PlayButtonClickSound ();
                AudioManager_colorSpin.Instance.ToggleSoundStatus();
			}
		});
	}

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		AudioManager.OnSoundStatusChangedEvent += OnSoundStatusChanged;
		initSoundStatus ();
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable()
	{
		AudioManager.OnSoundStatusChangedEvent -= OnSoundStatusChanged;
	}

	/// <summary>
	/// Inits the sound status.
	/// </summary>
	void initSoundStatus()
	{
		btnSoundImage.sprite = (AudioManager_colorSpin.Instance.isSoundEnabled) ? soundOnSprite : soundOffSprite;
	}

	/// <summary>
	/// Raises the sound status changed event.
	/// </summary>
	/// <param name="isSoundEnabled">If set to <c>true</c> is sound enabled.</param>
	void OnSoundStatusChanged (bool isSoundEnabled)
	{
		btnSoundImage.sprite = (isSoundEnabled) ? soundOnSprite : soundOffSprite;
	}	
}
