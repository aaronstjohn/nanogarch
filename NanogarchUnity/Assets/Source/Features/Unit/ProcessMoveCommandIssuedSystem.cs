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
public sealed class ProcessMoveCommandIssuedSystem : ReactiveSystem 
{
	readonly Contexts _contexts;
  readonly Context _core;
  readonly Context _input;
  public ProcessMoveCommandIssuedSystem(Contexts contexts) :base(contexts.core)
	{
		_contexts= contexts;
    _core = _contexts.core;
    _input = _contexts.input;
	}
  protected override Collector GetTrigger(Context context) {
      return context.CreateCollector( CoreMatcher.CommandIssued );
  }

  protected override bool Filter(Entity entity) {
      return _core.isReceivingOrders && _core.receivingOrdersEntity.commandIssued.command==CommandType.Move;
  }
  protected override void Execute(List<Entity> entities) {
      Debug.Log("Move Command Issued!!");
      var unit = _core.receivingOrdersEntity;
      int cellId = unit.inGridCell.id;
      var cell = _core.GetEntityWithGridCellId(cellId);
      int[] neighbors = cell.gridCell.neighbors;
      foreach(var n in neighbors)
      {
          Debug.Log(string.Format("Cell {0} has neighbor {1}",cellId,n));
      }
      _input.inputModeEntity.isPickingEnabled = true;
      unit.isPickingMoveDestination = true;
      // entities.SingleEntity()
      //     .isReceivingOrders = true;


      // _contexts.core.CreateEntity()
      //         .AddResource("CommandPicker");

      // _contexts.input.inputModeEntity.isPickingEnabled = false;



  }
	
}