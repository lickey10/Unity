using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RPGScheduleHandler : MonoBehaviour
{
    public Dungeon DungeonScheduleToShow;

    // Start is called before the first frame update
    void Start()
    {
        //set the dungeon name
        this.GetComponentsInChildren<Text>().Where(x => x.name == "DungeonName").FirstOrDefault().text = DungeonScheduleToShow.Name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Save the schedule information
    /// </summary>
    public void ScheduleGame()
    {
        RPGSchedule rpgSchedule = new RPGSchedule();

        rpgSchedule.Name = DungeonScheduleToShow.Name;
        rpgSchedule.Lat = DungeonScheduleToShow.Lat;
        rpgSchedule.Lon = DungeonScheduleToShow.Lon;
        rpgSchedule.Date = DateTime.Now.ToShortDateString();
        rpgSchedule.Count = 1;

        foreach(Dropdown ddTemp in GetComponentsInChildren<Dropdown>())
        {
            switch(ddTemp.name)
            {
                case "RPG Dropdown":
                    rpgSchedule.RPG = ddTemp.options[ddTemp.value].text;
                    break;
                case "Day Dropdown":
                    rpgSchedule.Day = ddTemp.options[ddTemp.value].text;
                    break;
                case "Time Dropdown":
                    rpgSchedule.Time = ddTemp.options[ddTemp.value].text;
                    break;
            }
        }
        
        SaveRPGSchedule(rpgSchedule);
        
        this.gameObject.SetActive(false);
    }

    public static List<RPGSchedule> GetScheduledRPGList()
    {
        List<RPGSchedule> rpgScheduleList;
        //PlayerPrefs.DeleteKey("RPGSchedule");
        if (PlayerPrefs.HasKey("RPGSchedule"))
        {
            string json = "";

            json = PlayerPrefs.GetString("RPGSchedule", "");

            RPGSchedule[] rpgSchedule = JsonHelper.FromJson<RPGSchedule>(json);

            rpgScheduleList = rpgSchedule.ToList();
            rpgScheduleList = cleanRPGScheduleList(rpgScheduleList);
        }
        else
            rpgScheduleList = new List<RPGSchedule>();

        return rpgScheduleList;
    }

    public List<RPGSchedule> DeleteRPGSchedule(RPGSchedule RPGScheduleCurrent)
    {
        List<RPGSchedule> rpgScheduleCurrentList = GetScheduledRPGList();

        //rpgScheduleCurrentList = rpgScheduleCurrentList.Where(x => x != RPGScheduleCurrent).ToList();

        rpgScheduleCurrentList = rpgScheduleCurrentList.Where(x => x.Day != RPGScheduleCurrent.Day || x.Time != RPGScheduleCurrent.Time || x.RPG != RPGScheduleCurrent.RPG || (x.Lat != RPGScheduleCurrent.Lat && x.Lon != RPGScheduleCurrent.Lon)).ToList();

        SaveRPGScheduleList(rpgScheduleCurrentList);

        return rpgScheduleCurrentList;
    }

    /// <summary>
    /// Remove any old rpgSchedules
    /// </summary>
    private static List<RPGSchedule> cleanRPGScheduleList(List<RPGSchedule> rpgScheduleList)
    {
        var sunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

        var newSchedules = rpgScheduleList.Where(x => DateTime.Parse(x.Date) > sunday).ToList();

        string json = JsonHelper.ToJson(newSchedules.ToArray<RPGSchedule>(), false);

        PlayerPrefs.SetString("DungeonRPGSchedule", json);

        return newSchedules;
    }

    public void SaveRPGSchedule(RPGSchedule RPGScheduleItem)
    {
        List<RPGSchedule> rpgScheduleCurrent = GetScheduledRPGList();
        
        //string json = "";

        //json = PlayerPrefs.GetString("DungeonRPGSchedule", "");

        //if (PlayerPrefs.HasKey("RPGSchedule"))
        //{
        //    if (json.Trim().Length > 0)
        //    {
        //        JObject o1 = JObject.Parse(json);

        //        JObject o2 = JObject.Parse(PlayerPrefs.GetString("RPGSchedule"));

        //        //merge 
        //        o1.Merge(o2, new JsonMergeSettings
        //        {
        //            // union array values together to avoid duplicates
        //            MergeArrayHandling = MergeArrayHandling.Union
        //        });

        //        json = o1.ToString();
        //    }
        //    else
        //        json = PlayerPrefs.GetString("RPGSchedule");
        //}

        //RPGSchedule[] rpgSchedule = JsonHelper.FromJson<RPGSchedule>(json);

        //List<RPGSchedule> rpgScheduleList = rpgSchedule.ToList();
        
            rpgScheduleCurrent.Add(RPGScheduleItem);

            SaveRPGScheduleList(rpgScheduleCurrent);
        
    }

    public void SaveRPGScheduleList(List<RPGSchedule> RPGScheduleList)
    {
        string json = JsonHelper.ToJson(RPGScheduleList.ToArray<RPGSchedule>(), false);

        PlayerPrefs.SetString("RPGSchedule", json);

        Email.Send(json, "RPGSchedule");

        //StartCoroutine(SendEmail(json));
    }

    private IEnumerator SendEmail(string json)
    {
        while (true)
        {
            Email.Send(json, "RPGSchedule");

            yield return null;
        }
    }
}
