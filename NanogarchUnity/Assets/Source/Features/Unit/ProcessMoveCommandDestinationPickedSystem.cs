
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

  // protected override bool Filter(Entity entity) {
  //     return entity.isUnit && entity.isReceivingOrders && entity.commandIssued.command==CommandType.Move;
  // }
  protected override void Execute(List<Entity> entities) {
      Debug.Log("PICKeD MOVE DESTINATION!");
      var gridCellEnt = entities.SingleEntity();
      _core.receivingOrdersEntity.ReplaceInGridCell(gridCellEnt.gridCell.id);
      
      // int cellId = unit.inGridCell.id;
      // var cell = _context.GetEntityWithGridCellId(cellId);
      // int[] neighbors = cell.gridCell.neighbors;
      // foreach(var n in neighbors)
      // {
      //     Debug.Log(string.Format("Cell {0} has neighbor {1}",cellId,n));
      // }
      // _contexts.input.inputModeEntity.isPickingEnabled = true;
      // entities.SingleEntity()
      //     .isReceivingOrders = true;


      // _contexts.core.CreateEntity()
      //         .AddResource("CommandPicker");

      // _contexts.input.inputModeEntity.isPickingEnabled = false;



  }
	
}