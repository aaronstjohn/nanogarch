
using System.Collections.Generic;


class CachedIndexedComputation<TKey,TValue>
{
	private Dictionary<TKey,int> cache;
	public delegate TValue ComputationDelegate(TKey key);
	public delegate int InsertValueDelegate(TValue value);
	private ComputationDelegate Compute;
	private InsertValueDelegate Insert;
	public CachedIndexedComputation(ComputationDelegate computeDelegate,InsertValueDelegate insertDelegate)
	{
		cache = new Dictionary<TKey,int>();
		Compute = computeDelegate;
		Insert =insertDelegate;
	}
	public int FetchOrCompute(TKey key)
	{
		int vertIndex;
		if (cache.TryGetValue(key, out vertIndex))
		{
			return vertIndex;
		}
		TValue value = Compute(key);
		vertIndex= Insert(value);
	
		// store it, return index
		
		cache.Add(key,vertIndex);
		return vertIndex;

	}
}