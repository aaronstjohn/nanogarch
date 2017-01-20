using Entitas;
using UnityEngine;
public sealed class CapturePickSystem :  IExecuteSystem ,ICleanupSystem, IInitializeSystem{
	readonly Context _context;
	Entity _inputEntity;
    RaycastHit hit;

	public CapturePickSystem(Contexts contexts) {
        _context = contexts.input;
    }
    public void Initialize()
    {
    	 _inputEntity = _context.inputModeEntity;
    }
 
    public bool CapturePick()
    {
        if(!_inputEntity.isPickingEnabled)
            return false;

        if(Input.GetMouseButtonDown(0))
        {
            _inputEntity.AddMouseDownTime(Time.time);
        }
        else if(Input.GetMouseButtonUp(0) && _inputEntity.HasComponent(InputComponentIds.MouseDownTime))
        {
             float downTime = _inputEntity.mouseDownTime.time;
             _inputEntity.RemoveMouseDownTime();
            if(Time.time - downTime < 0.25)
            {
                return true;
            }
           
        }
        return false;
    }
    public void Execute() {
           

        if( CapturePick() && 
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            _context.CreateEntity()
                .AddPick(hit.point,hit.collider.gameObject);
            Debug.Log("PICK");
        }
        
    }

    public void Cleanup() {
        if(_inputEntity.HasComponent(InputComponentIds.Pick))
         _inputEntity.RemovePick();
    }
}