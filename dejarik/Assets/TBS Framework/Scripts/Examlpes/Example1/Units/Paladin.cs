using System.Collections.Generic;
using UnityEngine;

public class Paladin : MyUnit
{
	Animator anim;

	void Start ()
	{
		anim = GetComponent<Animator>();
	}

    protected override void Defend(Unit other, int damage)
    {
        var realDamage = damage;
        if (other is Spearman)
            realDamage *= 2;//Spearman deals double damage to paladin.

        base.Defend(other, realDamage);
    }

	public override void Move(Cell destinationCell, List<Cell> path)
	{
		//anim.SetBool ("Moving", true);
		
		base.Move(destinationCell, path);
	}
}
