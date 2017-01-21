using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour {

    Systems _systems;
    void Awake()
    {
         Random.InitState(42);

        var contexts = Contexts.sharedInstance;
        contexts.SetAllContexts();
        contexts.AddEntityIndices();

        _systems = createSystems(contexts);
        _systems.Initialize();
    }
    void Start() {
       
    }

    void Update() {
        _systems.Execute();
        _systems.Cleanup();
    }

    Systems createSystems(Contexts contexts) {
        return new Feature("Systems")

            // Initialize
            .Add(new InitializeInputSystem(contexts))
            .Add(new InitializePlanetaryGridSystem(contexts))
            // .Add(new CreateFocusSpotlightSystem(contexts))

            // // Input
            .Add(new CapturePickSystem(contexts))
            .Add(new ProcessPlanetaryGridPickSystem(contexts))
            // .Add(new ProcessPlanetaryGridInputSystem(contexts))
            // .Add(new PlanetaryGridPolySelectionSystem(contexts))
            // .Add(new PlanetaryGridPolyFocusSystem(contexts))
            // .Add(new ProcessPlanetaryGridDragInputSystem(contexts))
            // .Add(new AddSpotlightSystem(contexts))
            


            // // Update
            // .Add(new LabelGridPolysSystem(contexts))
            // .Add(new RotateSystem(contexts))
            .Add(new ProcessUnitPickedSystem(contexts))
            .Add(new SpawnUnitSystem(contexts))


            // // Render
            // .Add(new RemoveViewSystem(contexts))
            .Add(new AddViewSystem(contexts))
            // .Add(new RenderPositionSystem(contexts))

            // // Destroy
            .Add(new DestroySystem(contexts));
    }
}