using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyUnit : Unit
{
    public Color PlayerColor;
	public AudioClip AttackAudioClip;
	public AudioClip DieAudioClip;
	public AudioClip GetHitAudioClip;
	public AudioClip MovingAudioClip;
    public string UnitName;
    public GameObject MyMagicSpell;

    private Transform Highlighter;
	Animator animator;
	AudioSource audioSource;
    private GameObject currentParticle;

    public override void Initialize()
    {
        base.Initialize();
        SetColor(PlayerColor);

        Highlighter = transform.Find("Highlighter");
        if (Highlighter != null)
        {
            Highlighter.position = transform.position + new Vector3(0, 0, 1.5f);
            foreach (Transform cubeTransform in Highlighter)
                Destroy(cubeTransform.GetComponent<BoxCollider>());
        }     
        gameObject.transform.position = Cell.transform.position + new Vector3(0, 0, -1.5f);
    }

	void Start ()
	{
		animator = GetComponentInChildren<Animator>();
		audioSource = GetComponentInChildren<AudioSource>();
	}
	
	void Update()
	{
		string error = "";

		if(animator)
			animator.SetBool ("Moving", base.isMoving);
	}

	public override void DealDamage(Unit other)
	{
		if(animator)
			animator.SetTrigger("Attack");

		if (AttackAudioClip && audioSource) 
			AudioSource.PlayClipAtPoint(AttackAudioClip,transform.position);

        //if this is an archer we need to send a magic fireball over at them
        if(this is Archer)
        {
                //if (currentParticle)
                //    Destroy(currentParticle);
                currentParticle = (GameObject)Instantiate(MyMagicSpell, transform.position, Quaternion.identity);

            currentParticle.GetComponent<ShootSpell>().ShootAt(other.transform);

                //currentParticle.transform.position = currentParticle.GetComponent<demoParticleControl>().startPosition;
                //currentParticle.transform.eulerAngles = currentParticle.GetComponent(demoParticleControl).startRotation;

            //if (currentParticle.GetComponent(demoParticleControl).shootsTarget)
            //{
            //    currentParticle.transform.LookAt(targets[targetNumber].transform);
            //    targetNumber += 1;
            //    if (targetNumber < 0)
            //        targetNumber = targets.Length - 1;
            //    else if (targetNumber == targets.Length)
            //        targetNumber = 0;
            //}

        }

		//yield return new WaitForAnimation( "Attack" );
		
		base.DealDamage (other);

	}

    protected override void Defend(Unit other, int damage)
    {
		if(animator)
			animator.SetTrigger("GetHit");

		if (GetHitAudioClip && audioSource) 
			AudioSource.PlayClipAtPoint (GetHitAudioClip, transform.position);

        base.Defend(other, damage);
        UpdateHpBar();
    }

    public override void Move(Cell destinationCell, List<Cell> path)
    {
		if (MovingAudioClip && audioSource) 
			AudioSource.PlayClipAtPoint (MovingAudioClip, transform.position);

        base.Move(destinationCell, path);
    }

    public override void MarkAsAttacking(Unit other)
    {
        StartCoroutine(Jerk(other));
    }
    public override void MarkAsDefending(Unit other)
    {
        StartCoroutine(Glow(new Color(1, 0.5f, 0.5f), 1));
    }
    public override void MarkAsDestroyed()
    {
		if(animator)
			animator.SetTrigger("Die");

		if (DieAudioClip && audioSource) 
			AudioSource.PlayClipAtPoint (DieAudioClip, transform.position);
    }

    private IEnumerator Jerk(Unit other)
    {
        var heading = other.transform.position - transform.position;
        var direction = heading / heading.magnitude;
        float startTime = Time.time;

        while (startTime + 0.25f > Time.time)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + (direction / 2.5f), ((startTime + 0.25f) - Time.time));
            yield return 0;
        }
        startTime = Time.time;
        while (startTime + 0.25f > Time.time)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position - (direction / 2.5f), ((startTime + 0.25f) - Time.time));
            yield return 0;
        }
        transform.position = Cell.transform.position + new Vector3(0, 0, -1.5f); ;
    }
    private IEnumerator Glow(Color color, float cooloutTime)
    {
        float startTime = Time.time;

        while (startTime + cooloutTime > Time.time)
        {
            SetColor(Color.Lerp(PlayerColor, color, (startTime + cooloutTime) - Time.time));
            yield return 0;
        }

        SetColor(PlayerColor);
    }

    public override void MarkAsFriendly()
    {
        SetHighlighterColor(new Color(0.8f,1,0.8f));
    }
    public override void MarkAsReachableEnemy()
    {
		SetHighlighterColor(Color.red);

		if(animator)
			animator.SetTrigger("WaitForBattle");
    }
    public override void MarkAsSelected()
    {
        SetHighlighterColor(new Color(0,1,0));
    }
    public override void MarkAsFinished()
    {
        SetColor(PlayerColor - Color.gray);
        SetHighlighterColor(new Color(0.8f, 1, 0.8f));
    }
    public override void UnMark()
    {
        SetColor(PlayerColor);
        SetHighlighterColor(Color.white);
        if (Highlighter == null) return;
            Highlighter.position = transform.position + new Vector3(0, 0, 1.52f);
    }

    private void UpdateHpBar()
    {
        if (GetComponentInChildren<Image>() != null)
        {
            GetComponentInChildren<Image>().transform.localScale = new Vector3((float)((float)HitPoints / (float)TotalHitPoints), 1, 1);
            GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.green,
                (float)((float)HitPoints / (float)TotalHitPoints));
        }
    }
    private void SetColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
    private void SetHighlighterColor(Color color)
    {

        if (Highlighter == null) return;

        //Highlighter.position = transform.position + new Vector3(0, 0, -1.48f);
		Highlighter.position = transform.position + new Vector3(0, 0, 1f);

        for (int i = 0; i < Highlighter.childCount; i++)
        {
            var rendererComponent = Highlighter.transform.GetChild(i).GetComponent<Renderer>();
            if (rendererComponent != null)
                rendererComponent.material.color = color;
        }
    }

	private IEnumerator WaitForAnimation ( string animationName )
	{
//		do
//		{
//			yield return null;
//		} while ( animation.isPlaying );

		do
		{
			yield return null;
		} while ( this.animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) );

	}
}