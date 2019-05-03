using Mapbox.Unity.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class RPGScheduleListController : MonoBehaviour
{
    public GameObject RPGScheduleListContentHolder;
    public GameObject RPGScheduleButton;
    public GameObject RPGScheduleInfoPopup;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        RefreshList();
    }

    public void UpdateList()
    {

    }

    public void RefreshList()
    {
        List<RPGSchedule> rpgSchedules = new List<RPGSchedule>();

        //remove any buttons already in the list
        foreach (Button button in RPGScheduleListContentHolder.GetComponentsInChildren(typeof(Button)))
            Destroy(button.gameObject);

        //get RPGSchedules
        rpgSchedules = RPGScheduleHandler.GetScheduledRPGList();

        rpgSchedules = rpgSchedules.OrderBy(x => x.Name).ToList();

        rpgSchedules.ForEach(x => AddButton(x));
    }

    public void AddButton(RPGSchedule rpgSchedule)
    {
        GameObject goButton = (GameObject)Instantiate(RPGScheduleButton);
        goButton.transform.SetParent(RPGScheduleListContentHolder.transform, false);
        goButton.transform.localScale = new Vector3(1, 1, 1);
        
        Text[] tmpButtonTexts = goButton.GetComponentsInChildren<Text>();
        tmpButtonTexts[0].text = rpgSchedule.Day.Substring(0,3) + " " + rpgSchedule.Time;

        string tmpName = rpgSchedule.Name;

        if (tmpName.Length > 14)
            tmpName = tmpName.Substring(0, 14).Trim() +"...";

        tmpButtonTexts[1].text = tmpName;

        Button tempButton = goButton.GetComponent<Button>();

        tempButton.onClick.AddListener(() => ButtonClicked(rpgSchedule));

        void ButtonClicked(RPGSchedule rpgScheduleToOpen)
        {
            RPGScheduleInfoPopup.GetComponent<RPGScheduleInfoPopupHandler>().CurrentSchedule = rpgScheduleToOpen;
            RPGScheduleInfoPopup.GetComponent<RPGScheduleInfoPopupHandler>().DisplayRPGSchedule(rpgScheduleToOpen);
            RPGScheduleInfoPopup.SetActive(true);
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
