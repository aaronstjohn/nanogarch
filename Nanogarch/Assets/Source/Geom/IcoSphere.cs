using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
public class IcoSphere : MeshData
{
	
	private float radius;
	private List<int> dualPolyCentroids;
	public IcoSphere(float radius) : base()
	{
		this.radius = radius;
		dualPolyCentroids = new List<int>();
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

		// SubdivideFaces();
		// ConvertToTruncatedIsocahedron();
	}
	public List<int> GetDualPolyCentroids()
	{
		return dualPolyCentroids;
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
	public MeshData GetPolyData(int ithPoly)
	{
		int centroid = dualPolyCentroids[ithPoly];
		MeshData md = new MeshData();
		TriangleIndices[] trisAroundCentroid = vertFaces[centroid].ToArray();
		
		for(int i= 0; i<trisAroundCentroid.Length;i++)
		{
			TriangleIndices nextTri = trisAroundCentroid[i];
			int a = md.InsertVertex(verts[nextTri.v1]);
			int b = md.InsertVertex(verts[nextTri.v2]);
			int c = md.InsertVertex(verts[nextTri.v3]);
			// Debug.Log("Creating face ("+a+","+b+","+c+")");
			md.InsertFace(new TriangleIndices(a,b,c));
		}
		return md;
	}
	float positiveRad(float rad)
	{
		while (rad <0 )
			rad+= (float)Math.PI *2;
		return rad;
	}
	public int OrderTris(int sharedVertex,int refVertex,TriangleIndices t1,TriangleIndices t2)
	{
		Vector3 shared = verts[sharedVertex];
		Vector3 reference = Vector3.zero;//verts[refVertex];
		Vector3 t1centroid = ComputeFaceCentroid(t1);
		Vector3 t2centroid = ComputeFaceCentroid(t2);
		Vector3 rayToreference = (reference-shared).normalized;
		float t1angle = Vector3.Angle( rayToreference,(t1centroid-shared).normalized);
		float t2angle = Vector3.Angle( rayToreference,(t2centroid-shared).normalized);


		if(t1angle >t2angle)
			return 1;
		if(t2angle > t1angle)
			return -1;
		return 0;

			
	}
	// public TriangleIndices? FindAdjacentTri(TriangleIndices tri, ref  List<TriangleIndices> triList)
	// {
	// 	foreach(TriangleIndices cantidate in triList)
	// 	{
	// 		if(tri.ExclusiveMatchesPair(cantidate))
	// 			return cantidate;
	// 	}
	// 	return null;
	// }
	public void ConvertToTruncatedIsocahedron()
	{
		//Grab a references to the current set of faces 
		Dictionary<int,HashSet<TriangleIndices>> prevVertFaces = vertFaces.Clone();//ObjectCopier.Clone(vertFaces);
		List<TriangleIndices> newFaces = new List<TriangleIndices>();
		CachedIndexedComputation<TriangleIndices,Vector3 > faceCentroidCache = new CachedIndexedComputation<TriangleIndices,Vector3 >(ComputeFaceCentroid,InsertVertex);
		
		//Go through each vertex and build faces for hexes and pentagons using the vertex as the center of the poly
		// and the centroids of the surrounding faces as the vertices 
		Debug.Log("THERE ARE "+prevVertFaces.Count+" Verts being converted to faces ");
		foreach ( KeyValuePair<int,HashSet<TriangleIndices>> kvp in prevVertFaces)
		{
			// TriangleIndices[] triFaces = kvp.Value.ToArray();

			List<TriangleIndices> triFaces = new List<TriangleIndices>(kvp.Value.ToArray());
			TriangleIndices next = triFaces[0];
			triFaces.RemoveAt(0);

			while(triFaces.Count>0)
			{
				TriangleIndices? adjacent = FindAdjacentTri(next,ref triFaces);
				if(!adjacent.HasValue)
					throw new Exception("Can't build polygon face, no adjacent triangle");
				TriangleIndices adjacentTri = adjacent.GetValueOrDefault();

				int a = kvp.Key;
				int b = faceCentroidCache.FetchOrCompute(next);
				int c = faceCentroidCache.FetchOrCompute(adjacentTri); 
				// InsertFace(new TriangleIndices(a, b, c));
				newFaces.Add(new TriangleIndices(a, b, c));
				next=adjacentTri;
				triFaces.Remove(adjacentTri);
			}
			// TriangleIndices[] triFaces = kvp.Value.ToArray();
			
			// Array.Sort(triFaces,(t1,t2)=>OrderTris(kvp.Key,triFaces[0].v1,t1,t2));
			// Vector3 hexCentroid = verts[kvp.Key];
			// dualPolyCentroids.Add(kvp.Key);

			// for(int i=0; i< triFaces.Length; i++)
			// {
			// 	int a = kvp.Key;
			// 	int b = faceCentroidCache.FetchOrCompute(triFaces[i]);
			// 	//Loop around to the first tri in the list
			// 	int c = faceCentroidCache.FetchOrCompute(triFaces[(i+1)%triFaces.Length]); 
			// 	newFaces.Add(new TriangleIndices(a, b, c));
			// 	// InsertFace(new TriangleIndices(a, b, c));
			// }
		}
		Debug.Log("THERE ARE STILL "+prevVertFaces.Count+" Verts being converted to faces ");
		
		//Reset the face list 
		ResetFaces();
		//Insert the newly computed faces 
		InsertFaces(newFaces);

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
	
	// private struct SphericalMesh
	// {
	// 	public List<Vector3> verts;
	// 	public List<TriangleIndices> faces;
	// 	public Dictionary<long, int> midpoints;
	// 	public Dictionary<int,List<TriangleIndices>> hexCentroids;
	// 	public Dictionary<TriangleIndices,int> faceCentroids;

 
	// 	float radius;
	// 	public SphericalMesh(float radius)
	// 	{
	// 		this.radius=radius;
	// 		verts = new List<Vector3>();
	// 		faces = new List<TriangleIndices>();
	// 		midpoints = new Dictionary<long, int>();
	// 		hexCentroids = new Dictionary<int,List<TriangleIndices> >();
	// 		faceCentroids = new Dictionary<TriangleIndices,int>();
	// 		// create 12 vertices of a icosahedron
	// 		float t = (1f + Mathf.Sqrt(5f)) / 2f;
 
	// 		verts.Add(new Vector3(-1f,  t,  0f).normalized * radius);
	// 		verts.Add(new Vector3( 1f,  t,  0f).normalized * radius);
	// 		verts.Add(new Vector3(-1f, -t,  0f).normalized * radius);
	// 		verts.Add(new Vector3( 1f, -t,  0f).normalized * radius);
 
	// 		verts.Add(new Vector3( 0f, -1f,  t).normalized * radius);
	// 		verts.Add(new Vector3( 0f,  1f,  t).normalized * radius);
	// 		verts.Add(new Vector3( 0f, -1f, -t).normalized * radius);
	// 		verts.Add(new Vector3( 0f,  1f, -t).normalized * radius);
 
	// 		verts.Add(new Vector3( t,  0f, -1f).normalized * radius);
	// 		verts.Add(new Vector3( t,  0f,  1f).normalized * radius);
	// 		verts.Add(new Vector3(-t,  0f, -1f).normalized * radius);
	// 		verts.Add(new Vector3(-t,  0f,  1f).normalized * radius);

	// 		// 5 faces around point 0
	// 		faces.Add(new TriangleIndices(0, 11, 5));
	// 		faces.Add(new TriangleIndices(0, 5, 1));
	// 		faces.Add(new TriangleIndices(0, 1, 7));
	// 		faces.Add(new TriangleIndices(0, 7, 10));
	// 		faces.Add(new TriangleIndices(0, 10, 11));
 
	// 		// 5 adjacent faces
	// 		faces.Add(new TriangleIndices(1, 5, 9));
	// 		faces.Add(new TriangleIndices(5, 11, 4));
	// 		faces.Add(new TriangleIndices(11, 10, 2));
	// 		faces.Add(new TriangleIndices(10, 7, 6));
	// 		faces.Add(new TriangleIndices(7, 1, 8));
 
	// 		// 5 faces around point 3
	// 		faces.Add(new TriangleIndices(3, 9, 4));
	// 		faces.Add(new TriangleIndices(3, 4, 2));
	// 		faces.Add(new TriangleIndices(3, 2, 6));
	// 		faces.Add(new TriangleIndices(3, 6, 8));
	// 		faces.Add(new TriangleIndices(3, 8, 9));
 
	// 		// 5 adjacent faces
	// 		faces.Add(new TriangleIndices(4, 9, 5));
	// 		faces.Add(new TriangleIndices(2, 4, 11));
	// 		faces.Add(new TriangleIndices(6, 2, 10));
	// 		faces.Add(new TriangleIndices(8, 6, 7));
	// 		faces.Add(new TriangleIndices(9, 8, 1));
	// 		// foreach( var face in faces)
	// 		// 	face.updateFaceReferences(ref hexCentroids);
	// 		Sqrt3Subdivide();
	// 		GenerateDual();
	// 	}
		
		
		
	// 	public int GetMiddlePoint(int p1, int p2)
	// 	{
	// 		// first check if we have it already
	// 		bool firstIsSmaller = p1 < p2;
	// 		long smallerIndex = firstIsSmaller ? p1 : p2;
	// 		long greaterIndex = firstIsSmaller ? p2 : p1;
	// 		long key = (smallerIndex << 32) + greaterIndex;
	 
	// 		int ret;
	// 		if (midpoints.TryGetValue(key, out ret))
	// 		{
	// 			return ret;
	// 		}
	 
	// 		// not in cache, calculate it
	// 		Vector3 point1 = verts[p1];
	// 		Vector3 point2 = verts[p2];
	// 		Vector3 middle = new Vector3
	// 		(
	// 			(point1.x + point2.x) / 2f,
	// 			(point1.y + point2.y) / 2f,
	// 			(point1.z + point2.z) / 2f
	// 		);
	 
	// 		// add vertex makes sure point is on unit sphere
	// 		int i = verts.Count;
	// 		verts.Add( middle.normalized * radius );
	 
	// 		// store it, return index
	// 		midpoints.Add(key, i);
	 
	// 		return i;
	// 	}
	// 	public TriangleIndices WoundTriangle(int a,int b, int c)
	// 	{
	// 		Vector3 va = verts[a];
	// 		Vector3 vb = verts[b];
	// 		Vector3 vc = verts[c];
			
	// 		Vector3 vab = vb-va;
	// 		Vector3 vac = vc-va;

	// 		Vector3 norm = Vector3.Cross(vab,vac);
	// 		Vector3 centroid = new Vector3
	// 		(
	// 			(va.x + vb.x + vc.x) / 3f,
	// 			(va.y + vb.y + vc.y) / 3f,
	// 			(va.z + vb.z + vc.z) / 3f
	// 		).normalized;
	// 		float dotResult = Vector3.Dot(centroid,norm);
			
	// 		if( dotResult> 0)
	// 		{	
	// 			Debug.Log("POSITIVE FLIP "+dotResult);
	// 			return new TriangleIndices(a,b,c);

	// 		}
	// 		Debug.Log("NEGATIVE FLIP "+dotResult);
	// 		return new TriangleIndices(b,a,c);

	// 	}
	// 	public void CountHexesAndPentagons()
	// 	{
	// 		int hexes =0;
	// 		int pents =0; 
	// 		int unknown =0;
	// 		foreach ( KeyValuePair<int,List<TriangleIndices>> kvp in hexCentroids)
	// 		{
	// 			List<TriangleIndices> tris = kvp.Value;
	// 			if(tris.Count == 6)
	// 				hexes++;
	// 			else if (tris.Count ==5)
	// 				pents++;
	// 			else
	// 				unknown++;
	// 		}
	// 		Debug.Log("There are : "+hexes+" hexagons");
	// 		Debug.Log("There are : "+pents+" pentagons");
	// 		Debug.Log("There are : "+unknown+" other polygons");
	// 		Debug.Log("That should be: "+(hexes*6+pents*5)+" triangles in the dual");
			
				
	// 	}
	// 	public List<Vector3> GetHexCentroids()
	// 	{
	// 		List<Vector3> centroids = new List<Vector3>();
	// 		foreach ( KeyValuePair<int,List<TriangleIndices>> kvp in hexCentroids)
	// 		{
	// 			centroids.Add(verts[kvp.Key]);
	// 		}
	// 		return centroids;
	// 	}
	

	// 	public List<Vector3> GetFaceCentroids()
	// 	{
	// 		List<Vector3> centroids = new List<Vector3>();
	// 		faceCentroids = new Dictionary<TriangleIndices,int>();
	// 		foreach ( KeyValuePair<int,List<TriangleIndices>> kvp in hexCentroids)
	// 		{
	// 			List<TriangleIndices> tris = kvp.Value;
	// 			for( int i=0 ; i < tris.Count ; i++)
	// 			{
	// 				centroids.Add(ComputeFaceCentroid(tris[i]));
	// 			}
	// 		}
	// 		Debug.Log("THere are : "+centroids.Count+" face centroids ");
	// 		return centroids;
	// 	}
	// 	public Vector3 ComputeFaceCentroid(TriangleIndices t)
	// 	{
	// 		Vector3 point1 = verts[t.v1];
	// 		Vector3 point2 = verts[t.v2];
	// 		Vector3 point3 = verts[t.v3];

	// 		Vector3 centroid = new Vector3
	// 		(
	// 			(point1.x + point2.x + point3.x) / 3f,
	// 			(point1.y + point2.y + point3.y) / 3f,
	// 			(point1.z + point2.z + point3.z) / 3f
	// 		);
	// 		return centroid.normalized * radius;
	// 	}
	// 	public int GetFaceCentroid(TriangleIndices t )
	// 	{
	// 		int ret;
	// 		if (this.faceCentroids.TryGetValue(t, out ret))
	// 		{
	// 			Debug.Log("RETURNING CACHED CENTROID "+ret);
				
	// 			Debug.Log("Key was v1: "+t.v1+" v2: "+t.v2+" v3: "+t.v3);
	// 			Debug.Log("There are "+faceCentroids.Count+" Centroids");
	// 			return ret;
	// 		}

	// 		Vector3 centroid = ComputeFaceCentroid(t);
	// 		// add vertex makes sure point is on unit sphere
	// 		int i = verts.Count;
	// 		verts.Add( centroid );
	 
	// 		// store it, return index
	// 		faceCentroids.Add(t, i);
	// 		Debug.Log("RETURNING NEW CENTROID "+i);
	// 		Debug.Log("Key was v1: "+t.v1+" v2: "+t.v2+" v3: "+t.v3);
	// 		Debug.Log("There are "+faceCentroids.Count+" Centroids");
	 		
	// 		return i;
	// 	}
	// 	public int CompareTrisAroundHexCentroid(Vector3 centroid,TriangleIndices referenceFace,TriangleIndices t1,TriangleIndices t2)
	// 	{
	// 		Vector3 referenceFaceCentroid = ComputeFaceCentroid(referenceFace);
	// 		float lhs = Vector3.Dot( (referenceFaceCentroid-centroid).normalized,(ComputeFaceCentroid(t1)-centroid).normalized);
	// 		float rhs = Vector3.Dot((referenceFaceCentroid-centroid).normalized,(ComputeFaceCentroid(t2)-centroid).normalized);
	// 		if (lhs > rhs)
	// 			return 1;
	// 		if (lhs <rhs)
	// 			return -1;
	// 		return 0;
				
	// 	}
	// 	public void GenerateDual()
	// 	{
	// 		List<TriangleIndices> dualFaces = new List<TriangleIndices>();
	// 		this.faceCentroids = new Dictionary<TriangleIndices,int>();
	// 		SphericalMesh sm = this;
	// 		int hexes =0;
	// 		foreach ( KeyValuePair<int,List<TriangleIndices>> kvp in hexCentroids)
	// 		{
	// 			List<TriangleIndices> tris = kvp.Value;
	// 			Vector3 hexCenter = verts[kvp.Key];
	// 			TriangleIndices refTri = tris[0];
	// 			//Sort by
	// 			//List<TriangleIndices>.Sort(tris,(x,y)=> Vector3.Dot(Vector3.right,(ComputeFaceCentroid(x)-hexCenter).normalized)-Vector3.Dot(Vector3.right, (ComputeFaceCentroid(y)-hexCenter).normalized) );
	// 			tris.Sort((x,y)=>  sm.CompareTrisAroundHexCentroid(hexCenter,refTri,x,y));
	// 			for( int i=0 ; i < tris.Count ; i++)
	// 			{
	// 				int a = kvp.Key;
	// 				int b = GetFaceCentroid(tris[i]);
	// 				int c = GetFaceCentroid(tris[(i+1)%tris.Count]); //Loop around to the first tri in the list

	// 				// TriangleIndices tri = WoundTriangle(a,b,c);
	// 				TriangleIndices tri =new TriangleIndices(a,b,c);

	// 				dualFaces.Add(tri);
	// 			}
	// 			hexes++;
	// 			if(hexes >=1)
	// 				break;

	// 		}
	// 		Debug.Log("CREATED : "+dualFaces.Count+"Faces ");
	// 		this.faces = dualFaces;
	// 	}
	// 	public void Sqrt3Subdivide()
	// 	{
	// 		List<TriangleIndices> subFaces = new List<TriangleIndices>();
	// 		hexCentroids = new Dictionary<int,List<TriangleIndices> >();

	// 		foreach (var tri in faces)
	// 		{
	// 			// replace triangle by 4 triangles
	// 			int a = GetMiddlePoint(tri.v1, tri.v2);
	// 			int b = GetMiddlePoint(tri.v2, tri.v3);
	// 			int c = GetMiddlePoint(tri.v3, tri.v1);
	// 			TriangleIndices t1 = new TriangleIndices(tri.v1, a, c);
	// 			TriangleIndices t2 = new TriangleIndices(tri.v2, b, a);
	// 			TriangleIndices t3 = new TriangleIndices(tri.v3, c, b);
	// 			TriangleIndices t4 = new TriangleIndices(a, b, c);

	// 			t1.updateFaceReferences(ref hexCentroids);
	// 			t2.updateFaceReferences(ref hexCentroids);
	// 			t3.updateFaceReferences(ref hexCentroids);
	// 			t4.updateFaceReferences(ref hexCentroids);
				
	// 			subFaces.Add(t1);
	// 			subFaces.Add(t2);
	// 			subFaces.Add(t3);
	// 			subFaces.Add(t4);
	// 		}
	// 		Debug.Log("There are "+hexCentroids.Count+" Hex Centers ");
	// 		this.faces = subFaces;
	// 	}
	
	// 	public Mesh createMesh(string name)
	// 	{
	// 		Mesh mesh = new Mesh();
	// 		mesh.name = name;
	// 		mesh.vertices = verts.ToArray();
	// 		List<int> triList = new List<int>();
	// 		for( int i = 0; i < faces.Count; i++ )
	// 		{
	// 			triList.Add( faces[i].v1 );
	// 			triList.Add( faces[i].v2 );
	// 			triList.Add( faces[i].v3 );
	// 		}
	// 		mesh.triangles = triList.ToArray();

	// 		var nVertices = mesh.vertices;
	// 		Vector2[] UVs = new Vector2[nVertices.Length];
 
	// 		for(var i= 0; i < nVertices.Length; i++){
	// 			var unitVector = nVertices[i].normalized;
	// 			Vector2 ICOuv = new Vector2(0, 0);
	// 			ICOuv.x = (Mathf.Atan2(unitVector.x, unitVector.z) + Mathf.PI) / Mathf.PI / 2;
	// 			ICOuv.y = (Mathf.Acos(unitVector.y) + Mathf.PI) / Mathf.PI - 1;
	// 			UVs[i] = new Vector2(ICOuv.x, ICOuv.y);
	// 		}
 
	// 		mesh.uv = UVs;
 
	// 		Vector3[] normales = new Vector3[ verts.Count];
	// 		for( int i = 0; i < normales.Length; i++ )
	// 			normales[i] = verts[i].normalized;
 
	// 		mesh.normals = normales;
 
	// 		mesh.RecalculateBounds();
	// 		return mesh;
	// 	}

	// }

// public class IcoSphere
// {
// 	public List<Vector3> verts;
// 	public List<TriangleIndices> faces;
// 	public Dictionary<int,TriangleIndices> faceMap;
// 	public float radius;
// 	public IcoSphere(float radius)
// 	{
// 		this.radius=radius;
// 		verts = new List<Vector3>();
// 		faces = new List<TriangleIndices>();
		
// 		float t = (1f + Mathf.Sqrt(5f)) / 2f;

// 		InsertVertex(new Vector3(-1f,  t,  0f).normalized * radius);
// 		InsertVertex(new Vector3( 1f,  t,  0f).normalized * radius);
// 		InsertVertex(new Vector3(-1f, -t,  0f).normalized * radius);
// 		InsertVertex(new Vector3( 1f, -t,  0f).normalized * radius);

// 		InsertVertex(new Vector3( 0f, -1f,  t).normalized * radius);
// 		InsertVertex(new Vector3( 0f,  1f,  t).normalized * radius);
// 		InsertVertex(new Vector3( 0f, -1f, -t).normalized * radius);
// 		InsertVertex(new Vector3( 0f,  1f, -t).normalized * radius);

// 		InsertVertex(new Vector3( t,  0f, -1f).normalized * radius);
// 		InsertVertex(new Vector3( t,  0f,  1f).normalized * radius);
// 		InsertVertex(new Vector3(-t,  0f, -1f).normalized * radius);
// 		InsertVertex(new Vector3(-t,  0f,  1f).normalized * radius);

// 		// 5 faces around point 0
// 		InsertFace((new TriangleIndices(0, 11, 5));
// 		InsertFace((new TriangleIndices(0, 5, 1));
// 		InsertFace((new TriangleIndices(0, 1, 7));
// 		InsertFace((new TriangleIndices(0, 7, 10));
// 		InsertFace((new TriangleIndices(0, 10, 11));

// 		// 5 adjacent faces
// 		InsertFace((new TriangleIndices(1, 5, 9));
// 		InsertFace((new TriangleIndices(5, 11, 4));
// 		InsertFace((new TriangleIndices(11, 10, 2));
// 		InsertFace((new TriangleIndices(10, 7, 6));
// 		InsertFace((new TriangleIndices(7, 1, 8));

// 		// 5 faces around point 3
// 		InsertFace((new TriangleIndices(3, 9, 4));
// 		InsertFace((new TriangleIndices(3, 4, 2));
// 		InsertFace((new TriangleIndices(3, 2, 6));
// 		InsertFace((new TriangleIndices(3, 6, 8));
// 		InsertFace((new TriangleIndices(3, 8, 9));

// 		// 5 adjacent faces
// 		InsertFace((new TriangleIndices(4, 9, 5));
// 		InsertFace((new TriangleIndices(2, 4, 11));
// 		InsertFace((new TriangleIndices(6, 2, 10));
// 		InsertFace((new TriangleIndices(8, 6, 7));
// 		InsertFace((new TriangleIndices(9, 8, 1));
// 		// foreach( var face in faces)
// 		// 	face.updateFaceReferences(ref hexCentroids);
// 		Sqrt3Subdivide();
// 	}
// 	public void Sqrt3Subdivide()
// 	{
// 		List<TriangleIndices> subFaces = new List<TriangleIndices>();
		
// 		foreach (var tri in faces)
// 		{
// 			// replace triangle by 4 triangles
// 			int a = GetMiddlePoint(tri.v1, tri.v2);
// 			int b = GetMiddlePoint(tri.v2, tri.v3);
// 			int c = GetMiddlePoint(tri.v3, tri.v1);
// 			TriangleIndices t1 = new TriangleIndices(tri.v1, a, c);
// 			TriangleIndices t2 = new TriangleIndices(tri.v2, b, a);
// 			TriangleIndices t3 = new TriangleIndices(tri.v3, c, b);
// 			TriangleIndices t4 = new TriangleIndices(a, b, c);

// 			// t1.updateFaceReferences(ref hexCentroids);
// 			// t2.updateFaceReferences(ref hexCentroids);
// 			// t3.updateFaceReferences(ref hexCentroids);
// 			// t4.updateFaceReferences(ref hexCentroids);
			
// 			subInsertFace((t1);
// 			subInsertFace((t2);
// 			subInsertFace((t3);
// 			subInsertFace((t4);
// 		}
// 		Debug.Log("There are "+hexCentroids.Count+" Hex Centers ");
// 		this.faces = subFaces;
// 	}
// }