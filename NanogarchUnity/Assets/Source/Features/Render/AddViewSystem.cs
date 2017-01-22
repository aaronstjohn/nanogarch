using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class AddViewSystem : ReactiveSystem {

    public AddViewSystem(Contexts contexts) : base(contexts.core) {
    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.Resource);
    }

    protected override bool Filter(Entity entity) {
        return entity.hasResource;
    }

    readonly Transform _viewContainer = new GameObject("Views").transform;
    readonly Transform _canvasContainer = GameObject.Find("Canvas").transform;

    protected override void Execute(List<Entity> entities) {
        foreach(var e in entities) {
            var res = Resources.Load<GameObject>(e.resource.name);
            GameObject gameObject = null;
            try {
                gameObject = UnityEngine.Object.Instantiate(res);
                gameObject.name = e.resource.name;
            } catch(Exception) {
                Debug.Log("Cannot instantiate " + res);
            }

            if(gameObject != null) {
                if(gameObject.GetComponent<CanvasRenderer>()!=null)
                    gameObject.transform.parent = _canvasContainer;
                else
                    gameObject.transform.parent = _viewContainer;
                
                if(gameObject.GetComponent<EntityLink>()!=null)
                    gameObject.LinkEntity(e);
                else
                     e.AddView(gameObject);
                
            }
        }
    }
}
