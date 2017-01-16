using System;
public struct VertexPair: IEquatable<VertexPair>
{
	public int v1;
	public int v2;
	public int[] sorted;
	public VertexPair(int v1, int v2)
	{
		this.v1 = v1;
		this.v2 = v2;
		sorted = new int[2] {v1,v2};
		Array.Sort(sorted);
	}
	public bool Equals(VertexPair other)
	{
		for (int i =0; i<2; i++)
			if( sorted[i] != other.sorted[i])
				return false;
		return true;
	}
	override public int GetHashCode()
	{
		int hashcode = 23;
		hashcode = (hashcode * 37) + sorted[0];
		hashcode = (hashcode * 37) + sorted[1];
		// hashcode = (hashcode * 37) + sorted[2];
		return hashcode;
	}
}