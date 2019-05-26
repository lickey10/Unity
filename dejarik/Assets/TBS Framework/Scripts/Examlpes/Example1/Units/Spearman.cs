using System.Collections.Generic;
using UnityEngine;

public class Spearman : MyUnit
{
	Animator anim;
	
	void Start ()
	{
		//anim = GetComponent<Animator>();
	}

	void Update()
	{
		//anim.SetBool ("Moving", base.isMoving);
	}

    protected override void Defend(Unit other, int damage)
    {
        var realDamage = damage;
        if (other is Archer)
            realDamage *= 2;//Archer deals double damage to spearman.

        base.Defend(other, realDamage);
    }

	public override void Move(Cell destinationCell, List<Cell> path)
	{
		base.Move(destinationCell, path);
	}
}
