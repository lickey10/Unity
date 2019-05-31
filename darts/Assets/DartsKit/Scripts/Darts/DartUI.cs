using UnityEngine;
using System.Collections;
using DartKit;


namespace DartKit
{
	public class DartUI : MonoBehaviour 
	{
		//horizontal powerbar
		public Powerbar horzPowerbar;

		//a ref to the vertical powerbar
		public Powerbar vertPowerbar;

		//a ref to the firebutton
		public TouchButton2 fireButton;

		//the baseplayer
		private BasePlayer m_player;

		void OnEnable()
		{
			BaseGameManager.onGameStart += onGameStart;
		}
		void OnDisable()	
		{
			BaseGameManager.onGameStart -= onGameStart;
		}
		bool isMyTurn()
		{
			bool myTurn = true;
			if(m_player)
			{
				myTurn= m_player.isMyTurn();
			}
			return myTurn;
		}

		void onGameStart () 
		{
			bool noAI = PlayerPrefs.GetInt("nomAI")==0;
			HumanPlayer[] humanPlayers = (HumanPlayer[])GameObject.FindObjectsOfType(typeof(HumanPlayer));
			for(int i=0; i<humanPlayers.Length;i++)
			{
				if(humanPlayers[i].playerIndex==0)
				{
					m_player = humanPlayers[i];
				}
			}
			if( noAI || (noAI==false && isMyTurn()))
			{
				if(horzPowerbar)
					horzPowerbar.gameObject.SetActive(true);
				if(vertPowerbar)
					vertPowerbar.gameObject.SetActive(false);
			}else
			{
				Debug.Log ("Disable");
				fireButton.GetComponent<GUITexture>().color = Color.gray;
				fireButton.enabled=false;

				if(horzPowerbar)
					horzPowerbar.gameObject.SetActive(false);
				if(vertPowerbar)
					vertPowerbar.gameObject.SetActive(false);
			}
			
		}
		public void setHorizontal()
		{
			bool noAI = PlayerPrefs.GetInt("nomAI")==0;
			
			if( noAI || (noAI==false && isMyTurn()))
			{
				if(horzPowerbar)
					horzPowerbar.press();
				
				if(vertPowerbar)
				{
					vertPowerbar.reset();
					vertPowerbar.gameObject.SetActive(true);
				}
			}
		}
		
		public void setVertical()
		{
			bool noAI = PlayerPrefs.GetInt("nomAI")==0;
			Debug.Log("noAI"+noAI);
			if( noAI || (noAI==false && isMyTurn()))
			{
				if(vertPowerbar)
					vertPowerbar.press();
				if(horzPowerbar)
				{
					horzPowerbar.gameObject.SetActive(false);
				}
				if(vertPowerbar)
				{
					vertPowerbar.gameObject.SetActive(false);
				}
				disableFireButton();
			}
			
		}

		public void enableFireButton()
		{
			bool noAI = PlayerPrefs.GetInt("nomAI")==0;

			if( noAI || (noAI==false && isMyTurn()))
			{
				if(fireButton)
				{
					fireButton.GetComponent<GUITexture>().color = Color.white	;
					fireButton.enabled=true;
				}	
			}
		}
		public void disableFireButton()
		{
			if(fireButton)
			{
				fireButton.GetComponent<GUITexture>().color = Color.gray*.5f;
				fireButton.enabled=false;
			}	
			
		}
		public void resetBar(int playerTurn)
		{
			bool noAI = PlayerPrefs.GetInt("nomAI")==0;
			
			if( noAI || (noAI==false && isMyTurn()))
			{
				enableFireButton();

			//	if(isMyTurn())
				{
					if(horzPowerbar)
					{
						horzPowerbar.gameObject.SetActive(true);
						horzPowerbar.reset();
					}
				}
			}
		}
	}
}