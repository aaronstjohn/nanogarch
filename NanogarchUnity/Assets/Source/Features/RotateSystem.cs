using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class RotateSystem : IExecuteSystem {
	readonly Context _context;
	readonly Group _rotaters;
    public RotateSystem(Contexts contexts) 
	{
		_context = contexts.core;
		_rotaters = _context.GetGroup(Matcher.AllOf(CoreMatcher.Rotation,CoreMatcher.View) ) ;
	}
	public void Execute() {
        
        Quaternion fromRotation;
 		Quaternion toRotation;
 		
    	float lerpSpeed = 1.0f;
        foreach(var e in _rotaters.GetEntities()) {
            fromRotation = e.view.gameObject.transform.rotation;
			toRotation = Quaternion.Euler(e.rotation.yDegrees,e.rotation.xDegrees,0);
			e.view.gameObject.transform.rotation =Quaternion.Lerp(fromRotation,toRotation,Time.deltaTime  * lerpSpeed);

        }
        
    }
}