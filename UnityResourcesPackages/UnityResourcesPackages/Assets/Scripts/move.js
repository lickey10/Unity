#pragma strict

var forwardRate : float = 3;
var turnRate : float = 2;


function Start () {

}

function Update () {

//tanks forward speed in action
var forwardMoveAmount = Input.GetAxis("Vertical") * forwardRate;

//force of tanks turn
var turnForce = Input.GetAxis("Horizontal") * turnRate;

//rotate tank in action
transform.Rotate(0, turnForce,0);

transform.position += transform.right * forwardMoveAmount * Time.deltaTime;


}