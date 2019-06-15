using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicButton_colorSpin : MonoBehaviour 
{
	// The button to toggle music, assigned from inspector.
	public Button btnMusic;
	// The image of the button.
	public Image btnMusicImage;
	// The On sprite for music.
	public Sprite musicOnSprite;
	// The off sprite for music.
	public Sprite musicOffSprite;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		btnMusic.onClick.AddListener(() => {
			if (InputManager_colorSpin.Instance.canInput ()) {
                AudioManager_colorSpin.Instance.PlayButtonClickSound ();
                AudioManager_colorSpin.Instance.ToggleMusicStatus	();
			}
		});
	}

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		AudioManager.OnMusicStatusChangedEvent += OnMusicStatusChanged;
		initMusicStatus ();
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable()
	{
		AudioManager.OnMusicStatusChangedEvent -= OnMusicStatusChanged;
	}

	/// <summary>
	/// Inits the music status.
	/// </summary>
	void initMusicStatus()
	{
		btnMusicImage.sprite = (AudioManager_colorSpin.Instance.isMusicEnabled) ? musicOnSprite : musicOffSprite;
	}

	/// <summary>
	/// Raises the music status changed event.
	/// </summary>
	/// <param name="isMusicEnabled">If set to <c>true</c> is music enabled.</param>
	void OnMusicStatusChanged (bool isMusicEnabled)
	{
		btnMusicImage.sprite = (isMusicEnabled) ? musicOnSprite : musicOffSprite;
	}	
}
