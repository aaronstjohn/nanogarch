using Entitas;
using System.Collections.Generic;

public enum CommandType {None,Move, Fortify}
public interface ICommand 
{
	string GetCommandName();
	CommandType GetCommandType();
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
	public CommandType GetCommandType(){return CommandType.Move;}
}

[Core]
public class FortifiableComponent : IComponent, ICommand
{
	public string GetCommandName(){return "Fortify";}
	public CommandType GetCommandType(){return CommandType.Fortify;}
}

[Core]
public class MoveCommandComponent: IComponent
{
	public int sourceCell;
	public int destCell;
}