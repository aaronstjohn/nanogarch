package nanogarch.util;
import ash.core.Entity;
import minject.Injector;

class InjectiveEntity extends Entity 
{
	// @inject public var globalInjector:Injector;
	public var injector(get,null):Injector;
	@inject public function new( globalInjector:Injector)
	{
		super();
		
		injector= globalInjector.createChildInjector();
	}

	override public function add<T>(component:T, componentClass:Class<Dynamic> = null):Entity
	{
		var type = Type.getClass(component);
		var typeName = Type.getClassName(type);
		injector.mapType(typeName).toValue(component);
		return super.add(component,componentClass);

	}
	function get_injector():Injector {return this.injector;}
}