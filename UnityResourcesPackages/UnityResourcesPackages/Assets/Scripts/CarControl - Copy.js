#pragma strict

var wheelFL : WheelCollider;
var wheelFR : WheelCollider;
var wheelRL : WheelCollider;
var wheelRR : WheelCollider;
var wheelFLTrans : Transform;
var wheelFRTrans : Transform;
var wheelRLTrans : Transform;
var wheelRRTrans : Transform;
var maxTorque : float = 50;
var lowestSteerAtSpeed : float = 50;
var lowSpeedSteerAngle : float = 10;
var highSpeedSteerAngle : float = 1; 
var decelerationSpeed : float = 30;
var currentSpeed : float;
var topSpeed : float = 150;
var maxReverseSpeed : float = 50;
private var braked : boolean = false;
var maxBrakeTorque : float = 100;
private var mySidewayFriction : float;
private var myForwardFriction : float;
private var slipSidewayFriction : float;
private var slipForwardFriction : float;
var gearRatio : int[];

function Start () {
	//lower the center of mass so the car doesn't flip as easy
	GetComponent.<Rigidbody>().centerOfMass.y = -0.9;
	//move center of mass forward to simulate an engine
	GetComponent.<Rigidbody>().centerOfMass.z = 0.5;
}

function FixedUpdate () {
	controller();
	handBrake();
	setValues();
}

function Update()
{
	wheelFLTrans.Rotate(wheelFL.rpm/60*360 * Time.deltaTime * -1,0,0);
	wheelFRTrans.Rotate(wheelFR.rpm/60*360 * Time.deltaTime,0,0);
	wheelRLTrans.Rotate(wheelRL.rpm/60*360 * Time.deltaTime * -1,0,0);
	wheelRRTrans.Rotate(wheelRR.rpm/60*360 * Time.deltaTime,0,0);
	
	wheelFLTrans.localEulerAngles.y = wheelFL.steerAngle - wheelFLTrans.localEulerAngles.z;
	wheelFRTrans.localEulerAngles.y = wheelFR.steerAngle - wheelFRTrans.localEulerAngles.z;
	
	//backlights();
	wheelPosition();
	engineSound();
}

function setValues()
{
	myForwardFriction = wheelRR.forwardFriction.stiffness;
	mySidewayFriction = wheelRR.sidewaysFriction.stiffness;
	slipForwardFriction = 0.04;
	slipSidewayFriction = 0.08;
}

function controller()
{
	currentSpeed = 2*22/7*wheelRL.radius* wheelRL.rpm*60/1000; //gets kmph
	currentSpeed = Mathf.Round(currentSpeed);

	if(currentSpeed < topSpeed && currentSpeed > -maxReverseSpeed && !braked)
	{
		wheelRR.motorTorque = maxTorque * Input.GetAxis("Vertical");
		wheelRL.motorTorque = maxTorque * Input.GetAxis("Vertical");
	}
	else
	{
		wheelRR.motorTorque = 0;
		wheelRL.motorTorque = 0;
	}
	
	if(Input.GetButton("Vertical") == false)
	{
		wheelRR.brakeTorque = decelerationSpeed;
		wheelRL.brakeTorque = decelerationSpeed;
	}
	else
	{
		wheelRR.brakeTorque = 0;
		wheelRL.brakeTorque = 0;
	}
	
	var speedFactor = GetComponent.<Rigidbody>().velocity.magnitude/lowestSteerAtSpeed;
	var currentSteerAngle = Mathf.Lerp(lowSpeedSteerAngle, highSpeedSteerAngle,speedFactor);
	
	currentSteerAngle *= Input.GetAxis("Horizontal");
	wheelFL.steerAngle = currentSteerAngle;
	wheelFR.steerAngle = currentSteerAngle;
}

