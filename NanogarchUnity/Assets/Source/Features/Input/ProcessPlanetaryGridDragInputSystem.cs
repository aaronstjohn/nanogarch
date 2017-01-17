using System.Collections.Generic;
using Entitas;
using UnityEngine;
using System.Linq;

public sealed class ProcessPlanetaryGridDragInputSystem : ReactiveSystem {

    readonly Contexts _contexts;
    private Quaternion fromRotation;
 	private Quaternion toRotation;
    public ProcessPlanetaryGridDragInputSystem(Contexts contexts) : base(contexts.input) {
        _contexts = contexts;
    
    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(InputMatcher.InputDrag,GroupEvent.Added);
    }

    protected override bool Filter(Entity entity) {
        return entity.hasInputDrag;
    }

    protected override void Execute(List<Entity> entities) {

    	float speed = 10.0f;
    	float friction = 0.25f;
    	var dragInput = entities.SingleEntity().inputDrag;
    	var planetEntity = _contexts.core.planetaryGridEntity;
    	float xDeg = planetEntity.rotation.xDegrees-dragInput.x_delta*speed*friction;
    	float yDeg = planetEntity.rotation.yDegrees+dragInput.y_delta*speed*friction;
    	planetEntity.ReplaceRotation(xDeg, yDeg);
    }
}