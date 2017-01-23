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
	readonly Context _context;
  public ProcessMoveCommandIssuedSystem(Contexts contexts) :base(contexts.core)
	{
		_context= contexts.core;
	}
  protected override Collector GetTrigger(Context context) {
      return context.CreateCollector( CoreMatcher.CommandIssued );
  }

  protected override bool Filter(Entity entity) {
      return entity.isUnit && entity.isReceivingOrders && entity.commandIssued.command==CommandType.Move;
  }
  protected override void Execute(List<Entity> entities) {
      Debug.Log("Move Command Issued!!");
      var unit = entities.SingleEntity();
      int cellId = unit.inGridCell.id;
      var cell = _context.GetEntityWithGridCellId(cellId);
      int[] neighbors = cell.gridCell.neighbors;
      foreach(var n in neighbors)
      {
          Debug.Log(string.Format("Cell {0} has neighbor {1}",cellId,n));
      }
      // entities.SingleEntity()
      //     .isReceivingOrders = true;


      // _contexts.core.CreateEntity()
      //         .AddResource("CommandPicker");

      // _contexts.input.inputModeEntity.isPickingEnabled = false;



  }
	
}