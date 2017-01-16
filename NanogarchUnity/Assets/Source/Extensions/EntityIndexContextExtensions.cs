using Entitas;

public static class EntityIndexContextExtensions {

    public const string NameKey = "Name";
    public const string PlanetaryGridPolygonKey = "PlanetaryGridPolygon";
    
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
        var gridPolygonIndex = new PrimaryEntityIndex<int>(
            contexts.core.GetGroup(CoreMatcher.PlanetaryGridPolygon),
            (entity, component) => {
                var planetaryGridPolygon = component as PlanetaryGridPolygonComponent;
                return planetaryGridPolygon != null
                    ? planetaryGridPolygon.polygonId
                    : entity.planetaryGridPolygon.polygonId;
            }
        );

        contexts.core.AddEntityIndex(PlanetaryGridPolygonKey, gridPolygonIndex);
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
}