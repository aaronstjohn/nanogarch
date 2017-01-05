package nanogarch;
import minject.Injector;
import minject.InjectorMapping;
import Type;
// import Signals.SignalPair;
import nanogarch.commands.Command;

class CommandBinder extends Injector
{
	//CommandBinder.bind(StartSignal,StartCommand);
	public function new()
	{
		super();
	}
	public function bind<T:Dynamic,U:Command<T>>(signalPairType:Class<Signals.SignalPair<T>> , commandType:Class<U>,?name:String):InjectorMapping<U>
	{
		//Create or fetch an instance of the signal 
		var signalPairInst = Type.createInstance(signalPairType,[]);

		//Get the name of the command for inection 
		var commandTypeName = Type.getClassName(commandType);

		//Create a mapping 
		var commandMapping = mapType(commandTypeName);

		//Add a handler to the signal that executes the command 
		signalPairInst.signal.handle(function(t:T){
			var command = getInstance(commandType);
			command.Execute(t);
		});

		//Now map the signal instance to this one
		var  signalPairTypeName = Type.getClassName(signalPairType);
		mapType(signalPairTypeName,name).toValue(signalPairInst);
		// map(SignalPair<T>,name).toValue(signalPairInst);

		return commandMapping;

	}
}