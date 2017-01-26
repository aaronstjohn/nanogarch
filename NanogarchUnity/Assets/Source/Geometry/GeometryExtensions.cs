using System.Collections.Generic;
using UnityEngine;
public static class GeometryExtensions
{
	
	public static Mesh CreateMesh(this TruncatedIcosahedron truncatedIco,string name)
	{
		MeshData meshData = truncatedIco.GetDuplicateVertsPerTri();
		Mesh mesh = new Mesh();
		mesh.name = name;
		List<Vector3> verts = meshData.GetVertices();
		List<TriangleIndices> faces = meshData.GetFaces();
		mesh.vertices = verts.ToArray();
		List<int> triList = new List<int>();
		List<Color> colors = new List<Color>();
		// Random random = new Random();
		for( int i = 0; i < faces.Count; i++ )
		{
			// Color c = Random.value<0.5f? Color.white: Color.red;
			Color c = Color.white;
			triList.Add( faces[i].v1 );
			triList.Add( faces[i].v2 );
			triList.Add( faces[i].v3 );
			colors.Add(c);
			colors.Add(c);
			colors.Add(c);
		}
		mesh.triangles = triList.ToArray();
		mesh.colors = colors.ToArray();
		var nVertices = mesh.vertices;
		Vector2[] UVs = new Vector2[nVertices.Length];
		
		for( int i = 0; i < faces.Count; i++ )
		{
			UVs[faces[i].v1 ] = new Vector2(27.0f/256.0f,186.0f/256.0f);
			UVs[faces[i].v2 ] = new Vector2(230.0f/256.0f,186.0f/256.0f);
			UVs[faces[i].v3 ] = new Vector2(127.0f/256.0f,10.0f/256.0f);
			
		}
		
		mesh.uv = UVs;

		Vector3[] normales = new Vector3[ verts.Count];
		for( int i = 0; i < normales.Length; i++ )
			normales[i] = verts[i].normalized;

		mesh.normals = normales;

		mesh.RecalculateBounds();
		return mesh;
	
	}
	public static Vector3 Clone(this Vector3 v)
	{
		return new Vector3(v.x,v.y,v.z);
	}
	public static List<Vector3> Clone(this List<Vector3> verts )
	{
		var newVerts = new List<Vector3>();
		foreach(Vector3 v in verts)
			newVerts.Add(new Vector3(v.x,v.y,v.z));
		return newVerts;
	}
	public static List<TriangleIndices> Clone(this List<TriangleIndices> tris )
	{
		var newList = new List<TriangleIndices>();
		foreach(TriangleIndices t in tris)
			newList.Add(new TriangleIndices(t.v1,t.v2,t.v3));
		return newList;

	}
	public static Dictionary<int,HashSet<TriangleIndices> > Clone(this Dictionary<int,HashSet<TriangleIndices> > vertTriMap )
	{
		var newVertTriMap = new Dictionary<int,HashSet<TriangleIndices> >();
		foreach ( KeyValuePair<int,HashSet<TriangleIndices>> kvp in vertTriMap)
		{
			int vert = kvp.Key;
			HashSet<TriangleIndices> tris = kvp.Value;
			var newTris = new HashSet<TriangleIndices>();
			foreach(TriangleIndices t in tris)
				newTris.Add(new TriangleIndices(t.v1,t.v2,t.v3));

			newVertTriMap.Add(vert,newTris);
		}
		return newVertTriMap;
		

	}
}