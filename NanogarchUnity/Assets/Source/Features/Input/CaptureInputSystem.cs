using Entitas;
using UnityEngine;
public sealed class CaptureInputSystem :  IExecuteSystem, ICleanupSystem ,IInitializeSystem{
	readonly Group _inputs;
	readonly Context _context;
	Entity _inputEntity;

	public CaptureInputSystem(Contexts contexts) {
        _context = contexts.input;
        _inputs = _context.GetGroup(InputMatcher.Input);
       
    }
    public void Initialize()
    {
    	 _inputEntity = _context.inputModeEntity;
    }
    public void CaptureSelection()
    {
        if(!_inputEntity.isSelectionEnabled)
            return;

        if(Input.GetMouseButtonDown(0))
        {
            _inputEntity.AddMouseDownTime(Time.time);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(Time.time - _inputEntity.mouseDownTime.time < 0.25)
            {
                Debug.Log("CLICK!");
            }
            _inputEntity.RemoveMouseDownTime();
        }
    }
    public void Execute() {
     	
        CaptureSelection();
     	
     	// CaptureSelect();
     	// CaptureDrag();
     	// CaptureHover();
        // if (Input.GetMouseButtonDown(0))
        // {
        //     CreateMouseButtonEntity(true);
        // }
        // else if(Input.GetMouseButton(0)) 
        // {
        //     _context.CreateEntity()
        //             .AddInputDrag(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
        // }
        // else
        // {
        //     CreateMouseButtonEntity(false);
       
        // }
        
    }

    public void Cleanup() {
        foreach(var e in _inputs.GetEntities()) {
            _context.DestroyEntity(e);
        }
    }
}