package nanogarch.components;

import openfl.display.DisplayObjectContainer;

class Display
{
    public var displayObject(default, null):DisplayObjectContainer;

    public function new()
    {
        
    }
    public function initialize(displayObject:DisplayObjectContainer):Display
    {
    	this.displayObject = displayObject;
    	
    	return this;
    }
}
