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
    	// float lerpSpeed = 1.0f;
    	var dragInput = entities.SingleEntity().inputDrag;
    	var planetEntity = _contexts.core.planetaryGridEntity;
    	// Debug.Log("Drag x : "+dragInput.x_delta+" Drag y: "+ dragInput.y_delta);
    	float xDeg = planetEntity.rotation.xDegrees-dragInput.x_delta*speed*friction;
    	float yDeg = planetEntity.rotation.yDegrees+dragInput.y_delta*speed*friction;
    	
    	// fromRotation = planetEntity.view.gameObject.transform.rotation;
    	// toRotation = Quaternion.Euler(yDeg,xDeg,0);
    	// Debug.Log("DRAGGING from "+fromRotation+" to: "+toRotation);
    	// Debug.Log("DRAGGING XDEG "+xDeg+" yDeg: "+yDeg);
    	
    	// planetEntity.view.gameObject.transform.rotation =Quaternion.Lerp(fromRotation,toRotation,Time.deltaTime  * lerpSpeed);
    	planetEntity.ReplaceRotation(xDeg, yDeg);
    	
        // var input = inputEntity.input;
        // var planetEntity = _contexts.core.planetaryGridEntity;

        // var spotlightEntity = _contexts.core.GetEntityNamed("CursorSpotlight");

        // spotlightEntity.view.gameObject.transform.position=input.inputPos.normalized*0.77f;
        // // spotlightEntity.view.gameObject.transform.LookAt(planetEntity.view.gameObject.transform);
        // spotlightEntity.LookAt(planetEntity);
        
        // int closestPolyIdx = planetEntity.planetaryGrid.geometry.NearestPoly(input.inputPos);
        // var polyEntity = _contexts.core.GetEntityWithPlanetaryGridPolygonId(closestPolyIdx);

        // //Take the focus off the last entity that was in focus
        // if(_contexts.core.inFocusEntity!=null)
        //  _contexts.core.inFocusEntity.isInFocus = false;
        
        // //Add focus to the new entity in focus 
        // polyEntity.isInFocus = true;
       

    }
}