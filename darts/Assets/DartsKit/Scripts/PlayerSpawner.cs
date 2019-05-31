using UnityEngine;
using System.Collections;
namespace DartKit
{
	public class PlayerSpawner : MonoBehaviour 
	{
		public Vector3 spawnLoc = new Vector3(0,0,-0.21f);
		void Start()
		{
		//	spawnPlayer(1);
		}
		public void OnEnable()
		{
			BaseGameManager.onSpawnPlayers += onSpawnPlayers;
		}
		
		public void OnDisable()
		{
			BaseGameManager.onSpawnPlayers -= onSpawnPlayers;
		}

		void updateNomPlayers()
		{


			BasePlayer[] players = (BasePlayer[])GameObject.FindObjectsOfType(typeof(BasePlayer));
			DartGameScript bk = (DartGameScript)GameObject.FindObjectOfType(typeof(DartGameScript));
			if(bk)
			{
				bk.setNomPlayers( players.Length);
			}
	
		}

		void spawnPlayer(int playerID)
		{
			string objectToSpawn = "HumanPlayer" + playerID;
			//lets spawn our players here.
			GameObject go = (GameObject)Instantiate(Resources.Load (objectToSpawn),
			                                        spawnLoc,
			                                          Quaternion.identity);
			updateNomPlayers();
		}

		//spawn the objects owned by the master client.
		public void onSpawnPlayers(int localHumansToSpawn,
		                           int nomAI)
		{
			{
				//the humans will as well.
				for(int i=0; i<localHumansToSpawn; i++)
				{
					spawnPlayer(i+1);
				}

				//the ai will be owned by the master client
				for(int i=0; i<nomAI; i++)
				{
					spawnAI(i);
				}

			}
			BaseGameManager.startGame();
		}

		void spawnAI (int i) 
		{
			//set our ai difficulty based on what level they got selected.
			string objectToSpawn = "AIPlayer" + PlayerPrefs.GetInt("AIDifficultyX",1);
			//lets spawn our players here.
			GameObject go = (GameObject)Instantiate(Resources.Load (objectToSpawn),
			                                          spawnLoc,
			                                          Quaternion.identity);	
			updateNomPlayers();
		}




	}
}
