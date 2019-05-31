using UnityEngine;
using System.Collections;
namespace DartKit
{
	//the base play for our bowling characters -- wether it be human or AI
	public class BasePlayer : MonoBehaviour 
	{
		//is it my turn.
		protected bool m_myTurn = false;

		//is it gameover
		protected bool m_gameover=false;

		//the name of the player
		public string playerName = "Player 1";

		//the player index of the player
		public int playerIndex = 0;

		//the horizontal
		private float horz = 0;
		//the vertical
		private float vert = 0;

		public void Awake()
		{
			m_myTurn = playerIndex==0;
		}



		public virtual void Start()
		{
			m_myTurn = playerIndex==0;
			onPlayerTurn(0);
		}


		public void onPowerbarPress(Powerbar powerbar)
		{
			if(m_myTurn)
			{
				Powerbar.PowerbarType ptype = powerbar.getPowerbarType ();
				if(ptype==Powerbar.PowerbarType.HORZ)
				{
					horz = powerbar.getValueAsScalar();
				}
				if(ptype==Powerbar.PowerbarType.VERT)
				{
					vert = powerbar.getValueAsScalar();
					BaseGameManager.setDart(horz,vert);
				}
			}
		}


		public void onGameStart()
		{
			m_myTurn = playerIndex==0;
		}


		public virtual void OnEnable()
		{
			BaseGameManager.onGameOver 		+= onGameOver;
			BaseGameManager.onPlayerTurn 	+= onPlayerTurn;
			BaseGameManager.onResetPlayer 	+= onResetPlayer;
			BaseGameManager.onGameStart 	+= onGameStart;
			BaseGameManager.onPowerbarPress += onPowerbarPress;

		}
		public virtual void OnDisable()
		{
			BaseGameManager.onGameOver 		-= onGameOver;
			BaseGameManager.onPlayerTurn 	-= onPlayerTurn;
			BaseGameManager.onResetPlayer 	-= onResetPlayer;
			BaseGameManager.onGameStart 	-= onGameStart;
			BaseGameManager.onPowerbarPress -= onPowerbarPress;

		}
	
		public virtual void notMyTurn()
		{


		}
		public virtual void onMyTurn()
		{
			
		}
		public virtual void onPlayerTurn(int pi)
		{
			myTurnRPC(pi);
		}
		public void myTurnRPC(int pi)
		{
			if(pi==playerIndex)
			{

				onMyTurn();
				m_myTurn = true;
			}else{
				notMyTurn();
				m_myTurn = false;
			}

		}
		void onGameOver(bool vic)
		{
			m_gameover=true;
		}
		public void onResetPlayer(int pi)
		{
			if(playerIndex==pi)
				onMyTurn();

			reset ();
		}

		public virtual void reset()
		{
		}


		public bool isMyTurn()
		{
			return m_myTurn;
		}

	}		

}
