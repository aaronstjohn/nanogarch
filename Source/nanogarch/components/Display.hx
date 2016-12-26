package nanogarch.components;

import openfl.display.DisplayObject;

class Display
{
    public var displayObject(default, null):DisplayObject;

    public function new()
    {
        
    }
    public function initialize(displayObject:DisplayObject):Display
    {
    	this.displayObject = displayObject;
    	
    	return this;
    }
}
