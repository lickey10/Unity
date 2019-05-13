using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEditor;

public class AutoLevelGenerator : MonoBehaviour
{
    //private GameObject[] instantiatedObjects;

    public GameObject BackgroundObject;
    public GameObject PlayerObject;//the player object
    public GameObject StartingPlatform;//the first platform to be put out and the one the player is put on to start
    public GameObject GoalObject;//the goal to reach for this level
    public int LevelToLoad = 1;
    public List<CustomLevel> Levels = new List<CustomLevel>();
    public PlatformObjects Platforms;
    public DecorationObjects Decorations;//trees, rocks, etc
    public BoxObjects Boxes;
    public PickupObjects Pickups;
    public DeathTrapObjects DeathTraps;
    
    List<GameObject> PlatformsInstantiated = new List<GameObject>();
    List<GameObject> DecorationsInstantiated = new List<GameObject>();
    List<GameObject> BoxesInstantiated = new List<GameObject>();
    List<GameObject> PickupsInstantiated = new List<GameObject>();
    List<GameObject> DeathTrapsInstantiated = new List<GameObject>();
    GameObject PlayerObjectInstantiated;
    GameObject GoalObjectInstantiated;

    Vector2 backgroundSize;


    //Vector2 size = GetComponent<SpriteRenderer>().bounds.size;
    //Rect objectArea = new Rect(transform.position.x, transform.position.y, size.x, size.y);
    //
    
    public bool DoesIntersect(Rect rectA, Rect rectB)
    {
        return (Mathf.Abs(rectA.x - rectB.x) < (Mathf.Abs(rectA.width + rectB.width) / 2))
            && (Mathf.Abs(rectA.y - rectB.y) < (Mathf.Abs(rectA.height + rectB.height) / 2));

    }

    public void ResetObjects()
    {
        GameObject.FindGameObjectsWithTag("Platform").ToList().ForEach(x => DestroyImmediate(x));
        PlatformsInstantiated.Clear();

        GameObject.FindGameObjectsWithTag("Decoration").ToList().ForEach(x => DestroyImmediate(x));
        DecorationsInstantiated.Clear();

        GameObject.FindGameObjectsWithTag("Box").ToList().ForEach(x => DestroyImmediate(x));
        BoxesInstantiated.Clear();

        GameObject.FindGameObjectsWithTag("Pickup").ToList().ForEach(x => DestroyImmediate(x));
        PickupsInstantiated.Clear();

        GameObject.FindGameObjectsWithTag("DeathTrap").ToList().ForEach(x => DestroyImmediate(x));
        DeathTrapsInstantiated.Clear();

        DestroyImmediate(GoalObjectInstantiated, true);
        DestroyImmediate(PlayerObjectInstantiated, true);
    }

