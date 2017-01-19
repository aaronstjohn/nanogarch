using UnityEngine;
using Entitas;
public static class ViewComponentExtensions
{
	public static void LookAt(this Entity entity, Entity other, Vector3? worldUp = null)
	{

		if(entity.hasView && other.hasView)
		{

			entity.view.gameObject.transform.LookAt(other.view.gameObject.transform,worldUp ?? Vector3.up);
		}
		// spotlightEntity.view.gameObject.transform.position=focusedItem.planetaryGridPolygon.centroid.normalized*1.4f;
        
	}
}