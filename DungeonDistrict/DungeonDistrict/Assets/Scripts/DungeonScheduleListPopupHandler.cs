using Mapbox.Unity.Map;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class DungeonScheduleListPopupHandler : MonoBehaviour
{
    public GameObject RPGScheduleListContentHolder;
    public GameObject RPGScheduleButtonPrefab;
    public Dungeon DungeonToShowSchedule;

    List<RPGSchedule> rpgSchedules = new List<RPGSchedule>();

    // Start is called before the first frame update
    void Start()
    {
        //for testing

        DungeonToShowSchedule = new Dungeon();
        DungeonToShowSchedule.Name = "Gryphon Games";
        DungeonToShowSchedule.Lat = 40.551784;
        DungeonToShowSchedule.Lon = -105.097592;

        //for testing


        //set the dungeon name
        this.GetComponentsInChildren<Text>().Where(x => x.name == "DungeonName").FirstOrDefault().text = DungeonToShowSchedule.Name;
        
        RefreshList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateList()
    {

    }

    public void RefreshList()
    {
        //remove any buttons already in the list
        foreach (Button button in RPGScheduleListContentHolder.GetComponentsInChildren(typeof(Button)))
            Destroy(button.gameObject);

        //get RPGSchedules
        rpgSchedules = DungeonRPGScheduleHandler.GetRPGScheduledList(DungeonToShowSchedule.Lat, DungeonToShowSchedule.Lon);

        if (rpgSchedules != null)
        {
            rpgSchedules = rpgSchedules.OrderBy(x => x.Name).ToList();

            rpgSchedules.ForEach(x => AddButton(x));
        }
    }

    public void AddButton(RPGSchedule rpgSchedule)
    {
        GameObject goButton = (GameObject)Instantiate(RPGScheduleButtonPrefab);
        goButton.transform.SetParent(RPGScheduleListContentHolder.transform, false);
        goButton.transform.localScale = new Vector3(1, 1, 1);

        Text[] tmpButtonTexts = goButton.GetComponentsInChildren<Text>();
        tmpButtonTexts[0].text = rpgSchedule.Day.Substring(0, 3) + " " + rpgSchedule.Time;

        string tmpName = rpgSchedule.Name;

        if (tmpName.Length > 14)
            tmpName = tmpName.Substring(0, 14).Trim() + "...";

        tmpButtonTexts[1].text = tmpName;

        Button tempButton = goButton.GetComponent<Button>();

        tempButton.onClick.AddListener(() => ButtonClicked(rpgSchedule.Lat.ToString(), rpgSchedule.Lon.ToString()));

        void ButtonClicked(string Lat, string Lon)
        {
            
        }
    }

    /// <summary>
    /// Remove local list and refresh list from server
    /// </summary>
    public void ResetList()
    {
        //DungeonHandler.ResetList();

        RefreshList();
    }
}
