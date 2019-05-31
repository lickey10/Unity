using UnityEngine;
using System.Collections;
using DartKit;

namespace DartKit
{
	public class AudioHelper : MonoBehaviour {
		//audio clip to play if we hit a target
		public AudioClip onDartHitTarget;

		//audio clip to play if we press the powerbar
		public AudioClip onPowerbarPressAC;

		//audio clip to play if we hit the cabient.
		public AudioClip onHitCabinent;


		public void OnEnable()
		{
			BaseGameManager.onDartHitBoard += onDartHitBoard;
			BaseGameManager.onPowerbarPress += onPowerbarPress;

		}
		public void OnDisable()
		{
			BaseGameManager.onDartHitBoard -= onDartHitBoard;
			BaseGameManager.onPowerbarPress -= onPowerbarPress;

		}

		void onPowerbarPress(Powerbar pow)
		{
			if(GetComponent<AudioSource>())
				GetComponent<AudioSource>().PlayOneShot(onPowerbarPressAC);

		}

		void onDartHitBoard(int score,DartField.FieldType fieldType)
		{

			if(fieldType!=DartField.FieldType.UNKNOWN)
			{
				GetComponent<AudioSource>().PlayOneShot(onDartHitTarget);
			}else{
				GetComponent<AudioSource>().PlayOneShot(onHitCabinent);

			}
		}
	}
}