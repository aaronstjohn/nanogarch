using Entitas;
using System.Collections.Generic;
public interface ICommand 
{

}


[Core]
public class UnitComponent : IComponent
{	
	// public List<ICommand> commands;
}

[Core]
public class MovementComponent : IComponent, ICommand
{
	public int range;
}

[Core]
public class FortifiableComponent : IComponent, ICommand
{
	
}