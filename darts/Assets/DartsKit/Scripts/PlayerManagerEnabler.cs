using UnityEngine;
using System.Collections;
namespace DartKit
	{
	public class PlayerManagerEnabler : MonoBehaviour {
		public static PlayerManager K_PLAYER_MANAGER;
		public PlayerManager playerManager;
		// Use this for initialization
		void Start () {
			if(K_PLAYER_MANAGER==null)
			{
				playerManager.gameObject.SetActive(true);
				K_PLAYER_MANAGER = playerManager;
			}
		}
	}
}