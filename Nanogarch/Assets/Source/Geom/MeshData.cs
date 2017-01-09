using System.Collections.Generic;
using UnityEngine;

using System.Linq;
public class MeshData
{
	protected List<Vector3> verts;
	protected List<TriangleIndices> faces;
	protected Dictionary<int,HashSet<TriangleIndices>> vertFaces;
	public MeshData()
	{
		verts = new List<Vector3>();
		faces = new List<TriangleIndices>();
		vertFaces = new Dictionary<int,HashSet<TriangleIndices>>();
	}

	public List<Vector3> GetVertices(){return verts;}
	public List<TriangleIndices> GetFaces() {return faces;}
	public TriangleIndices[] GetFacesAroundVertex(int vert)
	{
		return vertFaces[vert].ToArray();
		
	}
	protected int InsertVertex(Vector3 vert )
	{
		int i = verts.Count;
		verts.Add(vert);
		return i;
	}
	public Vector3 GetVertex(int index)
	{
		return verts[index];
	}
	protected void UpdateVertFaceMembership(TriangleIndices tri)
	{
		for(int i=0;i<tri.sorted.Length;i++)
		{
			int vert = tri.sorted[i];
			if(!vertFaces.ContainsKey(vert))
			{
				vertFaces.Add(vert,new HashSet<TriangleIndices>());
			}
			vertFaces[vert].Add(tri);
		}
		
	}
	protected void InsertFaces(List<TriangleIndices> tris)
	{
		foreach( TriangleIndices tri in tris)
			InsertFace(tri);
	}
	protected int InsertFace(TriangleIndices tri)
	{
		int i = faces.Count;
		faces.Add(tri);
		UpdateVertFaceMembership(tri);
		return i;
	}
	protected void ResetFaces()
	{
		faces = new List<TriangleIndices>();
		vertFaces = new Dictionary<int,HashSet<TriangleIndices>>();
	}
	virtual protected Vector3 ComputeMiddlePoint(VertexPair p)
	{
		Vector3 point1 = GetVertex(p.v1);
		Vector3 point2 = GetVertex(p.v2);
		Vector3 midpoint= new Vector3
		(
			(point1.x + point2.x) / 2f,
			(point1.y + point2.y) / 2f,
			(point1.z + point2.z) / 2f
		);
		return midpoint;
	}
	virtual protected Vector3 ComputeFaceCentroid(TriangleIndices tri)
	{
		Vector3 point1 = GetVertex(tri.v1);
		Vector3 point2 = GetVertex(tri.v2);
		Vector3 point3 = GetVertex(tri.v3);

		Vector3 centroid = new Vector3
		(
			(point1.x + point2.x + point3.x) / 3f,
			(point1.y + point2.y + point3.y) / 3f,
			(point1.z + point2.z + point3.z) / 3f
		);
		return centroid;
	}
}