using Entitas;
using UnityEngine;
[Input]
public sealed class PickComponent :IComponent
{
	public Vector3 point;
	public GameObject gameObject;
}