    public void Generate()
    {
        ResetObjects();

        backgroundSize = BackgroundObject.GetComponent<SpriteRenderer>().bounds.size;

        //generate platforms
        for (int x = 0; x < Platforms.Density; x++)
        {
            GameObject currentPlatform;
            Vector3 platformLocation;

            //choose platform
            if (PlatformsInstantiated.Count == 0)
                currentPlatform = StartingPlatform;
            else
                currentPlatform = Platforms.Platforms[Random.Range(0, Platforms.Platforms.Length - 1)];

            //choose a location
            platformLocation = getNewPlatformLocation(currentPlatform, PlatformsInstantiated);

            //make sure we are still over the background
            if (platformLocation.y > BackgroundObject.GetComponent<SpriteRenderer>().bounds.max.y - GoalObject.GetComponent<SpriteRenderer>().bounds.size.y)
            {
                break;
            }

            currentPlatform.tag = "Platform";

            PlatformsInstantiated.Add(GameObject.Instantiate(currentPlatform, platformLocation, Quaternion.identity));
        }

        int howDenseIsThisLevel = 0;

        //iterate decorations
        foreach (DecorationItem decoration in Decorations.Decorations)
        {
            howDenseIsThisLevel = Random.Range(decoration.DensityMin, decoration.DensityMax);

            //generate decorations - trees and rocks
            for (int x = 0; x < howDenseIsThisLevel; x++)
            {
                GameObject platform = PlatformsInstantiated[Random.Range(0, PlatformsInstantiated.Count - 1)];

                //add decorations to selected platform
                Vector3 decorationLocation = pickADecorationLocation(decoration.Decoration, platform);

                decoration.Decoration.tag = "Decoration";

                DecorationsInstantiated.Add(GameObject.Instantiate(decoration.Decoration, decorationLocation, Quaternion.identity, platform.transform));
            }
        }

        //generate boxes
        foreach (BoxItem box in Boxes.BoxItems)
        {
            howDenseIsThisLevel = Random.Range(box.DensityMin, box.DensityMax);

            for (int x = 0; x < howDenseIsThisLevel; x++)
            {
                GameObject platform = PlatformsInstantiated[Random.Range(0, PlatformsInstantiated.Count - 1)];

                //add boxes to selected platform
                Vector3 boxLocation = pickADecorationLocation(box.BoxObject, platform);

                boxLocation.z = -1;
                box.BoxObject.tag = "Box";

                BoxesInstantiated.Add(GameObject.Instantiate(box.BoxObject, boxLocation, Quaternion.identity, platform.transform));
            }
        }

        //generate DeathTraps
        foreach (DeathTrapItem deathTrap in DeathTraps.DeathTrapItems)
        {
            howDenseIsThisLevel = Random.Range(DeathTraps.DensityMin, DeathTraps.DensityMax);

            for (int x = 0; x < howDenseIsThisLevel; x++)
            {
                GameObject platform = PickupsInstantiated[Random.Range(0, PickupsInstantiated.Count - 1)];
                
                //add deathtrap to scene
                Vector3 deathTrapLocation = pickADecorationLocation(deathTrap.DeathTrapObject, platform);

                deathTrap.DeathTrapObject.tag = "DeathTrap";

                DeathTrapsInstantiated.Add(GameObject.Instantiate(deathTrap.DeathTrapObject, deathTrapLocation, Quaternion.identity));
            }
        }

        //place goal
        GoalObject.tag = "Goal";
        Vector3 goalLocation = pickADecorationLocation(GoalObject, PlatformsInstantiated[PlatformsInstantiated.Count-1]);
        GoalObjectInstantiated = GameObject.Instantiate(GoalObject, goalLocation, Quaternion.identity);

        //place player
        PlayerObject.tag = "Player";
        Vector3 playerLocation = pickADecorationLocation(PlayerObject, PlatformsInstantiated[0]);
        PlayerObjectInstantiated = GameObject.Instantiate(PlayerObject, playerLocation, Quaternion.identity);

        //generate pickups - coins, etc
        if (Pickups.PickupItems != null)
        {
            foreach (PickupItem pickup in Pickups.PickupItems)
            {
                howDenseIsThisLevel = Random.Range(pickup.DensityMin, pickup.DensityMax);

                for (int x = 0; x < howDenseIsThisLevel; x++)
                {
                    //GameObject platform = PickupsInstantiated[Random.Range(0, PickupsInstantiated.Count - 1)];

                    var allObjects = PlatformsInstantiated.Concat(BoxesInstantiated)
                                            .Concat(DeathTrapsInstantiated)
                                            .Concat(PickupsInstantiated)
                                            .ToList();

                    allObjects.Add(GoalObjectInstantiated);
                    allObjects.Add(PlayerObjectInstantiated);

                    //add pickups to scene
                    Vector3 pickupLocation = getNewPickupLocation(pickup, allObjects);

                    pickup.PickupObject.tag = "Pickup";

                    PickupsInstantiated.Add(GameObject.Instantiate(pickup.PickupObject, pickupLocation, Quaternion.identity));
                }
            }
        }
    }

    private Vector3 getNewPlatformLocation(GameObject currentPlatform, List<GameObject> objectsToCheck)
    {
        Vector2 platformLocation = pickNewPlatformLocation(currentPlatform, objectsToCheck);
        Bounds bounds = new Bounds();

        Vector2 platformSize = currentPlatform.GetComponent<SpriteRenderer>().bounds.size;
        currentPlatform.GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(x => bounds.Encapsulate(x.bounds));
        Vector2 platformSize2 = bounds.size / 2;

        Rect platformRect = new Rect(platformLocation.x, platformLocation.y, platformSize.x, platformSize.y);
        int retries = 0;

        //iterate objectsToCheck and compare locations
        foreach (GameObject instantiatedObject in objectsToCheck)
        {
            //Vector2 objectSize = currentPlatform.GetComponent<SpriteRenderer>().bounds.size;
            Rect objectRect = new Rect(instantiatedObject.transform.position.x, instantiatedObject.transform.position.y, bounds.size.x, bounds.size.y);

            while (DoesIntersect(platformRect, objectRect) && retries < 5)
            {
                platformLocation = pickNewPlatformLocation(currentPlatform, objectsToCheck);
                platformRect = new Rect(platformLocation.x, platformLocation.y, platformSize.x, platformSize.y);
                retries++;
            }
        }

        return new Vector3(platformLocation.x, platformLocation.y, 0);
    }

