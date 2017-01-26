using UnityEngine;
using Entitas;

// [Core]
// public sealed class PositionComponent : IComponent
// {
// 	public Vector3 position;
// }

[Core]
public sealed class HeadingComponent : IComponent
{
	public Vector3 heading;
}
[Core]
public sealed class DestinationComponent:IComponent
{
	// public Vector3 srcPosition;
	public float distance;
	public float startTime;
	public Vector3 srcPosition;
	public Vector3 destPosition;
	public float distanceHeading;
	public Vector3 srcHeading;
	public Vector3 destHeading;
	public int destCellId;
}