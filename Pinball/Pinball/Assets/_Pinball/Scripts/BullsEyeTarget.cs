using SgLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BullsEyeTarget : MonoBehaviour {

    public GameObject[] CorrespondingLights;
    //public int GroupBonusPoints = 50;
    public int Points = 10;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        var light = CorrespondingLights.Where(x => !x.GetComponent<LightController>().On).FirstOrDefault();

        if (light != null)
            light.GetComponent<LightController>().TurnLightOn();

        ScoreManager.Instance.AddScore(Points);
    }

    /// <summary>
    /// check the other targets in the group and make sure that at least one is still active
    /// </summary>
    private bool isTargetGroupActive()
    {
        if(this.transform.parent.name.ToLower().Contains("group"))
        {
            foreach (Transform target in transform.parent)
            {
                //if (target.gameObject.activeSelf)
                GameObject light = target.gameObject.GetComponent<GameObject>();

                if (light != null && light.GetComponent<LightController>().On)//turn the light off
                    return true;
            }
        }

        return false;
    }

    private void activateTargetGroup()
    {
        if (this.transform.parent.name.ToLower().Contains("group"))
        {
            //ScoreManager.Instance.AddScore(GroupBonusPoints);

            foreach (Transform target in transform.parent)
            {
                target.gameObject.SetActive(true);

                GameObject light = target.gameObject.GetComponent<GameObject>();

                if(light != null)//turn the light off
                    light.GetComponent<LightController>().TurnLightOff();
            }
        }
    }
}
