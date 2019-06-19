#pragma strict

var target : Transform; 
var smooth : float = 5; 
var XOffset : float = 1;
var YOffset : float = 2;

function Update () { 
transform.position.x = Mathf.Lerp(transform.position.x,target.position.x + XOffset,Time.deltaTime*smooth); 
transform.position.y = Mathf.Lerp(transform.position.y,target.position.y + YOffset,Time.deltaTime*smooth);

}