using System.Collections.Generic;
using Entitas;
using UnityEngine;
using System.Linq;

public sealed class PlanetaryGridPolySelectionSystem : ReactiveSystem {

    readonly Contexts _contexts;
    public PlanetaryGridPolySelectionSystem(Contexts contexts) : base(contexts.core) {
        _contexts = contexts;
    
    }
    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.Selected,GroupEvent.Added);
    }

    protected override bool Filter(Entity entity) {
        return entity.isSelected && entity.hasPlanetaryGridPolygon;
    }

    protected override void Execute(List<Entity> entities) {
    	var selectedItem = entities.SingleEntity();
        // Debug.Log("Got selection on grid item: "+selectedItem.name.id);
        selectedItem.AddInSpotlight(1.4f);
        HashSet<Entity> entsInside = _contexts.core.GetEntitiesInGridCell(selectedItem.planetaryGridPolygon.polygonId);
        
        foreach(var e in entsInside)
            e.isSelected = true;
        
        Debug.Log("Found : "+entsInside.Count+" entities inside of this cell ");
    }
}