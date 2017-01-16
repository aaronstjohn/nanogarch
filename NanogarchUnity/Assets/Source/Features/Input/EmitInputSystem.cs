using Entitas;
using UnityEngine;

public sealed class EmitInputSystem : IExecuteSystem, ICleanupSystem {

    readonly Context _context;
    readonly Group _inputs;
    Ray ray;
    RaycastHit hit;

    public EmitInputSystem(Contexts contexts) {
        _context = contexts.input;
        _inputs = _context.GetGroup(InputMatcher.Input);
    }

    public void Execute() {
        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            _context.CreateEntity()
                .AddInput(hit.point.Clone(),hit.collider.name);
            // Debug.Log("Emitting Input "+hit.point+" On collider "+hit.collider.name);
        }
        
    }

    public void Cleanup() {
        // Debug.Log("CLEANING UP "+_inputs.GetEntities().Length);
        foreach(var e in _inputs.GetEntities()) {
            _context.DestroyEntity(e);
        }
    }
}
