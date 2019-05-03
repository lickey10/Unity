using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DungeonClickHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        GameObject scripts = GameObject.FindGameObjectWithTag("scripts");
        GameObject DungeonInfoPopup = scripts.GetComponent<Settings>().DungeonInfoPopup;

        double lat = double.Parse(this.gameObject.name.Split(',')[0]);
        double lon = double.Parse(this.gameObject.name.Split(',')[1]);

        DungeonInfoPopup.SetActive(true);
        DungeonInfoPopup.GetComponent<DungeonInfoPopupHandler>().DisplayDungeon(DungeonHandler.GetDungeon(lat, lon));
    }
}