    private Vector2 pickNewPlatformLocation(GameObject currentPlatform, List<GameObject> objectsToCheck)
    {
        Vector2 platformLocation;
        Vector2 platformSize;
        Vector2 previousPlatformPosition;
        Bounds bounds = new Bounds();

        backgroundSize = BackgroundObject.GetComponent<SpriteRenderer>().bounds.size;
        platformSize = currentPlatform.GetComponent<SpriteRenderer>().bounds.size;
        //currentPlatform.GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(x => bounds.Encapsulate(x.bounds));
        //platformSize = bounds.size / 2;

        if (objectsToCheck.Count == 0)//this is the first platform
        {
            platformLocation.y = BackgroundObject.GetComponent<SpriteRenderer>().bounds.min.y + (platformSize.y / 2);
        }
        else
        {
            previousPlatformPosition = objectsToCheck[objectsToCheck.Count - 1].transform.position;
            platformLocation.y = Random.Range(previousPlatformPosition.y + Platforms.MinYProximity, previousPlatformPosition.y + Platforms.MaxYProximity);
        }

        //get X
        float minX = BackgroundObject.GetComponent<SpriteRenderer>().bounds.min.x + (platformSize.x / 2);
        float maxX = BackgroundObject.GetComponent<SpriteRenderer>().bounds.max.x - (platformSize.x / 2);
        
        platformLocation.x = Random.Range(minX, maxX);

        return platformLocation;
    }

    private Vector2 pickADecorationLocation(GameObject decoration, GameObject platform)
    {
        Vector2 decorationLocation;
        Vector2 decorationSize = decoration.GetComponentInChildren<SpriteRenderer>().bounds.size;
        float minX = platform.GetComponent<SpriteRenderer>().bounds.min.x + (decorationSize.x / 2);
        float maxX = platform.GetComponent<SpriteRenderer>().bounds.max.x - (decorationSize.x / 2);

        decorationLocation.y = platform.transform.position.y + platform.GetComponent<SpriteRenderer>().bounds.size.y / 2 + (decorationSize.y / 2);
        decorationLocation.x = Random.Range(minX, maxX);

        return decorationLocation;
    }

    private Vector3 getNewPickupLocation(PickupItem currentPickupItem, List<GameObject> objectsToCheck)
    {
        Vector2 platformLocation = new Vector2();// = pickNewPickupLocation(currentPickupItem, objectsToCheck);
        Bounds bounds = new Bounds();

        Vector2 pickupItemSize;// = currentPickupItem.PickupObject.GetComponent<SpriteRenderer>().bounds.size;
        //pickupItemSize.x += currentPickupItem.Buffer.x;
        //pickupItemSize.y += currentPickupItem.Buffer.y;

        Bounds pickupItemBounds = currentPickupItem.PickupObject.GetComponent<SpriteRenderer>().bounds;
        pickupItemSize.x = pickupItemBounds.max.x + currentPickupItem.Buffer.x;
        pickupItemSize.y = pickupItemBounds.max.y + currentPickupItem.Buffer.y;



        currentPickupItem.PickupObject.GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(x => bounds.Encapsulate(x.bounds));
        Vector2 platformSize2 = bounds.size / 2;
        Rect backgroudRect = new Rect(BackgroundObject.transform.position.x, BackgroundObject.transform.position.y, backgroundSize.x, backgroundSize.y);
        Rect pickupRect = new Rect();// = new Rect(platformLocation.x, platformLocation.y, pickupItemSize.x, pickupItemSize.y);
        int retries = 0;

        //iterate objectsToCheck and compare locations
        foreach (GameObject instantiatedObject in objectsToCheck)
        {
            //Vector2 objectSize = currentPlatform.GetComponent<SpriteRenderer>().bounds.size;
            Rect objectRect = new Rect(instantiatedObject.transform.position.x, instantiatedObject.transform.position.y, bounds.max.x, bounds.max.y);

            while (((pickupRect.x == 0 && pickupRect.y == 0) || !DoesIntersect(backgroudRect, objectRect) || DoesIntersect(pickupRect, objectRect)) && retries < 20)
            {
                platformLocation = pickNewPickupLocation(currentPickupItem, pickupItemSize, objectsToCheck);
                pickupRect = new Rect(platformLocation.x, platformLocation.y, pickupItemSize.x, pickupItemSize.y);
                retries++;
            }

            if(!DoesIntersect(backgroudRect, objectRect))
            {
                string inter = "";
            }

            if(DoesIntersect(pickupRect, objectRect))
            {
                string jhhghg = "";
            }



            GUIStyle _staticRectStyle = new GUIStyle();

            Texture2D _staticRectTexture = new Texture2D(1, 1);
            _staticRectTexture.SetPixel(0, 0, Color.red);
            _staticRectTexture.Apply();

            _staticRectStyle.normal.background = _staticRectTexture;

            GUI.Box(objectRect, GUIContent.none, _staticRectStyle);
        }

        return new Vector3(platformLocation.x, platformLocation.y, 0);
    }

