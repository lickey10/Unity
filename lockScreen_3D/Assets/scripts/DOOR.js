var doorClip : AnimationClip;
var Key : GameObject;
private var Door = false;

function Start () 
{

}

function Update () 
{
	if (Input.GetKeyDown(KeyCode.E) && Door == true && Key.active == false)
	{
		//GameObject.Find("leftGate").animation.Play("gateOpenLeft");
		gameObject.GetComponent.<Animation>().Play(doorClip.name);
	}
}

function OnTriggerEnter (theCollider : Collider)
{
	if (theCollider.tag == "Player")
	{
		Door = true;
	}
}

function OnTriggerExit (theCollider : Collider)
{
	if (theCollider.tag == "Player")
	{
		Door = false;
	}
}