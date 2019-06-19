using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public enum ModeEnum{Lock = 1, Edit= 2}

public class GameManager : MonoBehaviour
{

    public static GameManager SP;
	public bool InEditMode = false;

	private bool lastMode = false;

    private ArrayList objectsList;
	//private ArrayList combinationObjectsList;
	//[System.Serializable]
	List<CombinationObject> combinationObjectsList = new List<CombinationObject>();
    private float moveSpeed = 1.5f;
    private int spawnedObjects = 0;
    private int score;
	private int rowTopCount = 0;
	private int rowMiddleCount = 0;
	private int rowBottomCount = 0;
	private bool successfulCombination = true;

	public void SetInEditMode(string inEditMode)
	{
		//if(inEditMode.ToLower() == "true")
			InEditMode = true;
//		else
//			InEditMode = false;
	}

	void Awake () {
        SP = this;

        objectsList = new ArrayList();
        spawnedObjects = score =0;
		load();
	}

    void Update()
    {
		if(!gamestate.Instance.GetGamePaused())
		{
	        //Move objects
	        for (int i = objectsList.Count - 1; i >= 0; i--)
	        {
	            float farLeft = -10;
	            float farRight = 10;

	            MovingObject movObj = (MovingObject)objectsList[i];
	            Transform trans = movObj.transform;
	            trans.Translate((int)movObj.direction * Time.deltaTime * moveSpeed, 0, 0);
	            if (trans.position.x < farLeft || trans.position.x > farRight)
	            {
	                Destroy(trans.gameObject);
	                objectsList.Remove(movObj);
	            }
	        }
		}
    }

    void OnGUI(){
		GUILayout.Label(" SetInEditMode= " + InEditMode);

		if(!gamestate.Instance.GetGamePaused())
			InEditMode = GUILayout.Toggle(InEditMode,"Edit Mode");

		if(InEditMode != gamestate.Instance.GetLastMode())
		{
			gamestate.Instance.SetInEditMode(InEditMode);
			
			gamestate.Instance.SetLastMode(InEditMode);

			//we are switching modes so restart the level
			Application.LoadLevel(Application.loadedLevel);
		}

		if(!gamestate.Instance.GetGamePaused())
		{
			if(!InEditMode)
			{
					if(GUILayout.Button("Restart")){
						Application.LoadLevel(Application.loadedLevel);
					}

					GUILayout.Label(" Hit: " + score + "/" + spawnedObjects);
			}
			else
			{
				if(GUILayout.Button("Reset Combo")){
					combinationObjectsList = new List<CombinationObject>();
				}

				if(GUILayout.Button("Save")){
					//save the combinationObjectsList
					save();
				}
			}
		}
    }

	private void save() {
		if(combinationObjectsList != null && combinationObjectsList.Count > 0)
		{
			try {
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Create (Application.persistentDataPath + "/combo.gd");
				bf.Serialize(file, combinationObjectsList);
				file.Close();
			} catch (System.Exception ex) {
				Debug.LogError(ex.Message);
			}
		}
		else
		{
			//they need to select a target for the combination

		}
	}

	private void load() {
		InEditMode = gamestate.Instance.GetInEditMode();

		if(File.Exists(Application.persistentDataPath + "/combo.gd")) {
			try {
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/combo.gd", FileMode.Open);
				combinationObjectsList = (List<CombinationObject>)bf.Deserialize(file);
				file.Close();
			} catch (System.Exception ex) {
				Debug.LogError(ex.Message);
			}
		}
		else //there is no file - create a default file and combo
		{
			//default combo is the first target on the top row
			CombinationObject combinationObject =  new CombinationObject(1,1);
			combinationObjectsList.Add(combinationObject);
			save ();
		}
	}

    public void AddTarget(MovingObject newObj){
        spawnedObjects++;
        objectsList.Add(newObj);
    }

    public bool RemoveObject(Transform trans)
    {
        foreach (MovingObject obj in objectsList)
        {
			int selectedRowCounter = -1;

            if (obj.transform == trans)
            {
				//add row counter
				switch(obj.Row)
				{
				case (int)RowEnum.Top:
					rowTopCount++;
					selectedRowCounter = rowTopCount;
					break;
				case (int)RowEnum.Middle:
					rowMiddleCount++;
					selectedRowCounter = rowMiddleCount;
					break;
				case (int)RowEnum.Bottom:
					rowBottomCount++;
					selectedRowCounter = rowBottomCount;
					break;
				}

				//the selected object
				CombinationObject combinationObject =  new CombinationObject(obj.Row,selectedRowCounter);

				switch(InEditMode)
				{
				case(false):
					score++;
					objectsList.Remove(obj);
					Destroy(obj.transform.gameObject); 

					//check the selected target to the first combinationObjectsList object to see if we have a match
					if(successfulCombination && combinationObjectsList != null && combinationObjectsList.Count > 0 && combinationObjectsList[0].Row == combinationObject.Row && combinationObjectsList[0].RowCounter == combinationObject.RowCounter)
					{
						combinationObjectsList.RemoveAt(0);

						if(combinationObjectsList.Count == 0)//we successfully have our combination - unlock the screen
						{
                                Application.LoadLevel("BlankScene");
                                //Application.Quit();
                                
                            }
					}
					else//this is the wrong combination
					{
						Debug.Log("Wrong Combination");
						successfulCombination = false;
					}

					break;
				case (true):
					combinationObjectsList.Add(combinationObject);

					objectsList.Remove(obj);
					Destroy(obj.transform.gameObject); 
					break;
				}

				return true;
            }
        }
        Debug.LogError("ERROR: Couldn't find target!");
        return false;
    }
       
}
