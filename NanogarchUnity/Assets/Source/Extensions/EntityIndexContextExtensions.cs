using Entitas;
using System.Collections.Generic;
public static class EntityIndexContextExtensions {

    public const string NameKey = "Name";
    public const string GridCellKey = "GridCell";
    public const string InGridCellKey = "InGridCell";
    
    public static void AddEntityIndices(this Contexts contexts) {
        var nameIndex = new PrimaryEntityIndex<string>(
            contexts.core.GetGroup(CoreMatcher.Name),
            (entity, component) => {
                var nameComponent = component as NameComponent;
                return nameComponent != null
                    ? nameComponent.id
                    : entity.name.id;
            }
        );
        //Fetches the actual grid cell 
        var gridCellIndex = new PrimaryEntityIndex<int>(
            contexts.core.GetGroup(CoreMatcher.GridCell),
            (entity, component) => {
                var gridCell = component as GridCellComponent;
                return gridCell != null
                    ? gridCell.id
                    : entity.gridCell.id;
            }
        );
        //Indexes everything inside of a grid cell 
        var inGridCellIndex = new EntityIndex<int>(
            contexts.core.GetGroup(CoreMatcher.InGridCell),
            (entity, component) => {
                var inGridCell = component as InGridCellComponent;
                return inGridCell != null
                    ? inGridCell.id
                    : entity.inGridCell.id;
            }
        );
        contexts.core.AddEntityIndex(GridCellKey, gridCellIndex);
        contexts.core.AddEntityIndex(InGridCellKey, inGridCellIndex);
        contexts.core.AddEntityIndex(NameKey, nameIndex);
    }

    public static Entity GetEntityNamed(this Context context, string id) {
        var index = (PrimaryEntityIndex<string>)context.GetEntityIndex(NameKey);
        return index.GetEntity(id);
    }

    public static Entity GetEntityWithGridCellId(this Context context, int id) {
        var index = (PrimaryEntityIndex<int>)context.GetEntityIndex(GridCellKey);
        return index.GetEntity(id);
    }
    public static HashSet<Entity> GetEntitiesInGridCell(this Context context, int id) {
        var index = (EntityIndex<int>)context.GetEntityIndex(InGridCellKey);
        return index.GetEntities(id);
    }
}