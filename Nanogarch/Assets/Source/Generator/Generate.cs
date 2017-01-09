
using System.Collections.Generic;
using UnityEngine;

public static class Generate
{
	public static Mesh CreateMesh(MeshData meshData,string name)
	{
		Mesh mesh = new Mesh();
		mesh.name = name;
		List<Vector3> verts = meshData.GetVertices();
		List<TriangleIndices> faces = meshData.GetFaces();
		mesh.vertices = verts.ToArray();
		List<int> triList = new List<int>();
		for( int i = 0; i < faces.Count; i++ )
		{
			triList.Add( faces[i].v1 );
			triList.Add( faces[i].v2 );
			triList.Add( faces[i].v3 );
		}
		mesh.triangles = triList.ToArray();

		var nVertices = mesh.vertices;
		Vector2[] UVs = new Vector2[nVertices.Length];

		for(var i= 0; i < nVertices.Length; i++){
			var unitVector = nVertices[i].normalized;
			Vector2 ICOuv = new Vector2(0, 0);
			ICOuv.x = (Mathf.Atan2(unitVector.x, unitVector.z) + Mathf.PI) / Mathf.PI / 2;
			ICOuv.y = (Mathf.Acos(unitVector.y) + Mathf.PI) / Mathf.PI - 1;
			UVs[i] = new Vector2(ICOuv.x, ICOuv.y);
		}

		mesh.uv = UVs;

		Vector3[] normales = new Vector3[ verts.Count];
		for( int i = 0; i < normales.Length; i++ )
			normales[i] = verts[i].normalized;

		mesh.normals = normales;

		mesh.RecalculateBounds();
		return mesh;
	}
}