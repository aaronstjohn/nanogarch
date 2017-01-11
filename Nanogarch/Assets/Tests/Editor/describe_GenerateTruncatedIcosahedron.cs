using NSpec;
using UnityEngine;
using System.Collections.Generic;
using System;
// using FluentAssertions;
class describe_GenerateTruncatedIcosahedron : nspec {

	IcoSphere icoSphere;
	void before_each()
    {
    	icoSphere = new IcoSphere(0.5f);
    }
    void it_there_are_12_vertices_after_initialization()
    {
    	List<Vector3> verts = icoSphere.GetVertices();
    	verts.Count.should_be(12);
    	
    }
    void it_each_vertex_has_5_faces_around_it_before_subdivision()
    {
    	List<Vector3> verts = icoSphere.GetVertices();
    	for(int i=0; i < verts.Count;i++)
    	{
    		TriangleIndices[] faces = icoSphere.GetFacesAroundVertex(i);
    		faces.Length.should_be(5);
    	}
    }
    void it_there_are_36_verticies_after_first_truncation()
    {
    	List<Vector3> verts = icoSphere.GetVertices();
    	icoSphere.ConvertToTruncatedIsocahedron();
    	verts.Count.should_be(12+20);
    }
    // int [,] facesAround = new int[,]
    // // 5 faces around point 0
    //     InsertFace(new TriangleIndices(0, 11, 5));
    //     InsertFace(new TriangleIndices(0, 5, 1));
    //     InsertFace(new TriangleIndices(0, 1, 7));
    //     InsertFace(new TriangleIndices(0, 7, 10));
    //     InsertFace(new TriangleIndices(0, 10, 11));

    //     // 5 adjacent faces
    //     InsertFace(new TriangleIndices(1, 5, 9));
    //     InsertFace(new TriangleIndices(5, 11, 4));
    //     InsertFace(new TriangleIndices(11, 10, 2));
    //     InsertFace(new TriangleIndices(10, 7, 6));
    //     InsertFace(new TriangleIndices(7, 1, 8));

    //     // 5 faces around point 3
    //     InsertFace(new TriangleIndices(3, 9, 4));
    //     InsertFace(new TriangleIndices(3, 4, 2));
    //     InsertFace(new TriangleIndices(3, 2, 6));
    //     InsertFace(new TriangleIndices(3, 6, 8));
    //     InsertFace(new TriangleIndices(3, 8, 9));

    //     // 5 adjacent faces
    //     InsertFace(new TriangleIndices(4, 9, 5));
    //     InsertFace(new TriangleIndices(2, 4, 11));
    //     InsertFace(new TriangleIndices(6, 2, 10));
    //     InsertFace(new TriangleIndices(8, 6, 7));
    //     InsertFace(new TriangleIndices(9, 8, 1));

    void it_the_faces_around_each_vertex_are_expected()
    {
       // int[] testVerts = new int[] {0,5,3};
       List<Vector3> verts = icoSphere.GetVertices();
       for(int i=0; i<verts.Count;i++)
       {
            TriangleIndices[] faces = icoSphere.GetFacesAroundVertex(i);
            Console.WriteLine("FACES AROUND: "+i+" Are: ");
            for(int j=0 ; j<faces.Length; j++)
            {
                Console.WriteLine(" "+faces[j].ToString());
            }
       }
    }
    void it_there_are_12_pentagons_after_first_truncation()
    {
    	// List<Vector3> verts = icoSphere.GetVertices();
    	icoSphere.ConvertToTruncatedIsocahedron();
    	List<int> polyCentroids = icoSphere.GetDualPolyCentroids();
    	polyCentroids.Count.should_be(12);
    	for(int i=0; i < polyCentroids.Count;i++)
    	{
    		TriangleIndices[] faces = icoSphere.GetFacesAroundVertex(i);

    		faces.Length.should_be(5);
    	}
    }
    void it_total_tris_after_first_truncation_is_60()
    {
    	icoSphere.ConvertToTruncatedIsocahedron();
    	List<TriangleIndices> faces = icoSphere.GetFaces();
    	faces.Count.should_be(60);
    }
    void it_faces_and_vertices_all_different()
    {
    	icoSphere.ConvertToTruncatedIsocahedron();
    	List<Vector3> verts = icoSphere.GetVertices();
    	List<TriangleIndices> faces = icoSphere.GetFaces();

    	HashSet<Vector3> vertSet = new HashSet<Vector3>(verts);
    	HashSet<TriangleIndices> faceSet = new HashSet<TriangleIndices>(faces);
    	
    	(vertSet.Count ==verts.Count && faceSet.Count == faces.Count).should_be_true();
    		
    }
    // void it_faces_around_each_vertex()
    // {

    // }
    // void it_hex_faces_are_expected()
    // {
    //     // icoSphere.ConvertToTruncatedIsocahedron();
    //     // // icoSphere.FlipAllFacesOutward();
    //     // MeshData singlePoly = icoSphere.GetPolyData(0);
    //     // singlePoly.FlipAllFacesOutward();
    //     // List<Vector3> verts = singlePoly.GetVertices();
    //     // List<TriangleIndices> faces = singlePoly.GetFaces();
    //     // foreach(var tri in faces)
    //     // {
    //     //     Vector3 a = verts[tri.v1];
    //     //     Vector3 b = verts[tri.v2];
    //     //     Vector3 c = verts[tri.v3];
    //     //     Console.WriteLine("FACING: "+ singlePoly.TriangleFacing(tri));
    //     //     Console.WriteLine("TRI: "+a+","+b+","+c);
               
    //     // }
    // }
    // void it_each_
    // void it_truncation_before_subdivision_produces_12_pentagons()
    // {
    // 	List<Vector3> verts = icoSphere.GetVertices();
    // 	verts.Count.should_be(12);
    // 	icoSphere.ConvertToTruncatedIsocahedron();
    // 	verts.Count.should_be(12+20);
    	
    // 	for(int i=0; i < verts.Count;i++)
    // 	{
    // 		TriangleIndices[] faces = icoSphere.GetFacesAroundVertex(i);
    // 		Console.WriteLine(i+" Num Faces:  "+faces.Length);
    // 		faces.Length.should_be(5);
    // 		// faces.Length.Should().BeOneOf(5,6);
    // 	}
    // }
    // void it_after_a_single_subdivision_each_vertex_has_5_or_6_faces_around_it()
    // {
    // 	icoSphere.SubdivideFaces();
    // 	List<Vector3> verts = icoSphere.GetVertices();

    // 	for(int i=0; i < verts.Count;i++)
    // 	{
    // 		TriangleIndices[] faces = icoSphere.GetFacesAroundVertex(i);
    // 		Console.WriteLine("Num Faces:  "+faces.Length);
    // 		(faces.Length ==5 || faces.Length==6).should_be_true();
    // 		// faces.Length.Should().BeOneOf(5,6);
    // 	}
    // }
   
}
