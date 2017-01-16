using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
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
	public Dictionary<int,HashSet<TriangleIndices>> GetVertFaces()
	{
		return vertFaces;
	}
	public List<Vector3> GetVertices(){return verts;}
	public Dictionary<int,HashSet<TriangleIndices>>  GetFacesAroundVertices(){return vertFaces.Clone();}
	public List<TriangleIndices> GetFaces() {return faces;}
	public TriangleIndices[] GetFacesAroundVertex(int vert)
	{
		return vertFaces[vert].ToArray();
		
	}
	//Creates a copy of this mesh with an instance of each vertex per triangle.
	public MeshData GetDuplicateVertsPerTri()
	{
		MeshData newMd = new MeshData();

		foreach(TriangleIndices tri in faces)
		{
			Vector3 p1 = GetVertex(tri.v1).Clone();
			Vector3 p2 = GetVertex(tri.v2).Clone();
			Vector3 p3 = GetVertex(tri.v3).Clone();
			int a = newMd.InsertVertex(p1);
			int b = newMd.InsertVertex(p2);
			int c = newMd.InsertVertex(p3);
			newMd.InsertFace(new TriangleIndices(a,b,c));
				
		}
		return newMd;
	}
	public int InsertVertex(Vector3 vert )
	{
		//DEBUG ONLY
		// if(verts.Contains(vert))
		// {
		// 	throw new Exception("Don't add copies of verts to MeshData!");
		
		// }
		
		int i = verts.Count;
		verts.Add(vert);
		return i;
		
		
	}

	public void InsertVertices(List<Vector3> verts)
	{
		foreach(Vector3 v in verts)
			InsertVertex(v);

	}
	public void InsertFaces(List<TriangleIndices> tris)
	{
		foreach( TriangleIndices tri in tris)
			InsertFace(tri);
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
	
	public double determinant(double[,] m) {
		return
		 m[0,3] * m[1,2] * m[2,1] * m[3,0] - m[0,2] * m[1,3] * m[2,1] * m[3,0] -
		 m[0,3] * m[1,1] * m[2,2] * m[3,0] + m[0,1] * m[1,3] * m[2,2] * m[3,0] +
		 m[0,2] * m[1,1] * m[2,3] * m[3,0] - m[0,1] * m[1,2] * m[2,3] * m[3,0] -
		 m[0,3] * m[1,2] * m[2,0] * m[3,1] + m[0,2] * m[1,3] * m[2,0] * m[3,1] +
		 m[0,3] * m[1,0] * m[2,2] * m[3,1] - m[0,0] * m[1,3] * m[2,2] * m[3,1] -
		 m[0,2] * m[1,0] * m[2,3] * m[3,1] + m[0,0] * m[1,2] * m[2,3] * m[3,1] +
		 m[0,3] * m[1,1] * m[2,0] * m[3,2] - m[0,1] * m[1,3] * m[2,0] * m[3,2] -
		 m[0,3] * m[1,0] * m[2,1] * m[3,2] + m[0,0] * m[1,3] * m[2,1] * m[3,2] +
		 m[0,1] * m[1,0] * m[2,3] * m[3,2] - m[0,0] * m[1,1] * m[2,3] * m[3,2] -
		 m[0,2] * m[1,1] * m[2,0] * m[3,3] + m[0,1] * m[1,2] * m[2,0] * m[3,3] +
		 m[0,2] * m[1,0] * m[2,1] * m[3,3] - m[0,0] * m[1,2] * m[2,1] * m[3,3] -
		 m[0,1] * m[1,0] * m[2,2] * m[3,3] + m[0,0] * m[1,1] * m[2,2] * m[3,3];
	}
	public double TriangleFacing(TriangleIndices tri)
	{
		Vector3 p1 = GetVertex(tri.v1);
		Vector3 p2 = GetVertex(tri.v2);
		Vector3 p3 = GetVertex(tri.v3);
		double[,] m = new [,] {{ 	p1.x, p1.y, p1.z,1.0 },
		               		   { 	p2.x, p2.y, p2.z,1.0 },
		               		   { 	p3.x, p3.y, p3.z,1.0 },
		               		   { 	0.0,  0.0,  0.0, 1.0 }};
        return determinant(m);
	}
	public TriangleIndices? PositiveFacingPermutation(TriangleIndices tri)
	{
		foreach(TriangleIndices perm in tri.Permutations())
		{
			double facing = TriangleFacing(perm);
			// // Debug.Log("FACING OF "+perm+" is "+facing);
			// Vector3 p1 = GetVertex(perm.v1);
			// Vector3 p2 = GetVertex(perm.v2);
			// Vector3 p3 = GetVertex(perm.v3);
			// Debug.Log("P1 x:"+p1.x+" y: "+p1.y+" z: "+p1.z);
			// Debug.Log("P2 x:"+p2.x+" y: "+p2.y+" z: "+p2.z);
			// Debug.Log("P3 x:"+p3.x+" y: "+p3.y+" z: "+p3.z);
			
			if( facing >0)
				return perm;
		}
		return null;
	}
	
	public int InsertFace(TriangleIndices tri)
	{
		Vector3 p1 = GetVertex(tri.v1);
		Vector3 p2 = GetVertex(tri.v2);
		Vector3 p3 = GetVertex(tri.v3);
		if(p1==p2 || p1==p3 || p2==p3)
		{
			throw new Exception("All vertices of a triangle must be different");
		}
		TriangleIndices? positiveFacing = PositiveFacingPermutation(tri);
		if(!positiveFacing.HasValue)
		{	
			throw new Exception("Can't add face, no positive winding for tri : "+ tri);
		}
		int i = faces.Count;
		faces.Add(positiveFacing.GetValueOrDefault());
		UpdateVertFaceMembership(positiveFacing.GetValueOrDefault());
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
	virtual public Vector3 ComputeFaceCentroid(TriangleIndices tri)
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
	public static List<TriangleIndices>  OrderByTriangleAdjacency( List<TriangleIndices> triFaces)
	{
		List<TriangleIndices> workList = triFaces.Clone();
		List<TriangleIndices> outputList = new List<TriangleIndices>();

		TriangleIndices nextTri = workList[0];
		workList.Remove(nextTri);
		// outputList.Add(nextTri);
		while(workList.Count > 0)
		{
			TriangleIndices? adjacent = FindAdjacentTri(nextTri,ref workList);
			if(!adjacent.HasValue)
				throw new Exception("Can't build polygon face, no adjacent triangle");
			TriangleIndices adjacentTri = adjacent.GetValueOrDefault();
			workList.Remove(adjacentTri);
			outputList.Add(nextTri);
			// Debug.Log(string.Format("{0} <==> {1}",nextTri,adjacentTri));
			nextTri=adjacentTri;
			
		}
		outputList.Add(nextTri);
		return outputList;
	}
	public static TriangleIndices? FindAdjacentTri(TriangleIndices tri, ref  List<TriangleIndices> triList)
	{
		foreach(TriangleIndices cantidate in triList)
		{
			if(tri.ExclusiveMatchesPair(cantidate))
				return cantidate;
		}
		return null;
	}
}