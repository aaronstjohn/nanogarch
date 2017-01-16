using System.Collections.Generic;
using Entitas;
using UnityEngine;
using System.Linq;

public sealed class ProcessPlanetaryGridInputSystem : ReactiveSystem {

    readonly Contexts _contexts;
    public ProcessPlanetaryGridInputSystem(Contexts contexts) : base(contexts.input) {
        _contexts = contexts;
    
    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(InputMatcher.Input);
    }

    protected override bool Filter(Entity entity) {
        return entity.hasInput && entity.input.targetName=="PlanetaryGrid";
    }

    protected override void Execute(List<Entity> entities) {
        var inputEntity = entities.SingleEntity();
        var input = inputEntity.input;
        var planet = _contexts.core.GetEntityNamed(input.targetName);
        int closestPolyIdx = planet.planetaryGrid.geometry.NearestPoly(input.inputPos);
        var polyEntity = _contexts.core.GetEntityWithPlanetaryGridPolygonId(closestPolyIdx);
        // Debug.Log("got planet input on poly: "+polyEntity.planetaryGridPolygon.polygonId);
        polyEntity.IsInFocus(true);

    }
}