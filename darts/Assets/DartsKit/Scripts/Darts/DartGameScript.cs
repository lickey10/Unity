using UnityEngine;
using System.Collections;
using DartKit;

namespace DartKit
{
	public class DartGameScript : MonoBehaviour {
		//the maximum number of players
		private static int MAX_PLAYERS = 2;

		//the scores
		private int[] m_score = new int[MAX_PLAYERS];

		//the score texts
		public GUIText[] scoreTexts;

		//the player names
		public GUIText[] playerNames;


		//the player images
		public GUITexture[] playerImages;

		//a ref to the horizontal powerbar
		public Powerbar horzPowerbar;

		//a ref to the vertical powerbar
		public Powerbar vertPowerbar;


		//a ref to the fire button
		public TouchButton2 fireButton;

		//the time before switching rounds
		public float waitTimeBeforeChangeRounds = 1f;

		//the current player turn
		private int m_playerTurn = 0;

		//the dart index 0..6
		private int m_dartIndex=0;

		//the dart index  0..DARTS_PER_PLAYER (then switch players).
		private int m_dartIndex2=0;

		//the darts fired per player
		private static int DARTS_PER_PLAYER = 3;

		//the inital score
		public int initalScore = 301;

		//the fire index
		private int m_fireIndex=0;

		//the number of players
		private int m_nomPlayers = 0;

		//a ref to the dart UI
		public DartUI dartUI;


		public enum GameType
		{
			SCORE_ATTACK,
			AROUND_THE_WORLD
		};

		//the gametype
		private GameType m_gameType;



		public void Awake()
		{
			int gameToggle = Constants.getGameType();

			m_gameType = GameType.SCORE_ATTACK;
			initalScore = 301;
			if(gameToggle==1)
			{
				initalScore = 501;
			}

			if(gameToggle==2)
			{
				m_gameType = GameType.AROUND_THE_WORLD;
			}

		}
		public void setNomPlayers(int nomPlayers)
		{
			m_nomPlayers=nomPlayers;
		}
		public int getNomPlayers()
		{
			return m_nomPlayers;
		}
		public void onButtonPress(string str)
		{
			if(str.Equals("FireDart"))
			{
				fireDart();
			}
		}
		void OnEnable()
		{
			BaseGameManager.onDartHitBoard += onDartHitBoard;
			BaseGameManager.onButtonPress += onButtonPress;
			BaseGameManager.onSetDart 	+= onSetDart;
			BaseGameManager.onGameStart += onGameStart;
		}
		void OnDisable()
		{
			BaseGameManager.onButtonPress -= onButtonPress;

			BaseGameManager.onSetDart 	-= onSetDart;
			BaseGameManager.onDartHitBoard -= onDartHitBoard;
			BaseGameManager.onGameStart -= onGameStart;
		}
		public void onGameStart()
		{
			int gameToggle = Constants.getGameType();



			BasePlayer[] players = (BasePlayer[])GameObject.FindObjectsOfType(typeof(BasePlayer));
			m_nomPlayers = players.Length; 
			Debug.Log ("onGameStart m_nomPlayers:" + m_nomPlayers);
			if(m_nomPlayers==1)
			{
				//move it to the middle

				if(playerImages!=null && playerImages.Length>1)
				{
					if(playerImages[1]!=null)
					{
						//disabl player 2.
						playerImages[1].gameObject.SetActive(false);
					}
				}
			}

			if(playerImages[m_playerTurn]!=null)
				playerImages[m_playerTurn].color = Color.gray	;



			if(playerNames!=null && playerNames.Length==players.Length)
			{
				for(int i=0; i<players.Length; i++)
				{
					if(playerNames[i])
					{
						int index = players[i].playerIndex;
						playerNames[index].text = players[i].playerName;
					}
				}
			}

			m_dartIndex=0;

			for(int i=0; i<MAX_PLAYERS; i++)
			{
				if(m_gameType==GameType.SCORE_ATTACK)
				{
					m_score[i] = initalScore;
				}else if(m_gameType==GameType.AROUND_THE_WORLD)
				{
					m_score[i] = 1;
				}
				if(scoreTexts[i]!=null)
				{
					scoreTexts[i].text = m_score[i].ToString();
				}
			}
		}
		public void fireDart()
		{
			if(dartUI)
			{
				if(m_fireIndex==0)
				{
					dartUI.setHorizontal();
				}else{
					dartUI.setVertical();
				}
			}
			m_fireIndex^=1;
		}

		void onSetDart(float hrz, 
		                float vrt)
		{
			Debug.Log ("hrz " + hrz + " vrt" + vrt);
			BaseGameManager.fireDart(m_dartIndex,hrz,vrt,m_score[m_playerTurn]);

		}


		void onDartHitBoard(int score,DartField.FieldType fieldType)
		{

			if(m_gameType==GameType.SCORE_ATTACK)
			{
				m_score[m_playerTurn] -= score;
				Debug.Log ("m_score[m_playerTurn]" + m_score[m_playerTurn]);
				if(m_score[m_playerTurn]<0 && Application.loadedLevel!=0)
				{
					m_score[m_playerTurn]=0;
					Debug.Log ("GAMEOVER MAN");

					BaseGameManager.gameover(m_playerTurn==0);
				}
			}else{
				if(score==m_score[m_playerTurn])
				{
					m_score[m_playerTurn]++;
					if(m_score[m_playerTurn]>=20)
					{
						BaseGameManager.gameover(m_playerTurn==0);
					}
				}
			}

			if(scoreTexts!=null && m_playerTurn < scoreTexts.Length	&& scoreTexts[m_playerTurn]!=null)
			{	
				scoreTexts[m_playerTurn].text = m_score[m_playerTurn].ToString();
			}


			m_dartIndex++;
			m_dartIndex2++;
			if(m_dartIndex2>2)
			{
				m_dartIndex2=0;
			
				if(m_nomPlayers>1)
				{
					if(playerImages[m_playerTurn]!=null)
					{
						playerImages[m_playerTurn].color = Color.gray*.5f;
					}
					m_playerTurn ^=1;
					if(playerImages[m_playerTurn]!=null )
					{
						playerImages[m_playerTurn].color = Color.gray;
					}

				}


				if(m_playerTurn==0)
				{
					m_dartIndex=0;
				}
				StartCoroutine(changeTurnIE());
			}else{
				if(dartUI)
				{
					dartUI.resetBar(m_playerTurn);
				}
			}
		}

		public GameType getGameType()
		{
			return m_gameType;
		}
		IEnumerator changeTurnIE()
		{
			yield return new WaitForSeconds(waitTimeBeforeChangeRounds);
			BaseGameManager.resetDarts();
			BaseGameManager.playersTurn(m_playerTurn);
			BaseGameManager.resetFireButton();
			if(dartUI)
			{
				dartUI.resetBar(m_playerTurn);
			}

		}
	}
}
