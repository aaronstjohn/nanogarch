using System.Collections.Generic;
using Entitas;
using UnityEngine;
using System.Linq;

public sealed class PlanetaryGridPolyDeSelectionSystem : ReactiveSystem {

    readonly Contexts _contexts;
    public PlanetaryGridPolyDeSelectionSystem(Contexts contexts) : base(contexts.core) {
        _contexts = contexts;
    
    }
    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.Selected,GroupEvent.Removed);
    }

    protected override bool Filter(Entity entity) {
        return true;
    }

    protected override void Execute(List<Entity> entities) {
    	// var selectedItem = entities.SingleEntity();
        foreach(var e in entities)
            e.RemoveInSpotlight();

        // Debug.Log("Got selection on grid item: "+selectedItem.name.id);
        // selectedItem.AddInSpotlight(1.4f);

    }
}