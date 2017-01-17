using UnityEngine;
using Entitas;
public static class ViewComponentExtensions
{
	public static void LookAt(this Entity entity, Entity other)
	{
		if(entity.hasView && other.hasView)
			entity.view.gameObject.transform.LookAt(other.view.gameObject.transform);
		// spotlightEntity.view.gameObject.transform.position=focusedItem.planetaryGridPolygon.centroid.normalized*1.4f;
        
	}
}