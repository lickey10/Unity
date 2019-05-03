using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonInfoPopupHandler : MonoBehaviour
{
    public Dungeon CurrentDungeon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayDungeon(Dungeon dungeon)
    {
        CurrentDungeon = dungeon;

        //populate form
        foreach (Text txtTemp in GetComponentsInChildren<Text>())
        {
            switch(txtTemp.name)
            {
                case "Title":
                    txtTemp.text = dungeon.Name;
                    break;
                case "txtAddress":
                    txtTemp.text = dungeon.Address;
                    break;
                case "txtCity":
                    txtTemp.text = dungeon.City;
                    break;
                case "txtState":
                    txtTemp.text = dungeon.State;
                    break;
                case "txtZip":
                    txtTemp.text = dungeon.zip;
                    break;
                case "txtPhone":
                    txtTemp.text = dungeon.Phone;
                    break;
                case "txtRating":
                    txtTemp.text = dungeon.Rating.ToString();
                    break;
                case "txtFavorite":
                    txtTemp.text = dungeon.Favorite.ToString();
                    break;
            }
        }
    }

    public void ScheduleGame()
    {
        GameObject scripts = GameObject.FindGameObjectWithTag("scripts");
        Settings settings = scripts.GetComponent<Settings>();
        RPGScheduleHandler scheduleHandler = settings.RPGSchedulePopup.GetComponent<RPGScheduleHandler>();
        
        scheduleHandler.DungeonScheduleToShow = CurrentDungeon;
        settings.RPGSchedulePopup.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void DisplayScheduleList()
    {
        GameObject scripts = GameObject.FindGameObjectWithTag("scripts");
        Settings settings = scripts.GetComponent<Settings>();
        DungeonScheduleListPopupHandler scheduleHandler = settings.DungeonRPGScheduleListPopup.GetComponent<DungeonScheduleListPopupHandler>();

        scheduleHandler.DungeonToShowSchedule = CurrentDungeon;
        settings.DungeonRPGScheduleListPopup.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
