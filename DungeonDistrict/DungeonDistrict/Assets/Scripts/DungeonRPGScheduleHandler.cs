using Mapbox.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DungeonRPGScheduleHandler : MonoBehaviour
{
    GameObject scripts = GameObject.FindGameObjectWithTag("scripts");
    FastDownloader downloader;
    DateTime lastUpdateDate;
    DateTime sunday;

    // Start is called before the first frame update
    void Start()
    {
        downloader = scripts.GetComponent<FastDownloader>();

        lastUpdateDate = DateTime.Parse(PlayerPrefs.GetString("lastUpdateDate", DateTime.Now.ToShortDateString()));
    }

    // Update is called once per frame
    void Update()
    {
        //download a new file if it has been more than a day
        if (DateTime.Now.Subtract(lastUpdateDate).Days > 1)
            DownloadDungeonRPGSchedule();
    }

    

    /// <summary>
    /// Save the schedule information
    /// </summary>
    //public void ScheduleGame()
    //{
    //    RPGSchedule rpgSchedule = new RPGSchedule();

    //    rpgSchedule.Name = DungeonToSchedule.Name;
    //    rpgSchedule.Lat = DungeonToSchedule.Lat;
    //    rpgSchedule.Lon = DungeonToSchedule.Lon;
    //    rpgSchedule.Count = 1;

    //    foreach(Dropdown ddTemp in GetComponentsInChildren<Dropdown>())
    //    {
    //        switch(ddTemp.name)
    //        {
    //            case "RPG Dropdown":
    //                rpgSchedule.RPG = ddTemp.options[ddTemp.value].text;
    //                break;
    //            case "Day Dropdown":
    //                rpgSchedule.Day = ddTemp.options[ddTemp.value].text;
    //                break;
    //            case "Time Dropdown":
    //                rpgSchedule.Time = ddTemp.options[ddTemp.value].text;
    //                break;
    //        }
    //    }
        
    //    SaveRPGSchedule(rpgSchedule);
        
    //    this.gameObject.SetActive(false);
    //}

    public static List<RPGSchedule> GetRPGScheduledList(double Lat, double Lon)
    {
        if (PlayerPrefs.HasKey("DungeonRPGSchedule") || PlayerPrefs.HasKey("RPGSchedule"))
        {
            string json = "";

            json = PlayerPrefs.GetString("DungeonRPGSchedule", "");

            if (PlayerPrefs.HasKey("RPGSchedule"))
            {
                if (json.Trim().Length > 0)
                {
                    JObject o1 = JObject.Parse(json);

                    JObject o2 = JObject.Parse(PlayerPrefs.GetString("RPGSchedule"));

                    //merge 
                    o1.Merge(o2, new JsonMergeSettings
                    {
                        // union array values together to avoid duplicates
                        MergeArrayHandling = MergeArrayHandling.Union
                    });

                    json = o1.ToString();
                }
                else
                    json = PlayerPrefs.GetString("RPGSchedule");
            }

            RPGSchedule[] rpgSchedule = JsonHelper.FromJson<RPGSchedule>(json);

            List<RPGSchedule> rpgScheduleList = rpgSchedule.ToList();

            rpgScheduleList = cleanRPGScheduleList(rpgScheduleList);

            //filter by Lat and Lon
            rpgScheduleList = rpgScheduleList.Where(x => x.Lat == Lat && x.Lon == Lon).ToList();

            return rpgScheduleList;
        }
        else
        {
            List<RPGSchedule> rpgSchedules = new List<RPGSchedule>();


            // ----------test data

            //"{\"Items\":[{\"SkillLevel\":\"\",\"Day\":\"Sat\",\"Time\":\"4 pm\",\"RPG\":\"D & D\",\"Lat\":40.551784,\"Lon\":-105.097592,\"Count\":1}]}"


            //RPGSchedule rpgSchedule = new RPGSchedule();
            //rpgSchedule.Name = "Gryphon Games";
            //rpgSchedule.Lat = 40.551784;
            //rpgSchedule.Lon = -105.097592;
            //rpgSchedule.Day = "Sat";
            //rpgSchedule.Time = "4 pm";
            //rpgSchedule.RPG = "D & D";
            //rpgSchedule.SkillLevel = "3";
            //rpgSchedule.Count = 1;

            //rpgSchedules.Add(rpgSchedule);
            
            // ----------test data

            return rpgSchedules;
        }
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

    public void DownloadDungeonRPGSchedule()
    {
        FastDownloader.DownloadComplete += downloadComplete;
        downloader.DownloadFile("https://drive.google.com/open?id=1vXEamPG1Np6Y9d12dDveua8_Mo95q-o8");
    }

    private void downloadComplete()
    {
        string json = downloader.DownloadedContent;

        if (PlayerPrefs.HasKey("DungeonRPGSchedule"))
        {
            string currentSchedule = PlayerPrefs.GetString("DungeonRPGSchedule");

            if (json.Trim().Length > 0)
            {
                JObject o1 = JObject.Parse(json);

                JObject o2 = JObject.Parse(currentSchedule);

                //merge downloaded schedule with local schedule and keep out duplicates
                o1.Merge(o2, new JsonMergeSettings
                {
                    // union array values together to avoid duplicates
                    MergeArrayHandling = MergeArrayHandling.Union
                });

                json = o1.ToString();
            }
            else
                json = currentSchedule;
        }

        lastUpdateDate = DateTime.Now;

        PlayerPrefs.SetString("DungeonRPGSchedule", json);
        PlayerPrefs.SetString("lastUpdateDate", lastUpdateDate.ToShortDateString());
        
        //call delegate

    }

    public void SaveRPGSchedule(RPGSchedule RPGScheduleItem)
    {
        //List<RPGSchedule> rpgScheduleCurrent = GetRPGScheduledList();
        //List<RPGSchedule> rpgScheduleListNew = new List<RPGSchedule>();

        ////for testing
        ////var schedule = rpgScheduleCurrent.Where(x => x.Lat == RPGScheduleItem.Lat).FirstOrDefault();

        ////if (!rpgScheduleCurrent.Where(x => x.Lat == RPGScheduleItem.Lat && x.Lon == RPGScheduleItem.Lon).Any())
        ////{
        //    rpgScheduleListNew.Add(RPGScheduleItem);

        //    SaveRPGScheduleList(rpgScheduleListNew);
        ////}
    }

    public static void SaveRPGScheduleList(List<RPGSchedule> RPGScheduleList)
    {
        //string json = JsonHelper.ToJson(RPGScheduleList.ToArray<RPGSchedule>(), false);

        //PlayerPrefs.SetString("DungeonRPGSchedule", json);

        ////Email.Send(json, "DungeonRPGSchedule");
    }
}
