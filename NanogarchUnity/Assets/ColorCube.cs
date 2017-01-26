using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MeshFilter filter  = GetComponent<MeshFilter>();
		Mesh mesh = filter.sharedMesh;
		List<Color> colors = new List<Color>();
		List<Vector3> verts = new List<Vector3>();
		List<Vector3> normals = new List<Vector3>();
		List<int> faces = new List<int>();
		// Debug.Log("Total tris : "+mesh.triangles.Length);
		// Debug.Log("Total verts: "+mesh.vertices.Length);
		// Dictionary<int,Color> = new Dictionary<int,Color>();
		for(int i=0; i<mesh.triangles.Length; i+=3)
		{
			int ai = mesh.triangles[i];
			int bi = mesh.triangles[i+1];
			int ci = mesh.triangles[i+2];
			Vector3 a = mesh.vertices[ai]; //0->1
			Vector3 b = mesh.vertices[bi]; //1->2
			Vector3 c = mesh.vertices[ci];   //2
			
			verts.Add(a);
			verts.Add(b);
			verts.Add(c);
			faces.Add(i);
			faces.Add(i+1);
			faces.Add(i+2);
			Vector3 n = Vector3.Cross(b-a,c-a).normalized;
			normals.Add(n);
			normals.Add(n);
			normals.Add(n);
			
			float d = Vector3.Dot(n,transform.forward);
			if(d>0)
			{
				colors.Add(Color.red);
				colors.Add(Color.red);
				colors.Add(Color.red);
					
			}
			else if(d<0)
			{
				colors.Add(Color.blue);
				colors.Add(Color.blue);
				colors.Add(Color.blue);
			}
			else
			{
				colors.Add(Color.green);
				colors.Add(Color.white);
				colors.Add(Color.green);
			}
		}
		// Debug.Log("Total colors: "+colors.Count);
		mesh.vertices = verts.ToArray();
		mesh.triangles = faces.ToArray();
		mesh.colors = colors.ToArray();
		mesh.normals = normals.ToArray();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
