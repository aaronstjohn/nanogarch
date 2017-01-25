using Entitas;
using Entitas.CodeGenerator;

[Core,SingleEntity]
public sealed class CommandStateComponent: IComponent 
{
	
}

[Core,SingleEntity]
public sealed class CommandTargetComponent:IComponent
{

}

[Core,SingleEntity]
public class ReceivingOrdersComponent : IComponent
{	
	// public List<ICommand> commands;
}

[Core,SingleEntity]
public class CommandIssuedComponent: IComponent
{
	public CommandType command;
}
