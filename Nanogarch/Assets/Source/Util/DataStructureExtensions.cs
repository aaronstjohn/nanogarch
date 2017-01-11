using System.Collections.Generic;
using UnityEngine;
public static class DataStructureExtensions
{
	public static Vector3 Clone(this Vector3 v)
	{
		return new Vector3(v.x,v.y,v.z);
	}
	public static List<Vector3> Clone(this List<Vector3> verts )
	{
		List<Vector3> newVerts = new List<Vector3>();
		foreach(Vector3 v in verts)
			newVerts.Add(new Vector3(v.x,v.y,v.z));
		return newVerts;
	}
	public static List<TriangleIndices> Clone(this List<TriangleIndices> tris )
	{
		List<TriangleIndices> newList = new List<TriangleIndices>();
		foreach(TriangleIndices t in tris)
			newList.Add(new TriangleIndices(t.v1,t.v2,t.v3));
		return newList;

	}
	public static Dictionary<int,HashSet<TriangleIndices> > Clone(this Dictionary<int,HashSet<TriangleIndices> > vertTriMap )
	{
		Dictionary<int,HashSet<TriangleIndices> > newVertTriMap = new Dictionary<int,HashSet<TriangleIndices> >();
		foreach ( KeyValuePair<int,HashSet<TriangleIndices>> kvp in vertTriMap)
		{
			int vert = kvp.Key;
			HashSet<TriangleIndices> tris = kvp.Value;
			HashSet<TriangleIndices> newTris = new HashSet<TriangleIndices>();
			foreach(TriangleIndices t in tris)
				newTris.Add(new TriangleIndices(t.v1,t.v2,t.v3));

			newVertTriMap.Add(vert,newTris);
		}
		return newVertTriMap;
		

	}
}