    private Vector2 pickNewPickupLocation(PickupItem currentPickupItemItem, Vector2 pickupItemSize, List<GameObject> objectsToCheck)
    {
        Vector2 platformLocation;
        //Vector2 platformSize;
        //Bounds bounds = new Bounds();
        Bounds boundsBackground = BackgroundObject.GetComponent<SpriteRenderer>().bounds;

        //backgroundSize = boundsBackground.size;
        //platformSize = currentPickupItemItem.PickupObject.GetComponent<SpriteRenderer>().bounds.size;
        //currentPlatform.GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(x => bounds.Encapsulate(x.bounds));
        //platformSize = bounds.size / 2;

        platformLocation.y = Random.Range(boundsBackground.min.y + pickupItemSize.y, boundsBackground.max.y - (pickupItemSize.y));
        
        platformLocation.x = Random.Range(boundsBackground.min.x + pickupItemSize.x, boundsBackground.max.x - pickupItemSize.x);

        return platformLocation;
    }

    public void SaveLevel()
    {
        GameObjectInScene gameObjectInScene;
        List<GameObjectInScene> gameObjectList = new List<GameObjectInScene>();
        GameObject childGameObject;

        var allObjects = PlatformsInstantiated.Concat(BoxesInstantiated)
                                                //.Concat(DecorationsInstantiated)
                                                //.Concat(DeathTrapsInstantiated)
                                                .Concat(PickupsInstantiated)
                                                .ToList();

        allObjects.Add(PlayerObjectInstantiated);
        allObjects.Add(GoalObjectInstantiated);

        //allObjects.ForEach(x => gameObjectList.Add(new GameObjectInScene(x.name +"_"+ x.GetInstanceID(), x.transform.localScale, x.transform.position, x.transform.rotation, x.GetInstanceID(), x.transform.parent == null ? -1 : x.transform.parent.GetInstanceID())));

        //string json = JsonUtility.ToJson(allObjects);

        foreach (GameObject gObject in allObjects)//GameObject.FindGameObjectsWithTag("Platform"))
        {

            gameObjectInScene = new GameObjectInScene(gObject.name, gObject.tag, gObject.layer, gObject.transform.localScale, gObject.transform.position, gObject.transform.rotation, gObject.GetInstanceID(), -1);
            gameObjectList.Add(gameObjectInScene);

            if (gObject.transform.childCount > 0 && gObject.tag == "Platform")//this object has children and is a platform
            {
                //iterate children and add to gameObjectList
                for(int x = 0; x < gObject.transform.childCount - 1; x++)
                {
                    childGameObject = gObject.transform.GetChild(x).gameObject;
                    gameObjectInScene = new GameObjectInScene(childGameObject.name, childGameObject.tag, childGameObject.layer, childGameObject.transform.localScale, childGameObject.transform.position, childGameObject.transform.rotation, childGameObject.GetInstanceID(), gObject.GetInstanceID());
                    gameObjectList.Add(gameObjectInScene);
                }
            }

        }

        string json = JsonHelper.ToJson(gameObjectList.ToArray<GameObjectInScene>(), false);

        //string json = JsonUtility.ToJson(gameObjectList);

        if (!Directory.Exists("assets\\Levels"))
            Directory.CreateDirectory("assets\\Levels");

        string fileName = "level_" + System.DateTime.Now.ToShortDateString() + "_" + System.DateTime.Now.ToLongTimeString().Replace(":", "-").Replace(" ", "") + ".json";
        fileName = fileName.Replace("/", "-");
        fileName = fileName.Replace("\\", "-");

        File.WriteAllText("assets\\Levels\\"+ fileName, json);

        GetLevelList();
    }

