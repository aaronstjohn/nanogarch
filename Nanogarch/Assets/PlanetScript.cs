using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour {

	Ray ray;
    RaycastHit hit;
    MeshData meshData;
	// Use this for initialization
	void Start () {
		//Physics.queriesHitTriggers=true;
		MeshFilter filter = gameObject.GetComponentInParent<MeshFilter>();
		Mesh mesh = filter.sharedMesh;
		meshData = Generate.CreateMeshData(mesh);
	}
	
	// Update is called once per frame
	void Update () {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit))
		{
		 print (hit.collider.name);
		 print (hit.point);
		 // MeshRenderer sphere = gameObject.GetComponentInParent<MeshRenderer>( );MeshFilter filter = (MeshFilter)sphere.AddComponent(typeof(MeshFilter));


		}

	}

}
