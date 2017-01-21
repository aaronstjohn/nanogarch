using Entitas;
using System.Collections.Generic;
public interface ICommand 
{
	string GetCommandName();
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

	public string GetCommandName(){return "Move";}
}

[Core]
public class FortifiableComponent : IComponent, ICommand
{
	public string GetCommandName(){return "Fortify";}
}