    public void LoadLevel()
    {
        LoadLevel(Levels[LevelToLoad-1].name);
    }

    public void LoadLevel(string levelFileName)
    {
        //assets\\levels\\level_3-19-2019_7-22PM.json
        //DirectoryInfo di = new DirectoryInfo("assets\\levels\\level_3-20-2019_2-36PM.json");

        if (!levelFileName.ToLower().Contains("assets\\levels\\"))
            levelFileName = "assets\\levels\\" + levelFileName;

        string json = File.ReadAllText(levelFileName);
        GameObjectInScene[] objects = JsonHelper.FromJson<GameObjectInScene>(json);

        RegenerateLevel(objects);
    }

    private void RegenerateLevel(GameObjectInScene[] objects)
    {
        int previousID = -1;
        GameObject prevObject = null;

        ResetObjects();

        foreach (GameObjectInScene gObject in objects)
        {
            string resourcePath = "";

            switch (gObject.tag)
            {
                case "Platform":
                    resourcePath = "Platforms\\";
                    break;
                case "Decoration":
                    resourcePath = "Decorations\\";
                    break;
                case "Box":
                    resourcePath = "breakable\\";
                    break;
                case "Pickup":
                    resourcePath = "Pickups\\";
                    break;
                case "Player":
                    resourcePath = "";
                    break;
                case "Goal":
                    resourcePath = "";
                    break;
            }

            string tmpName = gObject.name.Replace("(Clone)", "");

            GameObject theResource = Resources.Load(resourcePath + tmpName, typeof(GameObject)) as GameObject;
            
            //instantiate object with loaded values
            GameObject instance;

            if (previousID == gObject.parentID && gObject.parentID != -1) //it has a parent
                instance = Instantiate(theResource, gObject.position, gObject.rotation, prevObject.transform) as GameObject;
            else
            {
                instance = Instantiate(theResource, gObject.position, gObject.rotation) as GameObject;

                previousID = gObject.ID;
                prevObject = instance;
            }

            //set values
            instance.transform.localScale = gObject.scale;

            switch (gObject.tag)
            {
                case "Platform":
                    PlatformsInstantiated.Add(instance);
                    break;
                case "Decoration":
                    DecorationsInstantiated.Add(instance);
                    break;
                case "Box":
                    BoxesInstantiated.Add(instance);
                    break;
                case "Pickup":
                    PickupsInstantiated.Add(instance);
                    break;
                case "Player":
                    PlayerObjectInstantiated = instance;
                    break;
                case "Goal":
                    GoalObjectInstantiated = instance;
                    break;
            }
        }
    }

    /// <summary>
    /// get the names of available levels
    /// </summary>
    public List<CustomLevel> GetLevelList()
    {
        Levels.Clear();

        Directory.GetFiles("assets\\levels\\", "*.json").ToList().ForEach(x => Levels.Add(new CustomLevel(x)));

        //foreach (string fileName in Directory.GetFiles("assets\\levels\\"))
        //{
        //    levels.Add(new CustomLevel(fileName));
        //}

        return Levels;
    }

    /// <summary>
    /// delete all levels marked with delete
    /// </summary>
    public void DeleteLevels()
    {
        var levels = Levels.Where(x => x.delete).ToList();

        levels.ForEach(x => File.Delete(x.name));
        levels.ForEach(x => Levels.Remove(x));

        GetLevelList();
    }
    
}

[System.Serializable]
public class GameObjectInScene
{
    public string name;
    public string tag;
    public int layer;
    public Vector3 scale;
    public Vector3 position;
    public Quaternion rotation;
    public int ID;
    public int parentID;

    public GameObjectInScene(string name, string tag, int layer, Vector3 scale, Vector3 position, Quaternion rotation, int ID, int parentID)
    {
        this.name = name;
        this.tag = tag;
        this.layer = layer;
        this.scale = scale;
        this.position = position;
        this.rotation = rotation;
        this.ID = ID;
        this.parentID = parentID;
    }
}

[System.Serializable]
public class CustomLevel
{
    public string name;
    public bool delete;
    
    public CustomLevel(string name)
    {
        this.name = name;
        this.delete = false;
    }
}