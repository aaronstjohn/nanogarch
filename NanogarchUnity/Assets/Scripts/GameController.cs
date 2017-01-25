using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour {

    Systems _systems;
    Contexts _contexts;
    void Awake()
    {
         Random.InitState(42);

        _contexts = Contexts.sharedInstance;
        _contexts.SetAllContexts();
        _contexts.AddEntityIndices();

        _systems = createSystems(_contexts);
        _systems.Initialize();
    }
    void Start() {
       SpawnUnit(36, "Tank");
    }
    void SpawnUnit(int gridId,string resource)
    {
        _contexts.core.CreateEntity()
            .IsUnit(true)
            .AddMovement(1)
            .IsFortifiable(true)
            .AddSpawn(gridId)
            .AddName("Unit")
            .AddId(1)
            .AddResource(resource);
    }
    void Update() {
        _systems.Execute();
        _systems.Cleanup();
    }

    Systems createSystems(Contexts contexts) {
        return new Feature("Systems")

            // Initialize
            .Add(new InitializeInputSystem(contexts))
            .Add(new InitializeUISystem(contexts))
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
            .Add(new LabelGridPolysSystem(contexts))
            // .Add(new RotateSystem(contexts))
            .Add(new ProcessUnitPickedSystem(contexts))
            .Add(new SpawnSystem(contexts))
            .Add(new PlanetaryGridCellAlignmentSystem(contexts))
            .Add(new PlanetaryGridCellPositionSystem(contexts))
            .Add(new ProcessMoveCommandIssuedSystem(contexts))
            .Add(new ProcessMoveCommandDestinationPickedSystem(contexts))
            .Add(new ProcessMoveOrderSystem(contexts))
            .Add(new ProcessDestinationSystem(contexts))


            // // Render
            .Add(new RemoveViewSystem(contexts))
            .Add(new AddViewSystem(contexts))
            .Add(new AddExecuteOrdersButtonSystem(contexts))
            // .Add(new RenderPositionSystem(contexts))

            // // Destroy
            .Add(new DestroySystem(contexts));
    }
}