function wheelPosition()
{
	var hit : RaycastHit;
	var wheelPos : Vector3;
	
	//wheelFL
	//cast a ray down from the center of the wheel to see if we are touching the ground
	if(Physics.Raycast(wheelFL.transform.position, -wheelFL.transform.up,hit, wheelFL.radius + wheelFL.suspensionDistance))
	{
		//get the position of where the raycast is hitting
		wheelPos = hit.point + wheelFL.transform.up * wheelFL.radius;
	}
	else
	{
		//if raycast is not hitting anything the wheel will be at the bottom of the suspension
		wheelPos = wheelFL.transform.position - wheelFL.transform.up * wheelFL.suspensionDistance;
	}
	
	wheelFLTrans.position = wheelPos;
	
	//wheelFR
	//cast a ray down from the center of the wheel to see if we are touching the ground
	if(Physics.Raycast(wheelFR.transform.position, -wheelFR.transform.up,hit, wheelFR.radius + wheelFR.suspensionDistance))
	{
		//get the position of where the raycast is hitting
		wheelPos = hit.point + wheelFR.transform.up * wheelFR.radius;
	}
	else
	{
		//if raycast is not hitting anything the wheel will be at the bottom of the suspension
		wheelPos = wheelFR.transform.position - wheelFR.transform.up * wheelFR.suspensionDistance;
	}
	
	wheelFRTrans.position = wheelPos;
	
	//wheelRL
	//cast a ray down from the center of the wheel to see if we are touching the ground
	if(Physics.Raycast(wheelRL.transform.position, -wheelRL.transform.up,hit, wheelRL.radius + wheelRL.suspensionDistance))
	{
		//get the position of where the raycast is hitting
		wheelPos = hit.point + wheelRL.transform.up * wheelRL.radius;
	}
	else
	{
		//if raycast is not hitting anything the wheel will be at the bottom of the suspension
		wheelPos = wheelRL.transform.position - wheelRL.transform.up * wheelRL.suspensionDistance;
	}
	
	wheelRLTrans.position = wheelPos;
	
	//wheelRR
	//cast a ray down from the center of the wheel to see if we are touching the ground
	if(Physics.Raycast(wheelRR.transform.position, -wheelRR.transform.up,hit, wheelRR.radius + wheelRR.suspensionDistance))
	{
		//get the position of where the raycast is hitting
		wheelPos = hit.point + wheelRR.transform.up * wheelRR.radius;
	}
	else
	{
		//if raycast is not hitting anything the wheel will be at the bottom of the suspension
		wheelPos = wheelRR.transform.position - wheelRR.transform.up * wheelRR.suspensionDistance;
	}
	
	wheelRRTrans.position = wheelPos;
}

function handBrake()
{
	if(Input.GetButton("Jump"))
	{
		braked = true;
	}
	else
	{
		braked = false;
	}
	
	if(braked)
	{
		wheelFR.brakeTorque = maxBrakeTorque;
		wheelFL.brakeTorque = maxBrakeTorque;
		
		wheelRR.motorTorque = 0;
		wheelRL.motorTorque = 0;
		
		if(GetComponent.<Rigidbody>().velocity.magnitude > 1)
		{
			setSlip(slipForwardFriction, slipSidewayFriction);
		}
		else
		{
			//set to max friction
			setSlip(1,1);
		}
		
		if(currentSpeed < 1 && currentSpeed > -1)
		{
			//apply backlight idle material
			
		}
		else
		{
			//apply brakelight material
			
		}
	}
	else
	{
		wheelFR.brakeTorque = 0;
		wheelFL.brakeTorque = 0;
		
		setSlip(myForwardFriction, mySidewayFriction);
	}
}

function setSlip(currentForwardFriction : float, currrentSidewaysFriction : float)
{
	wheelRR.forwardFriction.stiffness = currentForwardFriction;
	wheelRL.forwardFriction.stiffness = currentForwardFriction;
//	wheelFR.forwardFriction.stiffness = currentForwardFriction;
//	wheelFL.forwardFriction.stiffness = currentForwardFriction;
	
	wheelRR.sidewaysFriction.stiffness = currrentSidewaysFriction;
	wheelRL.sidewaysFriction.stiffness = currrentSidewaysFriction;
//	wheelFR.sidewaysFriction.stiffness = currrentSidewaysFriction;
//	wheelFL.sidewaysFriction.stiffness = currrentSidewaysFriction;
}

function engineSound()
{
	for(var i = 0; i < gearRatio.length; i++)
	{
		if(gearRatio[i] > currentSpeed)
			break;
	}

	var gearMinValue : float = 0.00;
	var gearMaxValue : float = 0.00;
	
	if(i == 0)
	{
		gearMinValue = 0;
		gearMaxValue = gearRatio[i];
	}
	else
	{
		gearMinValue = gearRatio[i-1];
		gearMaxValue = gearRatio[i];
	}
	
	var enginePitch : float = ((currentSpeed - gearMinValue) / (gearMaxValue - gearMinValue)) + 1;
	
	GetComponent.<AudioSource>().pitch = enginePitch;
}