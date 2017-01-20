using System.Collections.Generic;
using Entitas;
using System;
using UnityEngine;
using System.Linq;

public sealed class RemoveSpotlightSystem : ReactiveSystem {

    readonly Contexts _contexts;
    public RemoveSpotlightSystem(Contexts contexts) : base(contexts.core) {
        _contexts = contexts;
    
    }
    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.InSpotlight,GroupEvent.Removed);
    }

    protected override bool Filter(Entity entity) {
        return entity.hasView && entity.hasPlanetaryGridPolygon;
    }
    // readonly Transform _spotlightContainer = new GameObject("Spotlights").transform;

    protected override void Execute(List<Entity> entities) {
        // var planetEntity = _contexts.core.planetaryGridEntity;
        foreach(var e in entities)
        {   
            GameObject.Destroy(e.spotlight.spotlight);
            e.RemoveSpotlight();

            // var res = Resources.Load<GameObject>("Spotlight");
            // GameObject spotGameObject = null;
            // try {
            //     spotGameObject = UnityEngine.Object.Instantiate(res);
            //     spotGameObject.name = e.view.gameObject.name+"_Spotlight";
            // } catch(Exception) {
            //     Debug.Log("Cannot instantiate " + res);
            // }

            // if(spotGameObject != null) {
            //     spotGameObject.transform.parent = planetEntity.view.gameObject.transform;
                
            //     // Debug.Log("COMPONENT ID for inspotlight: "+CoreComponentIds.InSpotlight);
            //     // var inSpot = e.GetComponent(CoreComponentIds.InSpotlight);
            //     // Debug.Log("TYPE IS "+inSpot.GetType());
            //     spotGameObject.transform.position=planetEntity.view.gameObject.transform.TransformPoint(e.planetaryGridPolygon.centroid.normalized)*e.inSpotlight.distance;
            //     spotGameObject.transform.LookAt(planetEntity.view.gameObject.transform);
            //     e.AddSpotlight(spotGameObject);
            // }

        }
        // var focusedItem = entities.SingleEntity();
        // var planetEntity = _contexts.core.planetaryGridEntity;
        // var spotlightEntity = _contexts.core.GetEntityNamed("PolygonFocusSpotlight");
        
        // spotlightEntity.view.gameObject.transform.position=planetEntity.view.gameObject.transform.TransformPoint(focusedItem.planetaryGridPolygon.centroid.normalized)*1.4f;
        // spotlightEntity.LookAt(planetEntity);
    }
}