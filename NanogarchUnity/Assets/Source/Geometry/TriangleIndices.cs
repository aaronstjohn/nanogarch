using System.Collections.Generic;

public struct TriangleIndices: System.IEquatable<TriangleIndices>
{
	public int v1;
	public int v2;
	public int v3;
	public int[] sorted;
	public static int[,] perms = new int[,]{{0,1,2},
										  {0,2,1},
										  {1,0,2},
										  {1,2,0},
										  {2,0,1},
										  {2,1,0}};
	public TriangleIndices(int vi1, int vi2, int vi3)
	{
		this.v1 = vi1;
		this.v2 = vi2;
		this.v3 = vi3;
		sorted = new int[3];
		sorted[0] = v1;
		sorted[1] = v2;
		sorted[2] = v3;
		System.Array.Sort(sorted);

		
	}
	public IEnumerable<TriangleIndices> Permutations()
	{
		for ( int i =0; i<perms.GetLength(0); i++)
		{
			yield return new TriangleIndices(sorted[perms[i,0]],
											 sorted[perms[i,1]],
											 sorted[perms[i,2]]);
		}
		
	}
	public bool Matches(int vertIndex,int n)
	{
		int countMatches = 0;
		foreach(int v in sorted)
		{
			if(v==vertIndex)
				countMatches++;
		}
		return countMatches == n;
	}
	public bool ExclusiveMatchesPair(TriangleIndices other)
	{
		if( Matches(other.v1,1) && Matches(other.v2,1) && Matches(other.v3,0))
			return true;
		if( Matches(other.v1,1) && Matches(other.v2,0) && Matches(other.v3,1))
			return true;
		if( Matches(other.v1,0) && Matches(other.v2,1) && Matches(other.v3,1))
			return true;
		return false;
		
	}
	
	override public string ToString()
	{
		return "{"+v1+","+v2+","+v3+"}";
	}
	override public int GetHashCode()
	{
		int hashcode = 23;
		hashcode = (hashcode * 37) + sorted[0];
		hashcode = (hashcode * 37) + sorted[1];
		hashcode = (hashcode * 37) + sorted[2];
		return hashcode;
	}
	public bool Equals(TriangleIndices other)
	{
		
		bool val =true;
		for (int i =0; i<3; i++)
			if( sorted[i] != other.sorted[i])
			{	val=false;
				break;
			}
		// Console.WriteLine(this.ToString()+"=="+other.ToString()+" : "+val);
		return val;
	}
		
	

}