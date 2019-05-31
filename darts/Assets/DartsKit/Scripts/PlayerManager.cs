using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DartKit
{
	public class PlayerManager : MonoBehaviour 
	{
		//the maximum number of playes in the game
		public int MAX_PLAYERS  = 2;


		private bool m_loadedLevel=false;

		//the number of ai to spawn.
		private int m_aiToSpawn;
		//the number of humans to spawn.
		private int m_nomHumans;

		void Awake()
		{
			DontDestroyOnLoad( gameObject );
		}
		public void OnEnable()
		{
			BaseGameManager.onConnect 	+=connect;

		}
		public void OnDisable()
		{
			BaseGameManager.onConnect 	-=connect;
		}
		void Update()
		{
			if(Application.loadedLevel>0 && m_loadedLevel==false)
			{
				BaseGameManager.spawnPlayers(m_nomHumans,m_aiToSpawn);
				m_loadedLevel=true;
			}
		}
		//lets handle connect. Do we want to load offline mode, or connect to photon.
		void connect(bool offlineMode, 
		             int levelToLoad,
		             int nomHumans,
		             int nomAI)
		{
			m_loadedLevel=false;

			m_aiToSpawn = nomAI;
			m_nomHumans = nomHumans;
			PlayerPrefs.SetInt("nomAI",nomAI);
			Application.LoadLevel(levelToLoad);
		}


	}
}