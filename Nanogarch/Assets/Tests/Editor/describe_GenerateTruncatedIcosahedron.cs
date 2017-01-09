using NSpec;
using UnityEngine;
using System.Collections.Generic;
class describe_GenerateTruncatedIcosahedron : nspec {

	IcoSphere icoSphere;
	void before_each()
    {
    	icoSphere = new IcoSphere(0.5f);
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
    void it_after_a_single_subdivision_each_vertex_has_5_or_6_faces_around_it()
    {
    	icoSphere.SubdivideFaces();
    	List<Vector3> verts = icoSphere.GetVertices();

    	for(int i=0; i < verts.Count;i++)
    	{
    		TriangleIndices[] faces = icoSphere.GetFacesAroundVertex(i);
    		faces.Length.Should().BeOneOf(5,6);
    	}
    }
   
}
