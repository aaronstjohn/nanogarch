using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
public class Icosahedron : MeshData
{
	float radius;
	public Icosahedron(float radius,int subdivisions):base()
	{
		this.radius = radius; 
		float t = (1f + Mathf.Sqrt(5f)) / 2f;
		InsertVertex(new Vector3(-1f,  t,  0f).normalized * radius);
		InsertVertex(new Vector3( 1f,  t,  0f).normalized * radius);
		InsertVertex(new Vector3(-1f, -t,  0f).normalized * radius);
		InsertVertex(new Vector3( 1f, -t,  0f).normalized * radius);

		InsertVertex(new Vector3( 0f, -1f,  t).normalized * radius);
		InsertVertex(new Vector3( 0f,  1f,  t).normalized * radius);
		InsertVertex(new Vector3( 0f, -1f, -t).normalized * radius);
		InsertVertex(new Vector3( 0f,  1f, -t).normalized * radius);

		InsertVertex(new Vector3( t,  0f, -1f).normalized * radius);
		InsertVertex(new Vector3( t,  0f,  1f).normalized * radius);
		InsertVertex(new Vector3(-t,  0f, -1f).normalized * radius);
		InsertVertex(new Vector3(-t,  0f,  1f).normalized * radius);

		// 5 faces around point 0
		InsertFace(new TriangleIndices(0, 11, 5));
		InsertFace(new TriangleIndices(0, 5, 1));
		InsertFace(new TriangleIndices(0, 1, 7));
		InsertFace(new TriangleIndices(0, 7, 10));
		InsertFace(new TriangleIndices(0, 10, 11));

		// 5 adjacent faces
		InsertFace(new TriangleIndices(1, 5, 9));
		InsertFace(new TriangleIndices(5, 11, 4));
		InsertFace(new TriangleIndices(11, 10, 2));
		InsertFace(new TriangleIndices(10, 7, 6));
		InsertFace(new TriangleIndices(7, 1, 8));

		// 5 faces around point 3
		InsertFace(new TriangleIndices(3, 9, 4));
		InsertFace(new TriangleIndices(3, 4, 2));
		InsertFace(new TriangleIndices(3, 2, 6));
		InsertFace(new TriangleIndices(3, 6, 8));
		InsertFace(new TriangleIndices(3, 8, 9));

		// 5 adjacent faces
		InsertFace(new TriangleIndices(4, 9, 5));
		InsertFace(new TriangleIndices(2, 4, 11));
		InsertFace(new TriangleIndices(6, 2, 10));
		InsertFace(new TriangleIndices(8, 6, 7));
		InsertFace(new TriangleIndices(9, 8, 1));
		for(int i= 0; i<subdivisions;i++)
			SubdivideFaces();

	}
	public float GetRadius()
	{
		return radius;
	}
	public void SubdivideFaces()
	{
		//Grab a references to the current set of faces 
		List<TriangleIndices> prevFaces = faces.Clone() ;//ObjectCopier.Clone(faces);

		CachedIndexedComputation<VertexPair,Vector3 > midpointCache = new CachedIndexedComputation<VertexPair,Vector3 >(ComputeMiddlePoint,InsertVertex);

		ResetFaces();
		// Rebuild the face list by subdividing each triangle into 4 new triangles.
		foreach (var tri in prevFaces)
		{
			int a = midpointCache.FetchOrCompute( new VertexPair(tri.v1, tri.v2));
			int b = midpointCache.FetchOrCompute( new VertexPair(tri.v2, tri.v3));
			int c = midpointCache.FetchOrCompute( new VertexPair(tri.v3, tri.v1));
			InsertFace(new TriangleIndices(tri.v1, a, c));
			InsertFace(new TriangleIndices(tri.v2, b, a));
			InsertFace(new TriangleIndices(tri.v3, c, b));
			InsertFace(new TriangleIndices(a, b, c));
		}
		
	}
	override protected Vector3 ComputeMiddlePoint(VertexPair p)
	{
		return base.ComputeMiddlePoint(p).normalized*radius;
	}
	override public Vector3 ComputeFaceCentroid(TriangleIndices tri)
	{
		return base.ComputeFaceCentroid(tri).normalized*radius;
	}
}
public class TruncatedIcosahedron : MeshData
{
	List<RadialPolyTris> polys;
	Dictionary<int,HashSet<int>> neighborMap;
	public TruncatedIcosahedron(float radius,int subdivisions)
	{
		Icosahedron icosahedron = new Icosahedron(radius, subdivisions);
		neighborMap = new Dictionary<int,HashSet<int>>();

		polys = new List<RadialPolyTris>();
		// int count = 0;
		Dictionary<int,HashSet<TriangleIndices>> prevVertFaces = icosahedron.GetFacesAroundVertices();
		//This will compute the face centroids relative to the isoc's faces but insert the new vertices in the local MeshData and return the ids for the local mesh
		CachedIndexedComputation<TriangleIndices,Vector3 > faceCentroidCache = new CachedIndexedComputation<TriangleIndices,Vector3 >(icosahedron.ComputeFaceCentroid,InsertVertex);
		// Debug.Log(string.Format("Before Truncation there are {0} Polys ",prevVertFaces.Count));
	
		foreach ( KeyValuePair<int,HashSet<TriangleIndices>> kvp in prevVertFaces)
		{
			List<TriangleIndices> icoTriFaces = OrderByTriangleAdjacency(new List<TriangleIndices>(kvp.Value.ToArray()));
			List<TriangleIndices> truncatedIcoFaces = new List<TriangleIndices>();
			//Creates a vertex representing the centroid of the poly that we are creating to replace this vertex 
			int centroidIdx = InsertVertex(icosahedron.GetVertex(kvp.Key).Clone());
			// HashSet<int> neighbors = new HashSet<int>();
			for(int i=0; i<icoTriFaces.Count; i++)
			{
				int b = faceCentroidCache.FetchOrCompute(icoTriFaces[i]);
				int c = faceCentroidCache.FetchOrCompute(icoTriFaces[(i+1)%icoTriFaces.Count]);
				// neighbors.Add(centroidIdx);
				// neighbors.Add(b);
				// neighbors.Add(c);
				truncatedIcoFaces.Add(new TriangleIndices(centroidIdx,b,c));
				
			}

			InsertFaces(truncatedIcoFaces);
			foreach(var f in truncatedIcoFaces)
			{
				foreach(int u in f.sorted)
				{
					if(!neighborMap.ContainsKey(u))
						neighborMap.Add(u,new HashSet<int>());
					foreach(int v in f.sorted)
					{
						if(v!=u)
							neighborMap[u].Add(v);
					}
						
				}
			}
			// neighborMap.Add(centroidIdx,neighbors.ToArray());
			polys.Add(new RadialPolyTris(centroidIdx,truncatedIcoFaces));
			// Debug.Log(string.Format("Inserted {0} faces around vertex ",truncatedIcoFaces.Count));
			
		}
		// Debug.Log(string.Format("Generated {0} Polys ",polys.Count));
		// Debug.Log(string.Format("Verts with faces count: {0} ",vertFaces.Count));
	}
	public int[] GetNeighbors(int polyIndex)
	{
		return neighborMap[polyIndex].ToArray();
	}
	public List<RadialPolyTris> GetRadialPolys()
	{
		return polys;
	}
	public Vector3 GetPolyCentroid(RadialPolyTris poly)
	{
		return verts[poly.centroid];
	}
	public int NearestPoly(Vector3 point)
	{
		Vector3 bestPoly = verts[0];
		int bestPolyIndex = 0;
		float smallestAngle = Mathf.Acos(Vector3.Dot(bestPoly.normalized,point.normalized));
		// PolyCentroidAngle(bestPoly,point);

		// foreach(RadialPolyTris poly in polys)
		for(int i=0; i<verts.Count;i++)
		{
			Vector3 poly = verts[i];
			float newAngle = Mathf.Acos(Vector3.Dot(poly.normalized,point.normalized));
			if(newAngle < smallestAngle)
			{
				smallestAngle = newAngle;
				bestPoly = poly;
				bestPolyIndex = i;
			}
		}
		return bestPolyIndex;
	}
	// public int NearestPoly(Vector3 point)
	// {
	// 	RadialPolyTris bestPoly = polys[0];
	// 	int bestPolyIndex = 0;
	// 	float smallestAngle = PolyCentroidAngle(bestPoly,point);

	// 	// foreach(RadialPolyTris poly in polys)
	// 	for(int i=0; i<polys.Count;i++)
	// 	{
	// 		RadialPolyTris poly = polys[i];
	// 		float newAngle = PolyCentroidAngle(poly,point);
	// 		if(newAngle < smallestAngle)
	// 		{
	// 			smallestAngle = newAngle;
	// 			bestPoly = poly;
	// 			bestPolyIndex = i;
	// 		}
	// 	}
	// 	return bestPolyIndex;
	// }
	public float PolyCentroidAngle(RadialPolyTris poly, Vector3 point)
	{
		Vector3 centroid = verts[poly.centroid];
		return Mathf.Acos(Vector3.Dot(centroid.normalized,point.normalized));
	}
}