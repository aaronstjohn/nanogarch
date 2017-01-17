using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaterTest : MonoBehaviour {

	 private Vector3 v3StartRot;
	 private  Vector3 v2Axis;
	 private float fStartAngle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnMouseDown(){
		Debug.Log("DOWN");
	    v3StartRot = transform.eulerAngles;
	    v2Axis = Camera.main.WorldToScreenPoint(transform.position);
	    Vector2 v2T = Input.mousePosition - v2Axis;
	    fStartAngle = Mathf.Atan2(v2T.y, v2T.x);
	 }
 
	public void OnMouseDrag(){
		Vector2 v2T = Input.mousePosition - v2Axis;
		float fAngle = Mathf.Atan2(v2T.y, v2T.x);
		Vector3 v3T  = v3StartRot;
		v3T.y = v3T.y - (fAngle - fStartAngle) * Mathf.Rad2Deg;
		transform.eulerAngles = v3T;
	}
}
