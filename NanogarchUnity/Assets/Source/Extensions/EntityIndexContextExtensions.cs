using Entitas;
using System.Collections.Generic;
public static class EntityIndexContextExtensions {

    public const string NameKey = "Name";
    public const string PlanetaryGridPolygonKey = "PlanetaryGridPolygon";
    public const string InPlanetaryGridPolygonKey = "InPlanetaryGridPolygon";
    
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
        //Fetches the actual grid polygon 
        var gridPolygonIndex = new PrimaryEntityIndex<int>(
            contexts.core.GetGroup(CoreMatcher.PlanetaryGridPolygon),
            (entity, component) => {
                var planetaryGridPolygon = component as PlanetaryGridPolygonComponent;
                return planetaryGridPolygon != null
                    ? planetaryGridPolygon.polygonId
                    : entity.planetaryGridPolygon.polygonId;
            }
        );
        //Indexes everything inside of a grid polygon 
        var inGridPolygonIndex = new EntityIndex<int>(
            contexts.core.GetGroup(CoreMatcher.InGridPolygon),
            (entity, component) => {
                var inPlanetaryGridPolygon = component as InGridPolygonComponent;
                return inPlanetaryGridPolygon != null
                    ? inPlanetaryGridPolygon.gridPolyId
                    : entity.inGridPolygon.gridPolyId;
            }
        );
        contexts.core.AddEntityIndex(PlanetaryGridPolygonKey, gridPolygonIndex);
        contexts.core.AddEntityIndex(InPlanetaryGridPolygonKey, inGridPolygonIndex);
        contexts.core.AddEntityIndex(NameKey, nameIndex);
    }

    public static Entity GetEntityNamed(this Context context, string id) {
        var index = (PrimaryEntityIndex<string>)context.GetEntityIndex(NameKey);
        return index.GetEntity(id);
    }

    public static Entity GetEntityWithPlanetaryGridPolygonId(this Context context, int polyId) {
        var index = (PrimaryEntityIndex<int>)context.GetEntityIndex(PlanetaryGridPolygonKey);
        return index.GetEntity(polyId);
    }
    public static HashSet<Entity> GetEntitiesInPlanetaryGridPolygonWithId(this Context context, int polyId) {
        var index = (EntityIndex<int>)context.GetEntityIndex(InPlanetaryGridPolygonKey);
        return index.GetEntities(polyId);
    }
}