using Entitas;
using UnityEngine;

public sealed class EmitInputSystem : IExecuteSystem, ICleanupSystem {

    readonly Context _context;
    readonly Group _inputs;
    readonly Group _dragInputs;
    Ray ray;
    RaycastHit hit;
    // Transform cam;

    public EmitInputSystem(Contexts contexts) {
        _context = contexts.input;
        _inputs = _context.GetGroup(InputMatcher.Input);
        _dragInputs = _context.GetGroup(InputMatcher.InputDrag);
    }
    public void CreateMouseButtonEntity(bool isClick)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
           var input= _context.CreateEntity()
                .AddInput(hit.collider.gameObject.transform.InverseTransformPoint(hit.point),hit.collider.name);
            if (isClick)
            {
                Debug.Log("Marking input as selected ! ");
                input.isInputSelected = true;
            }
        }
    }
    public void Execute() {
        if (Input.GetMouseButtonDown(0))
        {
            CreateMouseButtonEntity(true);
        }
        else if(Input.GetMouseButton(0)) 
        {
            _context.CreateEntity()
                    .AddInputDrag(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
        }
        else
        {
            CreateMouseButtonEntity(false);
       
        }
        
    }

    public void Cleanup() {
        foreach(var e in _inputs.GetEntities()) {
            _context.DestroyEntity(e);
        }
        foreach(var e in _dragInputs.GetEntities()) {
            _context.DestroyEntity(e);
        }
    }
}
