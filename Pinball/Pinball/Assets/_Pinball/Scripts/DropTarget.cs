using SgLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTarget : MonoBehaviour {

    public GameObject CorrespondingLight;
    public int GroupBonusPoints = 50;
    public int Points = 10;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        this.gameObject.SetActive(false);

        var light = CorrespondingLight.GetComponent<LightController>();

        if (light != null)
            light.TurnLightOn();

        ScoreManager.Instance.AddScore(Points);

        if(!isTargetGroupActive())
        {
            activateTargetGroup();
        }
    }

    /// <summary>
    /// check the other targets in the group and make sure they at least one is still active
    /// </summary>
    private bool isTargetGroupActive()
    {
        if(this.transform.parent.name.ToLower().Contains("group"))
        {
            foreach (Transform target in transform.parent)
            {
                if (target.gameObject.activeSelf)
                    return true;
            }
        }

        return false;
    }

    private void activateTargetGroup()
    {
        if (this.transform.parent.name.ToLower().Contains("group"))
        {
            ScoreManager.Instance.AddScore(GroupBonusPoints);

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
