using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle
{
	public Vector3 position;
	public Vector3 heading;
	public float speed;
	public Vector3 steering;
	public float wheelBase;
	public float minTurnAngle;
	public float maxTurnAngle;
	public Vector3 frontWheel;
	public Vector3 backWheel;
	public Vehicle()
	{
		position = new Vector3(0.0f,6.0f,0.0f);
		heading = new Vector3(0.0f,0.0f,1.0f);
		speed = 0.0f;
		steering = new Vector3();
		wheelBase = 1.0f;
		minTurnAngle= -45.0f;
		maxTurnAngle = 45.0f;
		updateWheels();
	}
	public void updateWheels()
	{
		frontWheel = position + wheelBase*0.5f*heading;
		backWheel = position - wheelBase*0.5f*heading;
	}
	public void steerTo(Vector3 desiredHeading)
	{	

		steering = (desiredHeading-heading).normalized;

		// Plane p = new Plane(Vector3.up,Vector3.zero);
		// Debug.Log("desired Dir dist from plane: "+p.GetDistanceToPoint(desiredHeading));
		// Debug.Log("Heading dist from plane: "+p.GetDistanceToPoint(heading));
		// Vector3 steerDir = (heading-desiredHeading).normalized;
		// float realAngle = SignedAngle(steerDir,heading);
		// float clampedAngle = Mathf.Clamp(realAngle,minTurnAngle,maxTurnAngle);

		// Debug.Log("ROTATION ANGLE "+realAngle+" clamped: "+clampedAngle);
		// steering = (Quaternion.AngleAxis(clampedAngle,Vector3.up)*heading).normalized;
	}
	public float SignedAngle(Vector3 a, Vector3 b){
	   var angle = Vector3.Angle(a, b); // calculate angle
	   // assume the sign of the cross product's Y component:
	   return angle * Mathf.Sign(Vector3.Cross(a, b).y);
	}
	public void update()
	{
		updateWheels();

		float dt = Time.deltaTime;
		backWheel += speed * dt * heading;
		frontWheel += speed * dt * (heading+steering).normalized ;//new Vector2(cos(carHeading+steerAngle) , sin(carHeading+steerAngle));
		position = (frontWheel + backWheel) / 2;
		heading = (frontWheel-backWheel).normalized;
		// heading = Mathf.Atan2( frontWheel.Y - backWheel.Y , frontWheel.X - backWheel.X );
	}
}

public class Stick : MonoBehaviour {

	Vector3 target;
	RaycastHit hit;
	bool hasTarget;
	Vehicle vehicle;
	
	// Use this for initialization
	void Start () {
		UpdateStick();
		target = transform.position;
		
		InitVehicle();
		SynchVehicle();
		hasTarget = false;
	}	
  
	// Update is called once per frame
	void Update()
	{

		// UpdateStick();
		UpdateTarget();
		UpdateVehicle();
		// UpdateMovement2();
	}
	void InitVehicle()
	{
		vehicle=new Vehicle();
	}
	void SynchVehicle()
	{
		gameObject.transform.position = vehicle.position;
		UpdateStick();
		
		// Vector3 currentHeading = transform.TransformDirection(Vector3.forward);


		Quaternion rotation = Quaternion.LookRotation(vehicle.heading,gameObject.transform.position.normalized);
  		// Quaternion rotation = Quaternion.FromToRotation(currentHeading, vehicle.heading);
  		gameObject.transform.rotation *= rotation;

	}
	void UpdateVehicle()
	{
		if(hasTarget)
		{
			vehicle.speed = 5.0f;
			Vector3 targetOnPlane = Vector3.ProjectOnPlane(target,gameObject.transform.position.normalized);
			// Vector3 vehicleOnPlane = Vector3.ProjectOnPlane(vehicle.position,gameObject.transform.position.normalized);

			Vector3 desiredDir =Quaternion.FromToRotation(gameObject.transform.position.normalized, Vector3.up)*targetOnPlane.normalized;
			desiredDir = Vector3.ProjectOnPlane(desiredDir,Vector3.up);
			vehicle.steerTo(desiredDir);
		}

		vehicle.update();

		SynchVehicle();
		if(hasTarget && Vector3.Distance(gameObject.transform.position,target)<0.05)
		{
			hasTarget=false;
			vehicle.speed = 0.0f;
		}


	}
	void UpdateStick()
	{
		var down = ( Vector3.zero -transform.position).normalized;
		if(Physics.Raycast(transform.position, down, out hit))
		{
			// Debug.Log("FOund hit");
			gameObject.transform.position = hit.point;// + hit.normal;
			gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
		}
	}
	void UpdateTarget()
	{
		if(Input.GetMouseButtonUp(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
           target = hit.point;
           
          
           // moving = true;
           hasTarget = true;
           Debug.Log("New target "+target);
        }
	}
	// void UpdateMovement3()
	// {
	// 	if(!moving)
	// 		return;
	// 	float maxVel = 1.0f;
	// 	float maxForce = 1.0f;
	// 	Vector3 position = gameObject.transform.position;
	// 	Vector3 forward = transform.TransformDirection(Vector3.forward);

	// 	// gameObject.transform.position = position+forward*Time.deltaTime;
	// 	Vector3 targetPos = Vector3.ProjectOnPlane(target,gameObject.transform.position.normalized);
	// 	targetPos+=gameObject.transform.position;


	// 	Vector3 desired_vel = (targetPos-position ).normalized;
	// 	Vector3 steering = (desired_vel-forward);
	// 	Vector3 vel = forward + steering*0.5f;

 //  //       // Vector3 b = Vector3.ProjectOnPlane(neighborEnt.gridCell.centroid, cellCentroid.normalized);
	// 	Quaternion rotation = Quaternion.LookRotation(vel,position.normalized);
 //        gameObject.transform.rotation = rotation;

	// 	gameObject.transform.position = position+vel*Time.deltaTime;

	// }
	// void UpdateMovement2()
	// {
	// 	if(!moving)
	// 		return;
	// 	Vector3 forward = transform.TransformDirection(Vector3.forward);
	// 	//Projecting the hit point onto a plane should give us a point that will determine the direction we need to move in
	// 	Vector3 targetOnPlane = Vector3.ProjectOnPlane(target,gameObject.transform.position.normalized);
	// 	targetOnPlane+=gameObject.transform.position;
	// 	Vector3 desiredDir = (targetOnPlane-gameObject.transform.position ).normalized;
		

	// 	//BROKEN
	// 	Vector3 steerDir = (desiredDir-forward);
	// 	Vector3 moveDir = (desiredDir+steerDir).normalized*1.0f;

		
		
	// 	Quaternion rotation = Quaternion.LookRotation(moveDir,gameObject.transform.position.normalized);
 //        gameObject.transform.rotation = rotation;
 //        gameObject.transform.position+=moveDir*Time.deltaTime;

 //        // gameObject.transform.position-=desiredDir*Time.deltaTime;


	// 	if(Vector3.Distance(gameObject.transform.position,target)<0.01)
	// 	{ 	
	// 		Debug.Log("CLose enough!");
	// 		gameObject.transform.position=target;
	// 		moving = false;
	// 	}

	// }
	// void UpdateMovement()
	// {
	// 	Vector3 direction = transform.forward;
 //        float speed = 1.0f;
 //        float move = 1.0f;
 //        float deltaTime = Time.deltaTime;

	// 	Vector3 newPosition = body.position + direction * move * speed * deltaTime;

	// 	body.MovePosition(newPosition);
	// }
}
