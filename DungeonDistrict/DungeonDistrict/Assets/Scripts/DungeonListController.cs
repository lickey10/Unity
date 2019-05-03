using Mapbox.Unity.Map;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class DungeonListController : MonoBehaviour
{
    public GameObject DungeonListContentHolder;
    public GameObject DungeonButton;
    //public AbstractMap Map;
    public Mapbox.Examples.ForwardGeocodeUserInput searchBox;
    public MapController MapControllerObject;

    List<Dungeon> dungeons = new List<Dungeon>();

    // Start is called before the first frame update
    void Start()
    {
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
        foreach (Button button in DungeonListContentHolder.GetComponentsInChildren(typeof(Button)))
            Destroy(button.gameObject);

        MapControllerObject.ResetMap();

        //get dungeons
        dungeons = DungeonHandler.GetDungeonsList();

        dungeons = dungeons.OrderBy(x => x.Name).ToList();

        foreach (Dungeon dungeon in dungeons)
        {
            AddButton(dungeon);
        }
    }

    public void AddButton(Dungeon dungeon)
    {
        GameObject goButton = (GameObject)Instantiate(DungeonButton);
        goButton.transform.SetParent(DungeonListContentHolder.transform, false);
        goButton.transform.localScale = new Vector3(1, 1, 1);

        goButton.name = dungeon.Lat + "," + dungeon.Lon;

        Text tmpButtonText = goButton.GetComponentInChildren<Text>();
        string tmpName = dungeon.Name;

        if (tmpName.Length > 14)
            tmpName = tmpName.Substring(0, 14).Trim() +"...";

        tmpButtonText.text = tmpName;

        Button tempButton = goButton.GetComponent<Button>();

        tempButton.onClick.AddListener(() => ButtonClicked(dungeon.Lat.ToString(), dungeon.Lon.ToString()));

        void ButtonClicked(string Lat, string Lon)
        {
            //Map.SetCenterLatitudeLongitude(new Mapbox.Utils.Vector2d(double.Parse(Lat), double.Parse(Lon)));
            MapControllerObject.CenterMap(Lat, Lon);
            searchBox.HandleUserInput(Lon + "," + Lat);

            //Map.UpdateMap(new Mapbox.Utils.Vector2d(double.Parse(Lat), double.Parse(Lon)), 16);

            //Map.ResetMap();
            //Map.Initialize(new Mapbox.Utils.Vector2d(double.Parse(Lat), double.Parse(Lon)), 16);
            //Map.UpdateMap(float.Parse(Lat), float.Parse(Lon));
            //Debug.Log("Button clicked = " + btnLatLon);
        }
    }

    /// <summary>
    /// Remove local list and refresh list from server
    /// </summary>
    public void ResetList()
    {
        MapControllerObject.RefreshDungeons();
        DungeonHandler.ResetList();

        RefreshList();
    }
}
