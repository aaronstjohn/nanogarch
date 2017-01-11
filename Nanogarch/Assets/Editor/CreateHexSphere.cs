using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System;


public class CreateHexSphere : ScriptableWizard
{
	public int recursionLevel = 1;

	public float radius = 0.5f;
	static Camera cam;
	static Camera lastUsedCam;

	[MenuItem("GameObject/Create Other/HexSphere...")]
	static void CreateWizard()
	{
		cam = Camera.current;
		// Hack because camera.current doesn't return editor camera if scene view doesn't have focus
		if (!cam)
			cam = lastUsedCam;
		else
			lastUsedCam = cam;
		ScriptableWizard.DisplayWizard("Create HexSphere",typeof(CreateHexSphere));
	}
	


	void OnWizardUpdate()
	{
		recursionLevel = Mathf.Clamp(recursionLevel, 1, 3);
	}

	void OnWizardCreate()
	{

		GameObject sphere = new GameObject();
		sphere.name = "HexSphere";
		sphere.transform.position = Vector3.zero;

		MeshFilter filter = (MeshFilter)sphere.AddComponent(typeof(MeshFilter));
		sphere.AddComponent(typeof(MeshRenderer));

		string sphereAssetName = sphere.name + recursionLevel  + ".asset";
		Icosahedron icosahedron = new Icosahedron(radius,recursionLevel);
		// icosahedron.SubdivideFaces();
		TruncatedIcosahedron trunc = new TruncatedIcosahedron(icosahedron);
		// icoSphere.ConvertToTruncatedIsocahedron();
		// MeshData singlePoly = icoSphere.GetPolyData(0);

		Mesh mesh = Generate.CreateMesh(trunc,sphere.name);
		AssetDatabase.CreateAsset(mesh, "Assets/Editor/" + sphereAssetName);
		AssetDatabase.SaveAssets();
		
		filter.sharedMesh = mesh;
		mesh.RecalculateBounds();
 		Selection.activeObject = sphere;
		// // Mesh mesh = (Mesh)AssetDatabase.LoadAssetAtPath("Assets/Editor/" + sphereAssetName,typeof(Mesh));
		// // if (mesh == null)
		// // {	
		// SphericalMesh sphericalMesh = new SphericalMesh(radius);

		// Mesh mesh = sphericalMesh.createMesh(sphere.name);
		// sphericalMesh.CountHexesAndPentagons();
		// // List<Vector3> centroids = sphericalMesh.GetHexCentroids();
		// List<Vector3> centroids = sphericalMesh.GetFaceCentroids();
		// Debug.Log("CENTROID COUNT : "+centroids.Count);
		// GameObject zeroObj = new GameObject();
		// zeroObj.name = "ZeroObj";
		// zeroObj.transform.position = Vector3.zero;
		// zeroObj.transform.parent = sphere.transform;
		// foreach(var cent in centroids)
		// {
		// 	GameObject centroidSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		// 	GameObject cylinderParent = new GameObject();

		// 	cylinderParent.transform.parent = sphere.transform;
		// 	cylinderParent.transform.position = cent;
		// 	centroidSphere.transform.parent = cylinderParent.transform;

		// 	centroidSphere.transform.Rotate(Vector3.right*90.0f);
		// 	cylinderParent.transform.localScale = new Vector3(radius*0.01f,radius*0.01f,radius*0.01f);
		// 	cylinderParent.transform.LookAt(zeroObj.transform);

		// }
		// centroids = sphericalMesh.GetHexCentroids();
		// foreach(var cent in centroids)
		// {
		// 	GameObject centroidSphere = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		// 	GameObject cylinderParent = new GameObject();

		// 	cylinderParent.transform.parent = sphere.transform;
		// 	cylinderParent.transform.position = cent;
		// 	centroidSphere.transform.parent = cylinderParent.transform;

		// 	centroidSphere.transform.Rotate(Vector3.right*90.0f);
		// 	cylinderParent.transform.localScale = new Vector3(radius*0.1f,radius*0.1f,radius*0.1f);
		// 	cylinderParent.transform.LookAt(zeroObj.transform);

		// }
	
		
 
	}
}