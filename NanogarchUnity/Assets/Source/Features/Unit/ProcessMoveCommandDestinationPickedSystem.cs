
using Entitas;
using UnityEngine;
using System.Collections.Generic;

   // return new Collector(
   //          new Group[] {
   //              context.GetGroup(CoreMatcher.Resource),
   //              context.GetGroup(CoreMatcher.Destroy),
   //          },
   //          new GroupEvent[] {
   //              GroupEvent.AddedOrRemoved,
   //              GroupEvent.Added
   //          }
   //      );
public sealed class ProcessMoveCommandDestinationPickedSystem : ReactiveSystem 
{
	readonly Contexts _contexts;
  readonly Context _core;
  readonly Context _input;
  public ProcessMoveCommandDestinationPickedSystem(Contexts contexts) :base(contexts.core)
  {
    _contexts= contexts;
    _core = _contexts.core;
    _input = _contexts.input;
  }
  protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.Picked,GroupEvent.Added);
  }

  protected override bool Filter(Entity entity) {
      return _core.isReceivingOrders && 
             _core.receivingOrdersEntity.hasCommandIssued &&
             _core.receivingOrdersEntity.commandIssued.command==CommandType.Move && 
             _core.receivingOrdersEntity.isPickingMoveDestination &&
             entity.hasGridCell &&
             _core.receivingOrdersEntity.hasInGridCell &&
             _core.receivingOrdersEntity.inGridCell.id != entity.gridCell.id ? true:false;
  }

  protected override void Execute(List<Entity> entities) {
      Debug.Log("PICKeD MOVE DESTINATION!");
      var gridCellEnt = entities.SingleEntity();
      _core.CreateEntity()
            .AddSpawn(gridCellEnt.gridCell.id)
            .AddResource("DestinationMarker");
      var unit = _core.receivingOrdersEntity;//
      unit.AddOrders(new MoveOrder(unit.id.id,unit.inGridCell.id,gridCellEnt.gridCell.id));
      unit.isPickingMoveDestination = false;
      unit.isReceivingOrders = false;
      unit.RemoveCommandIssued();


  }
	
}