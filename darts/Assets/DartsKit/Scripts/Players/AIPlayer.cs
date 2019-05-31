using UnityEngine;
using System.Collections;
namespace DartKit
{
	//our human player script
	public class AIPlayer : BasePlayer 
	{
		//the variance for the AI -- between: -0.5 and 0.5
		public float variance = 0.5f;

		//the time before we fire the dart
		public float fireTime = 1;

		//the current fire time
		private float m_fireTime = 0;

		public override void onMyTurn ()
		{
			base.onMyTurn ();
			m_fireTime=fireTime;

		}

		public void Update()
		{
			if(m_myTurn && m_gameover==false)
			{
				m_fireTime-=Time.deltaTime;

				if(m_fireTime<0)
				{
					fireShot();
					m_fireTime=fireTime;
				}
			}
		}
		void fireShot()
		{
			//the middle is 0.5, we add some variance.
			float horz = 0.5f + Random.Range(-variance,variance);
			float vert = 0.5f +  Random.Range(-variance,variance);
			BaseGameManager.setDart(horz,vert);
		}
	}
}
