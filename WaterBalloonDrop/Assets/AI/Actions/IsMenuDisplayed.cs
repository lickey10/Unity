using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINDecision]
public class StartLevel : RAINDecision
{
    private int _lastRunning = 0;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);

        _lastRunning = 0;
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        ActionResult tResult = ActionResult.SUCCESS;

//        for (; _lastRunning < _children.Count; _lastRunning++)
//        {
//            tResult = _children[_lastRunning].Run(ai);
//            if (tResult != ActionResult.SUCCESS)
//                break;
//        }

		if (gamestate.Instance.DisplayingMenu)
			tResult = ActionResult.FAILURE;
		else
			tResult = ActionResult.SUCCESS;

        return tResult;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}