using System.Collections.Generic;
using Entitas;
using UnityEngine;
public sealed class DestroySystem : ReactiveSystem {
    readonly Context _context;

    public DestroySystem(Contexts contexts) : base(contexts.core) {
        _context = contexts.core;
    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.Destroy);
    }

    protected override bool Filter(Entity entity) {
        return entity.isDestroy;
    }

    protected override void Execute(List<Entity> entities) {
        foreach(var e in entities) {
            Debug.Log("DESTROYING ENTITY!!!");
            _context.DestroyEntity(e);
        }
    }
}
