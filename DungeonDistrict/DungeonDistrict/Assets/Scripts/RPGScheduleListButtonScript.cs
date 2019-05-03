using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPGScheduleListButtonScript : MonoBehaviour
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
        //get users rpg schedule and display count
        int rpgScheduleCount = RPGScheduleHandler.GetScheduledRPGList().Count;

        GetComponentInChildren<Text>().text = rpgScheduleCount.ToString();
    }
}
