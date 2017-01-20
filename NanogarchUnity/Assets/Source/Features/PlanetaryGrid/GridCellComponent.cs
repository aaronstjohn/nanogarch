using Entitas;
using UnityEngine;

[Core]
public sealed class GridCellComponent : IComponent
{
	public int id;
	public Vector3 centroid;
}