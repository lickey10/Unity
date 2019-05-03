using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonRPGScheduleButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UpdateCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCount()
    {
        //get dungeon rpg schedule and display count
        DungeonInfoPopupHandler dungeonInfoPopupHandler = GetComponentInParent<DungeonInfoPopupHandler>();
        int rpgScheduleCount = DungeonRPGScheduleHandler.GetRPGScheduledList(dungeonInfoPopupHandler.CurrentDungeon.Lat, dungeonInfoPopupHandler.CurrentDungeon.Lon).Count;

        GetComponentInChildren<Text>().text = rpgScheduleCount.ToString();
    }
}
