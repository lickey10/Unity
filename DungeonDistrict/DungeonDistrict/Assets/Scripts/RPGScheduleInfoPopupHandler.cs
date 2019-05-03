using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RPGScheduleInfoPopupHandler : MonoBehaviour
{
    public RPGSchedule CurrentSchedule;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayRPGSchedule(RPGSchedule rpgSchedule)
    {
        CurrentSchedule = rpgSchedule;

        //populate form
        foreach (Text txtTemp in GetComponentsInChildren<Text>())
        {
            switch(txtTemp.name)
            {
                case "DungeonName":
                    txtTemp.text = rpgSchedule.Name;
                    break;
                case "txtRPG":
                    txtTemp.text = rpgSchedule.RPG;
                    break;
                case "txtDay":
                    txtTemp.text = rpgSchedule.Day;
                    break;
                case "txtTime":
                    txtTemp.text = rpgSchedule.Time;
                    break;
            }
        }
    }

    public void DeleteRPGSchedule()
    {
        GameObject scripts = GameObject.FindGameObjectWithTag("scripts");
        Settings settings = scripts.GetComponent<Settings>();
        RPGScheduleHandler scheduleHandler = settings.RPGSchedulePopup.GetComponent<RPGScheduleHandler>();
        scheduleHandler.DeleteRPGSchedule(CurrentSchedule);
        List<RPGSchedule> rpgSchedule = new List<RPGSchedule>();
        rpgSchedule.Add(CurrentSchedule);
        string json = JsonHelper.ToJson(rpgSchedule.ToArray<RPGSchedule>(), false);
        Email.Send(json, "RPGSchedule_Delete");
        this.gameObject.SetActive(false);
    }

    public void ScheduleGame()
    {
        //GameObject scripts = GameObject.FindGameObjectWithTag("scripts");
        //Settings settings = scripts.GetComponent<Settings>();
        //RPGScheduleHandler scheduleHandler = settings.RPGSchedulePopup.GetComponent<RPGScheduleHandler>();
        
        //scheduleHandler.DungeonScheduleToShow = CurrentSchedule;
        //settings.RPGSchedulePopup.SetActive(true);
        //this.gameObject.SetActive(false);
    }

    public void DisplayScheduleList()
    {
        //GameObject scripts = GameObject.FindGameObjectWithTag("scripts");
        //Settings settings = scripts.GetComponent<Settings>();
        //DungeonScheduleListPopupHandler scheduleHandler = settings.DungeonRPGScheduleListPopup.GetComponent<DungeonScheduleListPopupHandler>();

        //scheduleHandler.DungeonToShowSchedule = CurrentSchedule;
        //settings.DungeonRPGScheduleListPopup.SetActive(true);
        //this.gameObject.SetActive(false);
    }
}
