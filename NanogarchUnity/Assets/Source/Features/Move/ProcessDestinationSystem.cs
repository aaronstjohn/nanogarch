using Entitas;
using UnityEngine;
using System.Collections.Generic;

public sealed class ProcessDestinationSystem : IExecuteSystem 
{
	readonly Contexts _contexts;
    readonly Group _moverGroup;
    // readonly float _planetRadius;
    public ProcessDestinationSystem(Contexts contexts) 
	{
		_contexts= contexts;
        _moverGroup = _contexts.core.GetGroup(Matcher.AllOf(CoreMatcher.Destination,CoreMatcher.View) ) ;
	   
    }
	
    public void Execute() {
        float lerpSpeed = 0.3f;
        float time = Time.time;
        float planetRadius = _contexts.core.planetaryGrid.geometry.GetRadius();
        foreach(var e in _moverGroup.GetEntities()) {
            float distCovered = (time- e.destination.startTime) * lerpSpeed;
            float fracJourney = distCovered / e.destination.distance;
            // e.view.gameObject.transform.position = 
            Vector3 pos = Vector3.Lerp(e.destination.srcPosition, e.destination.destPosition, fracJourney);
            e.view.gameObject.transform.position = pos.normalized*planetRadius;
            if(fracJourney >= 0.99f )
            {
                Debug.Log("AT DESTINATION!");
                e.ReplaceInGridCell(e.destination.destCellId);
                e.RemoveDestination();

            }

           // e.view.gameObject.transform.position= Vector3.Lerp(e.view.gameObject.transform.position, e.destination.destPosition, Time.deltaTime  * lerpSpeed);
        }
    	// Debug.Log("Executing Move Order!");
    	// foreach(var e in entities)
    	// {
     //  //       MoveOrder move = (MoveOrder)e.orders.order;
    	// 	// var srcCellEnt = _contexts.core.GetEntityWithGridCellId(move.srcCell);
     //  //       var dstCellEnt = _contexts.core.GetEntityWithGridCellId(move.dstCell);
     //  //       Vector3 heading = (dstCellEnt.gridCell.centroid - srcCellEnt.gridCell.centroid).normalized;
     //  //       e.AddDestination(dstCellEnt.gridCell.centroid,heading,dstCellEnt.gridCell.id);
    	// }
        
  
    }
}

