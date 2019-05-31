using UnityEngine;
using System.Collections;

namespace DartKit
{
	public class BaseGameManager
	{
		//an event that listens for when its your turn
		public delegate bool OnIsMyTurn(int playerID);
		public static event OnIsMyTurn onIsMyTurn;
		public static bool isMyTurn(int playerID)
		{
			bool rc = false;
			if(onIsMyTurn!=null)
			{
				rc=onIsMyTurn(playerID);	
			}
			return rc;
		}
		
		//called when we want to reset the darts.
		public delegate void OnResetDarts();
		public static event OnResetDarts onResetDarts;
		public static void resetDarts()
		{
			if(onResetDarts!=null)
			{
				onResetDarts();	
			}	
		}
		
		
		//called when the power bar is pressed.
		public delegate void OnButtonPress(string buttonState);
		public static event OnButtonPress onButtonPress;
		public static void buttonPress(string stateID)
		{
			if(onButtonPress!=null)
			{
				onButtonPress(stateID);	
			}
		}
		

		//called when the power bar is pressed.
		public delegate void OnToggleButton(string buttonState);
		public static event OnToggleButton onToggleButton;
		public static void toggleButton(string stateID)
		{
			if(onToggleButton!=null)
			{
				onToggleButton(stateID);	
			}
		}
		
		
		//called when the power bar is pressed.
		public delegate void OnPowerbarPress(Powerbar powerBar);
		public static event OnPowerbarPress onPowerbarPress;
		public static void powerbarPress(Powerbar powerBar)
		{
			if(onPowerbarPress!=null)
			{
				onPowerbarPress(powerBar);	
			}
		}
		
		
		//called when the ball stops.
		public delegate void OnDartHitBoard(int score, DartField.FieldType fieldType);
		public static event OnDartHitBoard onDartHitBoard;
		public static void dartHitBoard(int score,DartField.FieldType fieldType)
		{
			if(onDartHitBoard!=null)
			{
				onDartHitBoard(score,fieldType);	
			}
		}
		
		
		
		//called when the ball stops.
		public delegate void OnBallStop();
		public static event OnBallStop onBallStop;
		public static void ballStopped()
		{
			if(onBallStop!=null)
			{
				onBallStop();	
			}
		}
		//we want to spawn the scene.
		public delegate void OnResetFireButton();
		public static event OnResetFireButton onResetFireButton;
		public static void resetFireButton()
		{
			if(onResetFireButton!=null)
			{
				onResetFireButton();
			}
		}
		//we want to spawn the scene.
		public delegate void OnSpawnPlayers(int nomHumans, int nomAI);
		public static event OnSpawnPlayers onSpawnPlayers;
		public static void spawnPlayers(int nomHumans, int nomAI)
		{
			if(onSpawnPlayers!=null)
			{
				onSpawnPlayers(nomHumans, nomAI);
			}
		}
		//when we want to connect to the network using photon.
		public delegate void OnDisconnect();
		public static event OnDisconnect onDisconnect;
		public static void disconnect()
		{
			if(onDisconnect!=null)
			{
				onDisconnect();
			}
		}
		
		//when we want to connect to the network using photon.
		public delegate void OnConnect(bool offlineMode, int levelToLoad, int nomPlayers,int nomAI);
		public static event OnConnect onConnect;
		public static void connect(bool offlineMode, int lvl, int nomPlayers,int nomAI)
		{
			if(onConnect!=null)
			{
				onConnect(offlineMode,lvl, nomPlayers,nomAI);
			}
		}
		
		//called on a foul shot
		public delegate void OnFoul(string foul);
		public static event OnFoul onFoul;
		public static void foul(string foul)
		{
			if(onFoul!=null)
			{
				onFoul(foul);
			}
		}
		
		
		//called when the ball hits a ball
		public delegate void OnBallHitBall(Vector3 vel);
		public static event OnBallHitBall onBallHitBall;
		public static void ballHitBall(Vector3 vel)
		{
			if(onBallHitBall!=null)
			{
				onBallHitBall(vel);
			}
		}
		
		
		//called when the ball hits the wall
		public delegate void OnBallHitWall(Vector3 vel);
		public static event OnBallHitWall onBallHitWall;
		public static void ballHitWall(Vector3 vel)
		{
			if(onBallHitWall!=null)
			{
				onBallHitWall(vel);
			}
		}
		

		
		
		//called when a ball becomes active
		public delegate void OnSetActiveBall(Transform t);
		public static event OnSetActiveBall onSetActiveBall;
		public static void setActiveBall(Transform t)
		{
			if(onSetActiveBall!=null)
			{
				onSetActiveBall(t);	
			}
		}
		
		//called when the player rests
		public delegate void OnResetPlayer(int playerIndex);
		public static event OnResetPlayer onResetPlayer;
		public static void resetPlayer(int playerIndex)
		{
			if(onResetPlayer!=null)
			{
				onResetPlayer(playerIndex);	
			}
		}
		
		//called when the turn changes.
		public delegate void OnPlayerTurn(int playerIndex);
		public static event OnPlayerTurn onPlayerTurn;
		public static void playersTurn(int playerIndex)
		{
			if(onPlayerTurn!=null)
			{
				onPlayerTurn(playerIndex);	
			}
		}
		//called when the ball is fired.
		public delegate void OnSetDart(float hrz, float vrt);
		public static event OnSetDart onSetDart;
		public static void setDart(float hrz, float vrt)
		{
			if(onSetDart!=null)
			{
				onSetDart(hrz,vrt);	
			}
		}
		
		//called when the ball is fired.
		public delegate void OnFireDart(int dartIndex,float hrz, float vrt,int fieldIndex);
		public static event OnFireDart onFireDart;
		public static void fireDart(int dartIndex,float hrz, float vrt,int fieldIndex)
		{
			if(onFireDart!=null)
			{
				onFireDart(dartIndex,hrz,vrt,fieldIndex);	
			}
		}
		
		//called when the game starts
		public delegate void OnGameStart();
		public static event OnGameStart onGameStart;
		public static void startGame()
		{
			if(onGameStart!=null)
			{
				onGameStart();	
			}
		}
		
		//called when the game starts
		public delegate void OnShowTitleCard(string title);
		public static event OnShowTitleCard onShowTitleCard;
		public static void showTitleCard(string title)
		{
			if(onShowTitleCard!=null)
			{
				onShowTitleCard(title);	
			}
		}
		
		
		//called when the game is paused
		public delegate void OnGamePause(bool pause);
		public static event OnGamePause onGamePause;
		public static void pauseGame(bool pause)
		{
			if(onGamePause!=null)
			{
				onGamePause(pause);	
			}
		}
		
		//called when the game is over
		public delegate void OnGameOver(bool vic);
		public static event OnGameOver onGameOver;
		public static void gameover(bool vic)
		{
			if(onGameOver!=null)
			{
				onGameOver(vic);	
			}
		}
		
		
		//called when the auddio is toggled
		public delegate void OnToggleAudio();
		public static event OnToggleAudio onToggleAudio;
		public static void toggleAudio()
		{
			if(onToggleAudio!=null)
			{
				onToggleAudio();	
			}
		}
	}
}
