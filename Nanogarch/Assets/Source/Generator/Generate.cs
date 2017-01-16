
using System.Collections.Generic;
using UnityEngine;

public static class Generate
{
	public static MeshData CreateMeshData(Mesh mesh)
	{
		List<Vector3> vertices = new List<Vector3>(mesh.vertices);
		List<TriangleIndices> tris = new List<TriangleIndices>();

		int[] triangles = mesh.triangles;
		for(int i=0; i<triangles.Length; i+=3)
		{
			tris.Add(new TriangleIndices(triangles[i],triangles[i+1],triangles[i+2]));
		}
		MeshData md = new MeshData();
		md.InsertVertices(vertices);
		md.InsertFaces(tris);
		return md;
	}
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
		// x:27, y:186
		// x:230 y:186
		// x:127 y:10
		for( int i = 0; i < faces.Count; i++ )
		{
			UVs[faces[i].v1 ] = new Vector2(27.0f/256.0f,186.0f/256.0f);
			UVs[faces[i].v2 ] = new Vector2(230.0f/256.0f,186.0f/256.0f);
			UVs[faces[i].v3 ] = new Vector2(127.0f/256.0f,10.0f/256.0f);
			
		}
		// for(var i= 0; i < nVertices.Length; i++){
		// 	var unitVector = nVertices[i].normalized;
		// 	Vector2 ICOuv = new Vector2(0, 0);
		// 	ICOuv.x = (Mathf.Atan2(unitVector.x, unitVector.z) + Mathf.PI) / Mathf.PI / 2;
		// 	ICOuv.y = (Mathf.Acos(unitVector.y) + Mathf.PI) / Mathf.PI - 1;
		// 	UVs[i] = new Vector2(ICOuv.x, ICOuv.y);
		// }

		mesh.uv = UVs;

		Vector3[] normales = new Vector3[ verts.Count];
		for( int i = 0; i < normales.Length; i++ )
			normales[i] = verts[i].normalized;

		mesh.normals = normales;

		mesh.RecalculateBounds();
		return mesh;
	}
}