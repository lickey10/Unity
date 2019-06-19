using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RemoveCar : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		//MonoBehaviour.Destroy(ai.Body);
		ai.Body.gameObject.SendMessage("DestroyCar");

		SpawnAI spawnAI = ai.Body.GetComponentInChildren<SpawnAI>();
		spawnAI.Remove ();

		/*Spawner myScript = GameObject.Find ("Spawner_01").GetComponentInChildren<Spawner>();
		if (myScript.totalUnits > 0) {
			myScript.RemoveUnit ();
			//return ActionResult.SUCCESS;
		}

		myScript = GameObject.Find ("Spawner_02").GetComponentInChildren<Spawner>();
		if (myScript.totalUnits > 0) {
			myScript.RemoveUnit ();
			//return ActionResult.SUCCESS;
		}

		myScript = GameObject.Find ("Spawner_03").GetComponentInChildren<Spawner>();
		if (myScript.totalUnits > 0) {
			myScript.RemoveUnit ();
			//return ActionResult.SUCCESS;
		}

		myScript = GameObject.Find ("Spawner_04").GetComponentInChildren<Spawner>();
		if (myScript.totalUnits > 0) {
			myScript.RemoveUnit ();
			//return ActionResult.SUCCESS;
		}*/

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}