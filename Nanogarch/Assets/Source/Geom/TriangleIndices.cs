using System;

using System.Collections;
public struct TriangleIndices: IEquatable<TriangleIndices>
{
	public int v1;
	public int v2;
	public int v3;
	public int[] sorted;

	public TriangleIndices(int vi1, int vi2, int vi3)
	{
		this.v1 = vi1;
		this.v2 = vi2;
		this.v3 = vi3;
		sorted = new int[3];
		sorted[0] = v1;
		sorted[1] = v2;
		sorted[2] = v3;
		Array.Sort(sorted);
		
	}
	
	public bool Equals(TriangleIndices other)
	{
		for (int i =0; i<3; i++)
			if( sorted[i] != other.sorted[i])
				return false;
		return true;
	}
		
	

}