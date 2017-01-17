using Entitas;
using UnityEngine;
using System.Collections.Generic;
public sealed class LabelGridPolysSystem:ReactiveSystem
{
	readonly Context _context;
   public LabelGridPolysSystem(Contexts contexts) :base(contexts.core)
	{
		_context= contexts.core;
	}
  protected override Collector GetTrigger(Context context) {
      return context.CreateCollector(CoreMatcher.View);
  }

  protected override bool Filter(Entity entity) {
      return entity.hasPlanetaryGridPolygon && entity.hasName;
  }
  protected override void Execute(List<Entity> entities) {
  	var planetEntity = _context.GetEntityNamed("PlanetaryGrid");
  	foreach(var e in entities)
  	{
  		var labelGO = e.view.gameObject;
      labelGO.name = "label_"+e.name.id;
      labelGO.transform.parent = planetEntity.view.gameObject.transform;
  		var textMesh = labelGO.GetComponent<TextMesh>();
      textMesh.text = e.name.id;
      labelGO.transform.position=e.planetaryGridPolygon.centroid.normalized*0.5f;
  		labelGO.transform.LookAt(planetEntity.view.gameObject.transform);
  	}
  }
}