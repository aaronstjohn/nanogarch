using Entitas;
using Entitas.CodeGenerator;

[Core,SingleEntity]
public sealed class PlanetaryGridComponent: IComponent {
	public TruncatedIcosahedron geometry;
}