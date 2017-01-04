package nanogarch.components;

class Unit
{
	public var type(get,null):String;
	public function new(){}
	public function initialize(type:String)
	{
		this.type = type;
		return this;
	}
	public function get_type():String {return type;}
}