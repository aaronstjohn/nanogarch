using Entitas;
using UnityEngine;
using System.Collections.Generic;

public sealed class ProcessUnitPickedSystem : ReactiveSystem 
{
	readonly Contexts _contexts;
    public ProcessUnitPickedSystem(Contexts contexts) :base(contexts.core)
	{
		_contexts= contexts;
	}
	protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.Picked,GroupEvent.Added);
    }

    protected override bool Filter(Entity entity) {
        return entity.isUnit && entity.hasCommands;
    }
    protected override void Execute(List<Entity> entities) {

        entities.SingleEntity()
            .isReceivingOrders = true;


        _contexts.core.CreateEntity()
                .AddResource("CommandPicker");

        _contexts.input.inputModeEntity.isPickingEnabled = false;


  
    }
	
}