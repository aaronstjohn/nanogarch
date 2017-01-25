using Entitas;
using UnityEngine;
using System.Collections.Generic;

public sealed class AddExecuteOrdersButtonSystem : ReactiveSystem 
{
	readonly Contexts _contexts;
  public AddExecuteOrdersButtonSystem(Contexts contexts) :base(contexts.core)
	{
		_contexts= contexts;
	}
	protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.Orders,GroupEvent.Added);
    }

    protected override bool Filter(Entity entity) {

        return !_contexts.uI.uIModeEntity.isExecuteOrdersButtonEnabled;
    }
    protected override void Execute(List<Entity> entities) {

      
        _contexts.core.CreateEntity()
                .AddResource("ExecuteOrdersButton");
      
  
    }
	
}