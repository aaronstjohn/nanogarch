using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour {

    Systems _systems;

    void Start() {
        Random.InitState(42);

        var contexts = Contexts.sharedInstance;
        contexts.SetAllContexts();
        contexts.AddEntityIndices();

        _systems = createSystems(contexts);
        _systems.Initialize();
    }

    void Update() {
        _systems.Execute();
        _systems.Cleanup();
    }

    Systems createSystems(Contexts contexts) {
        return new Feature("Systems")

            // Initialize
            .Add(new CreatePlanetaryGridSystem(contexts))
            .Add(new CreateFocusSpotlightSystem(contexts))
            // .Add(new CreateOpponentsSystem(contexts))
            // .Add(new CreateFinishLineSystem(contexts))

            // // Input
            .Add(new EmitInputSystem(contexts))
            .Add(new ProcessPlanetaryGridInputSystem(contexts))
            .Add(new PlanetaryGridPolyFocusSystem(contexts))


            // // Update
            .Add(new LabelGridPolysSystem(contexts))
            // .Add(new AccelerateSystem(contexts))
            // .Add(new MoveSystem(contexts))
            // .Add(new ReachedFinishSystem(contexts))

            // // Render
            // .Add(new RemoveViewSystem(contexts))
            .Add(new AddViewSystem(contexts))
            // .Add(new RenderPositionSystem(contexts))

            // // Destroy
            .Add(new DestroySystem(contexts));
    }
}