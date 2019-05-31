using UnityEngine;
using System.Collections;
namespace DartKit
{
	public class Constants : MonoBehaviour {

		private static string GOLD_ID = "XGOLD";
		
		public static string AUDIO_ID = "X_AUDIO";

		public static string GAME_TYPE_ID = "X_GAME_TYPE";

		public static string GAME_COST_ID = "X_COST";

		public static string EXP_COST_ID = "X_EXP";

		public static string PLAYER_2_ID = "X_PLAYER2";

		public static void addGold(int gold)
		{
			int currentGold = getGold();
			currentGold+=gold;
			PlayerPrefs.SetInt(GOLD_ID,currentGold);
		}

		public static int getGold()
		{
			return PlayerPrefs.GetInt(GOLD_ID,200);
		}


		public static void setPlayer2(int player2)
		{
			PlayerPrefs.SetInt(PLAYER_2_ID,player2);
		}
		
		public static int getPlayer2()
		{
			return PlayerPrefs.GetInt(PLAYER_2_ID,0);
		}



		public static void setGameType(int gametype)
		{
			PlayerPrefs.SetInt(GAME_TYPE_ID,gametype);
		}
		
		public static int getGameType()
		{
			return PlayerPrefs.GetInt(GAME_TYPE_ID,0);
		}

		public static void setGameCost(int val)
		{
			PlayerPrefs.SetInt(GAME_COST_ID,val);
		}
		
		public static int getGameCost()
		{
			return PlayerPrefs.GetInt(GAME_COST_ID,0);
		}

		public static void setExpCost(int val)
		{
			PlayerPrefs.SetInt(EXP_COST_ID,val);
		}
		
		public static int getExpCost()
		{
			return PlayerPrefs.GetInt(EXP_COST_ID,0);
		}

	}
}