using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonRPGScheduleListButtonScript : MonoBehaviour
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
        int rpgScheduleCount = DungeonRPGScheduleHandler.GetRPGScheduledList(double.Parse(gameObject.transform.parent.name.Split(',')[0]), double.Parse(gameObject.transform.parent.name.Split(',')[1].ToString())).Count;

        GetComponentInChildren<Text>().text = rpgScheduleCount.ToString();
    }
}
