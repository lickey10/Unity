using SgLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameGoals : MonoBehaviour {

    [Header("Goal Title")]
    public string Title = "";
    public int GoalBonusPoints = 100;
    public GameObject[] LightGroup;
    public GameObject[] BonusLightGroup;

    private LightController lightController; 

    // Use this for initialization
    void Start () {
        lightController = new LightController();

        lightController.LightIsLit += lightController_LightIsLit;
    }

    private void lightController_LightIsLit(object sender, System.EventArgs args)
    {
        //check that the light that turned on is in LightGroup
        if (!LightGroup.Where(x => x.GetComponent<LightController>() == (LightController)sender).Any())
            return;

        //check to see if we have completed a goal
        completedGroup(sender);
    }

    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// Have we completed any of the groups
    /// </summary>
    /// <returns></returns>
    private bool completedGroup(object sender)
    {
        bool groupCompleted = false;

        //check to see if the groups the light belongs to have been lit
        if(!LightGroup.Where(x => !x.GetComponentInParent<LightGroupController>().GroupHasBeenLit).Any())
        {
            LightGroup.ToList().ForEach(x => x.GetComponentInParent<LightGroupController>().GroupHasBeenLit = false);

            //turn the next bonus light on
            BonusLightGroup.Where(x => !x.GetComponent<LightController>().On).FirstOrDefault().GetComponent<LightController>().TurnLightOn();

            if (BonusLightGroup.Where(x => !x.GetComponent<LightController>().On).Any())//all the bonus lights are lit - flash all the lights in the BonusLightGroup
            {
                BonusLightGroup.Where(x => !x.GetComponent<LightController>()).ToList().ForEach(x => x.GetComponent<LightController>().Flash(5, .2f));

                ScoreManager.Instance.AddScore(GoalBonusPoints);

                groupCompleted = true;
            }
        }

        return groupCompleted;
    }
}
