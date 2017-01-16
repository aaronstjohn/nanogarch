
using System.Collections.Generic;

//Has a central vertex that is shared between a set of triangles
public struct RadialPolyTris
{
	public List<TriangleIndices> faces;
	public int centroid;
	public RadialPolyTris(int centroid, List<TriangleIndices> tris)
	{
		this.centroid = centroid;
		faces = tris.Clone();

	}
}