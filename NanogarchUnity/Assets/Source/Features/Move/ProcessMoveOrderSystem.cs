using Entitas;
using UnityEngine;
using System.Collections.Generic;

public sealed class ProcessMoveOrderSystem : ReactiveSystem 
{
	readonly Contexts _contexts;
    public ProcessMoveOrderSystem(Contexts contexts) :base(contexts.core)
	{
		_contexts= contexts;
	}
	protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.ExecutingOrders,GroupEvent.Added);
    }

    protected override bool Filter(Entity entity) {

        return entity.hasOrders && entity.orders.order.type == CommandType.Move;
    }
    protected override void Execute(List<Entity> entities) {

    	Debug.Log("Executing Move Order!");
        float startTime = Time.time;
    	foreach(var e in entities)
    	{
            MoveOrder move = (MoveOrder)e.orders.order;
    		var srcCellEnt = _contexts.core.GetEntityWithGridCellId(move.srcCell);
            var dstCellEnt = _contexts.core.GetEntityWithGridCellId(move.dstCell);
            Vector3 heading = (dstCellEnt.gridCell.centroid - srcCellEnt.gridCell.centroid).normalized;
            
            float distance = Vector3.Distance(srcCellEnt.gridCell.centroid,dstCellEnt.gridCell.centroid);

            e.AddDestination(distance,startTime,srcCellEnt.gridCell.centroid,dstCellEnt.gridCell.centroid,dstCellEnt.gridCell.id);
    	}
        
  
    }
}

