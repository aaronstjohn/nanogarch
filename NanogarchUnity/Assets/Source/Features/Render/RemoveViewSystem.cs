using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class RemoveViewSystem : ReactiveSystem {

    public RemoveViewSystem(Contexts contexts) : base(contexts.core) {
    }

    protected override Collector GetTrigger(Context context) {
        return new Collector(
            new Group[] {
                context.GetGroup(CoreMatcher.Resource),
                context.GetGroup(CoreMatcher.Destroy),
            },
            new GroupEvent[] {
                GroupEvent.AddedOrRemoved,
                GroupEvent.Added
            }
        );
    }

	protected override bool Filter(Entity entity) {
        return entity.hasView;
	}
	

    protected override void Execute(List<Entity> entities) {
        foreach(var e in entities) {
            Object.Destroy(e.view.gameObject);
            e.RemoveView();
        }
    }
}
