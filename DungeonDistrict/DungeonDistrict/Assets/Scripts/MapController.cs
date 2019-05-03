using Mapbox.Examples;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapController : MonoBehaviour, IPointerClickHandler
{
    public Transform CurrentLocation;
    public Transform DestinationLocation;
    public GameObject DirectionsObject;
    public SpawnOnMap SpawnOnMapController;
    [SerializeField]
    public AbstractMap _map;
    public GameObject EditDungeonPopUp;
    public GameObject AddDungeonPopUp;
    LocationService locationService = new LocationService();
    private Plane _yPlane;
    bool one_click = false;
    bool timer_running;
    float timer_for_double_click = 0;
    ForwardGeocodeUserInput forwardGeocodeUserInput = new ForwardGeocodeUserInput();
    private AbstractLocationProvider _locationProvider = null;
    CameraMovement cameraMovement;

    //this is how long in seconds to allow for a double click
    float delay = .5f;
    //XmlDocument xDungeons;
    string LatLonUpdating = "";
    List<Dungeon> lDungeons = new List<Dungeon>();
    private bool getLocation = false;
    Location currLoc;

    //get dungeon locations
    //spawn locations on map that are within a certain radius of current physical location


    // Start is called before the first frame update
    void Start()
    {
        if (null == _locationProvider)
        {
            _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;

            getLocation = true;
        }

        _yPlane = new Plane(Vector3.up, Vector3.zero);

        cameraMovement = Camera.main.GetComponent<CameraMovement>();


        //UpdateMap(40.551668f, -105.097807f);//gryphon games

        

        //get current location
        //initialize map
        //StartCoroutine(locationService.GetLocation(result => {
        //    // callBack is going to be null until it’s set
        //    if (result != null)
        //    {
        //        if (result == -1)
        //        {
        //            //failed
        //            //updateMap(9.4166f, 82.5208f);//Bocas Del Toro
        //            //UpdateMap(40.551668f, -105.097807f);//gryphon games

        //            // Now callBack acts as an int
        //            print(result);
        //        }

        //        if (result == 1)
        //        {
        //            //success
        //            UpdateMap(locationService.Latitude, locationService.Longitude);
        //        }
        //    }
        //}));
    }

    // Update is called once per frame
    void Update()
    {
        if (getLocation)
        {
            currLoc = _locationProvider.CurrentLocation;
            string _statusText = "";

            if (currLoc.IsLocationServiceInitializing)
            {
                _statusText = "location services are initializing";
            }
            else
            {
                if (!currLoc.IsLocationServiceEnabled)
                {
                    _statusText = "location services not enabled";

                    getLocation = false;

                    UpdateMap(40.551668f, -105.097807f);//gryphon games
                }
                else
                {
                    if (currLoc.LatitudeLongitude.Equals(Vector2d.zero))
                    {
                        _statusText = "Waiting for location ....";
                    }
                    else
                    {
                        _statusText = string.Format("{0}", currLoc.LatitudeLongitude);
                        
                        getLocation = false;

                        UpdateMap((float)currLoc.LatitudeLongitude.x, (float)currLoc.LatitudeLongitude.y);//current location
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!one_click) // first click no previous clicks
            {
                one_click = true;

                timer_for_double_click = Time.time; // save the current time
                                                    // do one click things;
            }
            else //double click
            {
                one_click = false; // found a double click, now reset

                //if (MenuBackground && !MenuBackground.activeSelf)//make sure there is not menu open
                //{

                //Code to be place in a MonoBehaviour with a GraphicRaycaster component
                //GraphicRaycaster gr = this.GetComponent<GraphicRaycaster>();
                ////Create the PointerEventData with null for the EventSystem
                //PointerEventData ped = new PointerEventData(null);
                ////Set required parameters, in this case, mouse position
                //ped.position = Input.mousePosition;
                ////Create list to receive all results
                //List<RaycastResult> results = new List<RaycastResult>();
                ////Raycast it
                //gr.Raycast(ped, results);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "dungeon")//they clicked a dungeon - open dungeon info
                    {
                        //MenuBackground.SetActive(true);
                        EditDungeonPopUp.SetActive(true);
                    }
                }
                //else if (results.Count > 0)//see if we clicked on a graphics object
                //{

                //}
                else //they double clicked the map
                {
                    //spawnDungeon(Input.mousePosition);
                }
                //}
            }
        }
        if (one_click)
        {
            // if the time now is delay seconds more than when the first click started. 
            if ((Time.time - timer_for_double_click) > delay)
            {
                //basically if thats true its been too long and we want to reset so the next click is simply a single click and not a double click.
                one_click = false;
            }
        }
    }

    public void UpdateMap(float lat, float lon)
    {

        _map.Initialize(new Mapbox.Utils.Vector2d((double)lat, (double)lon), 14);

        //xDungeons = DungeonHandler.GetDungeons();
        lDungeons = DungeonHandler.GetDungeonsList();

        spawnDungeons();
    }

    public void CancelDungeonAdd()
    {
        DungeonHandler.DeleteDungeon(LatLonUpdating.Split(',')[0], LatLonUpdating.Split(',')[1]);

        LatLonUpdating = "";

        SpawnOnMapController.RefreshDungeons(DungeonHandler.GetDungeonsList());

        AddDungeonPopUp.SetActive(false);
        //MenuBackground.SetActive(false);
    }

    public void CancelDungeonEdit()
    {
        EditDungeonPopUp.SetActive(false);
        //MenuBackground.SetActive(false);
    }

    public void AddDungeon(Dungeon newDungeon)
    {
        newDungeon.Lat = double.Parse(LatLonUpdating.Split(',')[0]);
        newDungeon.Lon = double.Parse(LatLonUpdating.Split(',')[1]);

        DungeonHandler.AddDungeon(newDungeon);

        LatLonUpdating = "";

        SpawnOnMapController.RefreshDungeons(DungeonHandler.GetDungeonsList());

        AddDungeonPopUp.SetActive(false);
        //MenuBackground.SetActive(false);
    }

    public void RefreshDungeons()
    {
        SpawnOnMapController.RefreshDungeons(DungeonHandler.GetDungeonsList());
    }

    public void CenterMap(string Lat, string Lon)
    {
        _map.SetCenterLatitudeLongitude(new Mapbox.Utils.Vector2d(double.Parse(Lat), double.Parse(Lon)));
    }

    public void ResetMap()
    {
        SpawnOnMapController.RefreshDungeons(DungeonHandler.GetDungeonsList());

        Mapbox.Utils.Vector2d centerLatLon = _map.CenterLatitudeLongitude;

        _map.ResetMap();
        _map.Initialize(centerLatLon, 14);
    }

    private void spawnDungeons()
    {
        if (SpawnOnMapController)
        {
            foreach (Dungeon dungeon in lDungeons)
            {
                if (dungeon.Lat == -1)
                    populateLatLon(dungeon);
                else
                    SpawnOnMapController.AddDungeon(dungeon);
            }
        }

        SpawnOnMapController.RefreshLocations();
    }

    private void populateLatLon(Dungeon dungeon)
    {
        forwardGeocodeUserInput.HandleUserInput(dungeon.Lon + "," + dungeon.Lat);

        
    }

    private void spawnDungeon(Vector3 location)
    {
        Ray ray = Camera.main.ScreenPointToRay(location);
        float enter = 0.0f;
        if (_yPlane.Raycast(ray, out enter))
        {
            Vector2d newLocation = _map.WorldToGeoPosition(ray.GetPoint(enter));
            SpawnOnMapController.AddDungeon(newLocation);

            SpawnOnMapController.RefreshLocations();

            LatLonUpdating = newLocation.x + "," + newLocation.y;

            AddDungeonPopUp.SetActive(true);
            //MenuBackground.SetActive(true);
        }
    }

    public void DisplayDirections()
    {
        if (!DirectionsObject.activeSelf)
        {
            if (!currLoc.LatitudeLongitude.Equals(Vector2d.zero))
            {
                DirectionsObject.SetActive(true);

                CurrentLocation.position = _map.GeoToWorldPosition(currLoc.LatitudeLongitude, true);
                DestinationLocation.position = _map.GeoToWorldPosition(_map.CenterLatitudeLongitude, true);
            }
            else
            {

                DirectionsObject.SetActive(true);

                CurrentLocation.position = _map.GeoToWorldPosition(new Vector2d(40.551668f, -105.097807f), true);
                DestinationLocation.position = _map.GeoToWorldPosition(_map.CenterLatitudeLongitude, true);
            }

            DirectionsObject.GetComponent<DirectionsFactory>().Query();
        }
        else//turn off directions
        {
            DirectionsObject.GetComponent<DirectionsFactory>().StopAllCoroutines();
            DirectionsObject.GetComponent<DirectionsFactory>().DestroyDirections();

            DirectionsObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
